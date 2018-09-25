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
            if (Session["user"] != null)
            {
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

            var clientes = from cli in db.Clientes.ToList()
                           join comp in db.Compras.ToList() on cli.id equals comp.Cliente.id
                           join prod in db.Produtoes.ToList() on comp.Produto.id equals prod.id
                           select new { NomeCliente = cli.nome, NomeProduto = prod.nome, PrecoProduto = prod.preco };

            //var clientes2 = from cli in db.Clientes.ToList()
            //                join comp in db.Compras.ToList() on cli.id equals comp.Cliente.id
            //                join prod in db.Produtoes.ToList() on comp.Produto.id equals prod.id
            //                group prod by new { cli.nome } into g
            //                select new { NomeCliente = g.Key.nome, Preco = g.Sum(prod => prod.preco) };

            List<ClienteCompra> cc = new List<ClienteCompra>();

            foreach (var item in clientes)
            {
                cc.Add(new ClienteCompra
                {
                    NomeCliente = item.NomeCliente,
                    NomeProduto = item.NomeProduto,
                    PrecoProduto = item.PrecoProduto
                });
            }


            return View("Index", cc);
        }
    }
}