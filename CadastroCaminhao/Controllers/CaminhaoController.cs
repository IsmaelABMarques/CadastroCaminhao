using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CadastroCaminhao.Models.Entity;
using CadastroCaminhao.Models.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using CadastroCaminhao.Services;

namespace CadastroCaminhao.Controllers
{
    public class CaminhaoController : Controller
    {
        private readonly CaminhaoService _service;

        public CaminhaoController(CaminhaoService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {            
            return View(await _service.GetAll());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Caminhao = await _service.Get(id);

            if (Caminhao == null)
            {
                return NotFound();
            }

            return View(Caminhao);
        }

        public async Task<IActionResult> Create()
        {
            CadastroCaminhaoModel cadastro = new CadastroCaminhaoModel();
            var modelos = await _service.GetAllModelo();
            cadastro.Modelos = modelos.ToList().Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Descricao
            })
            .ToList();

            return View(cadastro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AnoFabricacao,AnoModelo,ModeloId")] Caminhao Caminhao)
        {
            if (ModelState.IsValid)
            {
                await _service.Insert(Caminhao);
                return RedirectToAction(nameof(Index));
            }
            return View(Caminhao);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            CadastroCaminhaoModel cadastro = new CadastroCaminhaoModel();
            var modelos = await _service.GetAllModelo();

            if (id == null)
            {
                return NotFound();
            }

            cadastro.Caminhao = await _service.Get(id);
            cadastro.Modelos = modelos.ToList().Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Descricao
            })
            .ToList();

            if (cadastro.Caminhao == null)
            {
                return NotFound();
            }
            return View(cadastro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AnoFabricacao,AnoModelo,ModeloId")] Caminhao Caminhao)
        {
            if (id != Caminhao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.Update(Caminhao);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_service.Exists(Caminhao.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Caminhao);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Caminhao = await _service.Get(id);
            if (Caminhao == null)
            {
                return NotFound();
            }

            Caminhao.Modelo = await _service.GetModelo(Caminhao.ModeloId);
           
            return View(Caminhao);
        }

        // POST: Caminhao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
