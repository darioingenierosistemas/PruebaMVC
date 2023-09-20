using Microsoft.AspNetCore.Mvc;
using MVC.DAOs;
using MVC.Models;
using Newtonsoft.Json;
using System.Text;

namespace MVC.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuariosDAO _usuariosDAO;

        public UsuariosController(IUsuariosDAO usuariosDAO)
        {
            _usuariosDAO = usuariosDAO;
        }

        public async Task<IActionResult> Listar()
        {
            var usuarios = await _usuariosDAO.ListarTodosAsync();
            return View(usuarios);
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Usuarios usuario)
        {
            await _usuariosDAO.GuardarAsync(usuario);
            return RedirectToAction("Listar");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var usuario = await _usuariosDAO.ObtenerPorIdAsync(id);
            return View("Crear", usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Usuarios usuario)
        {
            await _usuariosDAO.EditarAsync(usuario);
            return RedirectToAction("Listar");
        }

        [HttpGet]
        public async Task<IActionResult> Borrar(int id)
        {
            await _usuariosDAO.EliminarAsync(id);
            return RedirectToAction("Listar");
        }
    }

}
