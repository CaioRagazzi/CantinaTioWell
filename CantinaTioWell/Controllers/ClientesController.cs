using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CantinaTioWell.Models;

namespace CantinaTioWell.Controllers
{
    public class ClientesController : Controller
    {
        private CantinaTioWellContext db = new CantinaTioWellContext();

        public ActionResult Index()
        {
            return VerificaSeEstaLogadoEPerfil();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        public ActionResult Create()
        {
            var perfis = new List<Perfil>();

            Perfil adm = new Perfil
            {
                Id = 1,
                Nome = "admin"
            };
            Perfil user = new Perfil
            {
                Id = 2,
                Nome = "Cliente"
            };
            perfis.Add(adm);
            perfis.Add(user);

            ViewBag.Perfis = new SelectList(perfis, "Id", "Nome");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cliente cliente)
        {            
            if (ModelState.IsValid)
            {
                db.Clientes.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nome,email,telefone,cpf")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            db.Clientes.Remove(cliente);
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

        private ActionResult VerificaSeEstaLogadoEPerfil()
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
                bool logado = true;
                ViewBag.Logado = logado;
                return View(db.Clientes.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
