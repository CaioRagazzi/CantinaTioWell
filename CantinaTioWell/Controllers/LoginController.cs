using CantinaTioWell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CantinaTioWell.Controllers
{
    public class LoginController : Controller
    {
        private CantinaTioWellContext db = new CantinaTioWellContext();

        public ActionResult Index()
        {
            bool logado = false;
            ViewBag.Logado = logado;
            return View();
        }

        public ActionResult Logar(string nome, string senha)
        {
            var clientes = from cli in db.Clientes.ToList()
                           where cli.nome == nome && cli.Senha == senha
                           select cli;

            if (clientes.Any())
            {
                Cliente clienteLogando = new Cliente();
                clienteLogando = clientes.First();

                if (clienteLogando.Perfil == 2)
                {
                    Session["user"] = nome;
                    Response.Cookies.Add(new HttpCookie("MyCookie", Convert.ToString(clienteLogando.id)));
                    return RedirectToAction("Index", "Compras");
                }
                else
                {
                    Session["user"] = nome;
                    Response.Cookies.Add(new HttpCookie("MyCookie", Convert.ToString(clienteLogando.id)));
                    return RedirectToAction("Index", "Administrativo");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Deslogar()
        {
            if (Request.Cookies["MyCookie"] != null)
            {
                var cookie = new HttpCookie("MyCookie");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }

            ViewBag.Logado = false;
            Session["user"] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}