using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabalhoAluno.Data;
using TrabalhoAluno.Models;

namespace TrabalhoAluno.Controllers
{
    public class AlunoController : Controller
    {
        private readonly AppCont _appCont;
        private object cmd;

        public AlunoController(AppCont appCont)
        {
            _appCont = appCont;
        }

        public IActionResult Index()
        {
            var allTasks = _appCont.Aluno.ToList();
            return View(allTasks);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Nome, Email, Logradouro, Bairro, Cidade, UF")] Aluno aluno)
        {

            var nome = BCrypt.Net.BCrypt.HashString(aluno.Nome);
            var Email = BCrypt.Net.BCrypt.HashString(aluno.Email);
            var Logradouro = BCrypt.Net.BCrypt.HashString(aluno.Logradouro);
            var Bairro = BCrypt.Net.BCrypt.HashString(aluno.Bairro);
            var Cidade = BCrypt.Net.BCrypt.HashString(aluno.Cidade);
            var UF = BCrypt.Net.BCrypt.HashString(aluno.UF);


            if (ModelState.IsValid)
            {
                _appCont.Add(aluno);
                await _appCont.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? Id)
        {
                     

            if (Id == null)
            {
                return NotFound();
            }

            var aluno = await _appCont.Aluno.FindAsync(Id);

            if (aluno == null)
            {
                return BadRequest();
            }
            return View(aluno);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var aluno = await _appCont.Aluno.FindAsync(Id);
            if (aluno == null)
            {
                return BadRequest();
            }
            return View(aluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? Id, Aluno aluno)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var dadosAntigos = _appCont.Aluno.AsNoTracking().FirstOrDefault(a => a.Id == Id);

            if (ModelState.IsValid)
            {
                _appCont.Update(aluno);
                await _appCont.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var aluno = await _appCont.Aluno.FindAsync(Id);
            if (aluno == null)
            {
                return BadRequest();
            }
            return View(aluno);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? Id)
        {
            var aluno = await _appCont.Aluno.FindAsync(Id);
            _appCont.Aluno.Remove(aluno);
            await _appCont.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
