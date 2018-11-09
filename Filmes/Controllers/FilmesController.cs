using Filmes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Filmes.Controllers
{
    public class FilmesController : Controller
    {
        public ActionResult Index()
        {
            ComandosBancoDeDados comandos = new ComandosBancoDeDados();
            var lista = comandos.BuscarFilmes();
            return View(lista);
        }
        public ActionResult Editar(Guid FilmeId)
        {
            ComandosBancoDeDados comandos = new ComandosBancoDeDados();
            var filmes = comandos.BuscarFilmePorId(FilmeId);
            return View(filmes);
        }
        public ActionResult Deletar(Guid FilmeId)
        {
            ComandosBancoDeDados comandos = new ComandosBancoDeDados();
            comandos.ExcluirFilme(FilmeId);
            return RedirectToAction("Index");
        }
        public ActionResult SalvarFilme(Models.Filmes filmes)
        {
            ComandosBancoDeDados comandos = new ComandosBancoDeDados();
            comandos.SalvarFilme(filmes);
            return RedirectToAction("Index");
        }
        public ActionResult CriarFilme()
        {
            return View();
        }
        public ActionResult SalvarFilmeCriado(Models.Filmes filmes)
        {
            ComandosBancoDeDados comandos = new ComandosBancoDeDados();
            comandos.InserirFilme(filmes);
            return RedirectToAction("Index");
        }     
    }
}