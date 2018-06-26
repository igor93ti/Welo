using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Welo.Domain.Entities.WebApp;
using Welo.Domain.Interfaces.Services.WebApp;
using Welo.WebApp.Models;

namespace Welo.WebApp.Controllers
{
    public class MovieController : Controller
    {
        private IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            this._movieService = movieService;
        }
        // GET: Movie
        public ActionResult Index()
        {
            IEnumerable<MovieModel> model = Mapper.Map<IEnumerable<MovieModel>>(_movieService.GetAll());
            
            
            return View(model);
        }

        // GET: Movie/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        [HttpPost]
        public ActionResult Create(MovieModel model)
        {
            try
            {
                var item = Mapper.Map<Movie>(model);

                _movieService.Add(item);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Movie/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Movie/Edit/5
        [HttpPost]
        public ActionResult Edit(MovieModel model)
        {
            try
            {
                var item = Mapper.Map<Movie>(model);

                _movieService.Update(item);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Movie/Delete/5
        public ActionResult Delete(int id)
        {
            _movieService.Remove(id);
            return View();
        }

        // POST: Movie/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public JsonResult MovieList()
        {
            var listaItens = Mapper.Map<IEnumerable<MovieModel>>(_movieService.GetAll()).Select(x => x.Name).ToList();
            return Json(listaItens,JsonRequestBehavior.AllowGet);
        }
    }
}
