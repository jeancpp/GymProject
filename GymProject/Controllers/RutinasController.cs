using GymProject.Data;
using GymProject.Models.Domain;
using GymProject.Models.ViewModels;
using GymProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;

namespace GymProject.Controllers
{
    [Authorize (Roles = "Admin, Entrenador")]
    public class RutinasController : Controller
    {
        private readonly IEjerciciosRepository ejerciciosRepository;
        private readonly ICategoriasRepository categoriasRepository;
        private readonly IRutinasRepository rutinasRepository;

        public RutinasController(IEjerciciosRepository ejerciciosRepository, ICategoriasRepository categoriasRepository, IRutinasRepository rutinasRepository)
        {
            this.ejerciciosRepository = ejerciciosRepository;
            this.categoriasRepository = categoriasRepository;
            this.rutinasRepository = rutinasRepository;
        }
        public async Task<IActionResult> Index(string? searchQuery, DateTime? searchDate, int pageSize = 9, int pageNumber = 1)
        {
            ViewBag.SearchQuery = searchQuery;
            if (searchDate != null)
            {
                ViewBag.SearchDate = searchDate.Value.ToString("yyyy-MM-dd");
            }
            var registros = await rutinasRepository.CountAsync();
            var ejercicios = await ejerciciosRepository.GetAllAsync();

            var totalPaginas = Math.Ceiling((decimal)registros / pageSize);
            ViewBag.totalPaginas = totalPaginas;
            ViewBag.pageNumber = pageNumber;

            if (pageNumber > totalPaginas)
            {
                pageNumber--;
            }

            if (pageNumber < 1)
            {
                pageNumber++;
            }

            var rutinas = await rutinasRepository.GetAllAsync(searchQuery, searchDate, pageNumber, pageSize);

            var model = new RutinasVM
            {
                ListaEjercicios = ejercicios.Select(e => new Ejercicio
                {
                    IdEjercicio = e.IdEjercicio,
                    Nombre = e.Nombre
                }).ToList(),

                Rutinas = rutinas.Select(r => new Rutinas
                {
                    IdRutina = r.IdRutina,
                    Nombre = r.Nombre,
                    Descripcion = r.Descripcion,
                    Sets = r.Sets.Select(s => new Sets
                    {
                        IdSet = s.IdSet,
                        Nombre = s.Nombre,
                        Series = s.Series,
                        SetEjercicios = s.SetEjercicios.Select(se => new SetEjercicios
                        {
                            Ejercicio = se.Ejercicio,
                            Repeticiones = se.Repeticiones
                        }).ToList(),
                    }).ToList(),
                }).ToList(),

            };
            return View(model);
        }

        [HttpPost]
        [ActionName("Index")]
        public async Task<IActionResult> Index(RutinasVM Rutina)
        {
            if (ModelState.IsValid) {
                var rutina = new Rutinas
                {
                    Nombre = Rutina.Nombre,
                    Descripcion = Rutina.Descripcion,
                    FechaCreacion = DateTime.Now,
                    Sets = new List<Sets>()
                };

                foreach (var setCreado in Rutina.Sets)
                {
                    var set = new Sets
                    {
                        Nombre = setCreado.Nombre,
                        Series = setCreado.Series,
                        SetEjercicios = new List<SetEjercicios>()
                    };

                    foreach (var ejercicioCreado in setCreado.Ejercicios)
                    {
                        var ejercicio = new SetEjercicios
                        {
                            IdEjercicio = ejercicioCreado.IdEjercicio,
                            Repeticiones = ejercicioCreado.Repeticiones
                        };
                        set.SetEjercicios.Add(ejercicio);
                    }
                    rutina.Sets.Add(set);
                }
                await rutinasRepository.AddAsync(rutina);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task <IActionResult> Delete(RutinasVM Rutina)
        {
            var rutinaEliminada = await rutinasRepository.DeleteAsync(Rutina.Id);

            if(rutinaEliminada != null)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditarRutina(RutinasVM Rutina)
        {
           
            return RedirectToAction("Index");
        }
    }
}
