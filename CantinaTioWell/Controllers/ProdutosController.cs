using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CantinaTioWell.Helper;
using CantinaTioWell.Models;

namespace CantinaTioWell.Controllers
{
    public class ProdutosController : Controller
    {
        private CantinaTioWellContext db = new CantinaTioWellContext();

        public ActionResult Index()
        {
            //Verifica se usuário está logado
            if (Session["user"] != null)
            {
                //Verifica perfil do usuário
                HttpCookie cookie = Request.Cookies.Get("MyCookie");
                int IdUsuario = Convert.ToInt32(cookie.Value);
                if (db.Clientes.Find(IdUsuario).Perfil == 1)
                {
                    bool adm = true;
                    ViewBag.Perfil = adm;
                }
                else
                {
                    return RedirectToAction("Index","Compras");
                }
                bool logado = true;
                ViewBag.Logado = logado;
                
                return View(db.Produtoes.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtoes.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nome,preco")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                db.Produtoes.Add(produto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(produto);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtoes.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nome,preco")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(produto);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtoes.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produto produto = db.Produtoes.Find(id);
            db.Produtoes.Remove(produto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ListaProdutosComprados()
        {
            HttpCookie cookie = Request.Cookies.Get("MyCookie");
            int IdUsuario = Convert.ToInt32(cookie.Value);

            List<Compra> compras = db.Compras.ToList();
            List<Produto> produtos = db.Produtoes.ToList();
            List<Cliente> clientes = db.Clientes.ToList();

            var produtosComDivida = from cust in compras
                                    join prod in produtos on cust.Produto.id equals prod.id
                                    join cli in clientes on cust.Cliente.id equals cli.id
                                    where cli.id == IdUsuario
                                    select new { NomeProduto = prod.nome, PrecoProduto = prod.preco, IdProduto = prod.id, IdCompra = cust.CompraId, DataCompra = cust.DataCompra };

            List<ProdutoCompra> todosProdutos = new List<ProdutoCompra>();

            foreach (var item in produtosComDivida)
            {
                todosProdutos.Add(new ProdutoCompra
                {
                    IdProduto = item.IdProduto,
                    NomeProduto = item.NomeProduto,
                    PrecoProduto = item.PrecoProduto,
                    IdCompra = item.IdCompra,
                    DataCompra = item.DataCompra
                });
            }

            bool logado = true;
            ViewBag.Logado = logado;
            bool perfil = false;
            ViewBag.Perfil = perfil;

            return View(todosProdutos);
        }
    }
}
