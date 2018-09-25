using CantinaTioWell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CantinaTioWell.Controllers
{
    public class AdministrativoController : Controller
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
                    return RedirectToAction("Index", "Compras");
                }

                return RedirectToAction("ListaClientesComDivida");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult ListaClientesComDivida()
        {
            bool logado = true;
            ViewBag.Logado = logado;
            bool perfil = true;
            ViewBag.Perfil = perfil;

            //var clientes = from cli in db.Clientes.ToList()
            //               join comp in db.Compras.ToList() on cli.id equals comp.Cliente.id
            //               join prod in db.Produtoes.ToList() on comp.Produto.id equals prod.id
            //               select new { NomeCliente = cli.nome, NomeProduto = prod.nome, PrecoProduto = prod.preco };

            var clientes = from cli in db.Clientes.ToList()
                            join comp in db.Compras.ToList() on cli.id equals comp.Cliente.id
                            join prod in db.Produtoes.ToList() on comp.Produto.id equals prod.id
                            group prod by new { cli.nome, cli.id } into g
                            select new { IdCliente = g.Key.id ,NomeCliente = g.Key.nome, Preco = g.Sum(prod => prod.preco) };

            List<ClienteCompra> cc = new List<ClienteCompra>();

            foreach (var item in clientes)
            {
                cc.Add(new ClienteCompra
                {
                    IdCliente = item.IdCliente,
                    NomeCliente = item.NomeCliente,
                    //NomeProduto = item.NomeProduto,
                    PrecoProduto = item.Preco
                });
            }


            return View("Index", cc);
        }

        public ActionResult ListaClienteEProdutoEspecificoComDivida(int id)
        {
            bool logado = true;
            ViewBag.Logado = logado;
            bool perfil = true;
            ViewBag.Perfil = perfil;

            var clientes = from cli in db.Clientes.ToList()
                           join comp in db.Compras.ToList() on cli.id equals comp.Cliente.id
                           join prod in db.Produtoes.ToList() on comp.Produto.id equals prod.id
                           where cli.id == id
                           select new { NomeCliente = cli.nome, NomeProduto = prod.nome, PrecoProduto = prod.preco };

            List<ClienteCompra> cc = new List<ClienteCompra>();

            foreach (var item in clientes)
            {
                cc.Add(new ClienteCompra
                {
                    //IdCliente = item.IdCliente,
                    NomeCliente = item.NomeCliente,
                    NomeProduto = item.NomeProduto,
                    PrecoProduto = item.PrecoProduto
                });
            }


            return View("ListaClienteEProdutoEspecificoComDivida", cc);
        }
    }
}