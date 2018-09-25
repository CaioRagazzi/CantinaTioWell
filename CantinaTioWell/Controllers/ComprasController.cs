using CantinaTioWell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CantinaTioWell.Controllers
{
    public class ComprasController : Controller
    {
        private CantinaTioWellContext db = new CantinaTioWellContext();

        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                bool logado = true;
                ViewBag.Logado = logado;
                bool perfil = false;
                ViewBag.Perfil = perfil;
                List<Produto> listaProdutos = db.Produtoes.ToList();
                return View(listaProdutos);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [ValidateInput(false)]
        public ActionResult Comprar(int id, string qtd)
        {

            HttpCookie cookie = Request.Cookies.Get("MyCookie");
            int IdUsuario = Convert.ToInt32(cookie.Value);


            var cliente = from cli in db.Clientes.ToList()
                          where cli.id == IdUsuario
                          select cli;

            Cliente clienteLogado = new Cliente();
            clienteLogado = cliente.FirstOrDefault();

            var produto = from pro in db.Produtoes.ToList()
                          where pro.id == id
                          select pro;

            Produto produtoSelecionado = new Produto();
            produtoSelecionado = produto.FirstOrDefault();

            Compra compra = new Compra(produtoSelecionado, clienteLogado);

            db.Compras.Add(compra);
            db.SaveChanges();

            return RedirectToAction("ListaProdutosComprados", "Produtos");
        }

        public ActionResult Excluir(int IdCompra)
        {
            var compras = db.Compras.ToList();

            var compra = from comp in compras
                         where comp.CompraId == IdCompra
                         select comp;

            db.Compras.Remove(compra.FirstOrDefault());
            db.SaveChanges();

            return RedirectToAction("ListaProdutosComprados", "Produtos");
        }
    }
}