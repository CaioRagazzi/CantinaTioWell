using CantinaTioWell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
                           select new { IdCliente = g.Key.id, NomeCliente = g.Key.nome, Preco = g.Sum(prod => prod.preco) };

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
                           select new { IdCliente = cli.id ,NomeCliente = cli.nome, NomeProduto = prod.nome, PrecoProduto = prod.preco };

            List<ClienteCompra> cc = new List<ClienteCompra>();

            foreach (var item in clientes)
            {
                cc.Add(new ClienteCompra
                {
                    IdCliente = item.IdCliente,
                    NomeCliente = item.NomeCliente,
                    NomeProduto = item.NomeProduto,
                    PrecoProduto = item.PrecoProduto
                });
            }


            return View("ListaClienteEProdutoEspecificoComDivida", cc);
        }

        public ActionResult EnvioDeEmailDeCobranca(int id)
        {

            var clientes = from cli in db.Clientes.ToList()
                           join comp in db.Compras.ToList() on cli.id equals comp.Cliente.id
                           join prod in db.Produtoes.ToList() on comp.Produto.id equals prod.id
                           where cli.id == id
                           select new { IdCliente = cli.id, NomeCliente = cli.nome, NomeProduto = prod.nome, PrecoProduto = prod.preco, EmailCliente = cli.email };

            //Instância classe email
            MailMessage mail = new MailMessage();
            mail.To.Add(clientes.First().EmailCliente); 
            mail.From = new MailAddress("cantinadotiowell@gmail.com");
            mail.Subject = "Cobrança da Cantina do Tio Well";

            mail.Body = $"Olá {clientes.First().NomeCliente}, esté é um e-mail da Cantina Do Tio Well, informamos que você possui dívidas conosco, favor ajustar.";

            mail.IsBodyHtml = true;

            //Instância smtp do servidor, neste caso o gmail.
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential
            ("cantinadotiowell@gmail.com", "cantinatiowell");// Login e senha do e-mail.
            smtp.EnableSsl = true;
            smtp.Send(mail);

            return RedirectToAction("Index");
        }
    }
}