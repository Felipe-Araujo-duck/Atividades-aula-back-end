using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Strong_Fit.Models;

namespace Strong_Fit.Controllers
{
    public class AcademiaController : Controller
    {
        public Context context;

        public AcademiaController (Context context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View(context.Alunos.Include(p => p.Personal));
        }

        public IActionResult IndexPersonal()
        {
            return View(context.Personais);
        }

        public IActionResult IndexTreino()
        {
            return View(context.Treinos.Include(a => a.Aluno));
        }

        public IActionResult IndexExercicio()
        {
            return View(context.Exercicios);
        }

        /*CRUD TREINO*/

        public IActionResult CreateTreino()
        {
            ViewBag.AlunoId = new SelectList(context.Alunos.OrderBy(a => a.Nome), "AlunoId", "Nome");
            ViewBag.ExercicioId = new SelectList(context.Exercicios.OrderBy(e => e.Nome), "ExercicioId", "Nome");
            return View();
        }

        [HttpPost]
        public IActionResult CreateTreino(Treino treino)
        {
            context.Add(treino);
            context.SaveChanges();
            return RedirectToAction("IndexTreino");
        }

        public IActionResult EditTreino(int id)
        {
            var treino = context.Treinos.Find(id);
            ViewBag.AlunoId = new SelectList(context.Alunos.OrderBy(p => p.Nome), "AlunoId", "Nome");
            return View(treino);
        }

        [HttpPost]
        public IActionResult EditTreino(Treino treino)
        {
            context.Entry(treino).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("IndexTreino");
        }

        public IActionResult DetailsTreino(int id)
        {
            var treino = context.Treinos.Include(p => p.Aluno).FirstOrDefault(p => p.AlunoId == id);
            return View(treino);
        }

        public IActionResult DeleteTreino(int id)
        {
            var treino = context.Treinos.Find(id);
            ViewBag.AlunoId = new SelectList(context.Alunos.OrderBy(p => p.Nome), "AlunoId", "Nome");
            return View(treino);
        }

        [HttpPost]
        public IActionResult DeleteTreino(Treino treino)
        {
            context.Treinos.Remove(treino);
            context.SaveChanges();
            return RedirectToAction("IndexTreino");
        }


        /*CRUD EXERCICIO*/

        public IActionResult CreateExercicio()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateExercicio(Exercicio exercicio)
        {
            context.Add(exercicio);
            context.SaveChanges();
            return RedirectToAction("IndexExercicio");
        }

        public IActionResult EditExercicio(int id)
        {
            var exercicio = context.Exercicios.Find(id);
            return View(exercicio);
        }

        [HttpPost]
        public IActionResult EditExercicio(Exercicio exercicio)
        {
            context.Entry(exercicio).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("IndexExercicio");
        }

        public IActionResult DetailsExercicio(int id)
        {
            var exercicio = context.Exercicios.Find(id);
            return View(exercicio);
        }

        public IActionResult DeleteExercicio(int id)
        {
            var exercicio = context.Exercicios.Find(id);
            return View(exercicio);
        }

        [HttpPost]
        public IActionResult DeleteExercicio(Exercicio exercicio)
        {
            context.Exercicios.Remove(exercicio);
            context.SaveChanges();
            return RedirectToAction("IndexExercicio");
        }

        /*CRUD PERSONAL*/
        public IActionResult CreatePersonal()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePersonal(Personal personal) 
        {
            context.Add(personal);
            context.SaveChanges();
            return RedirectToAction("IndexPersonal");
        }

        public IActionResult EditPersonal(int id)
        {
            var personal = context.Personais.Find(id);
            return View(personal);
        }

        [HttpPost]
        public IActionResult EditPersonal(Personal personal)
        {
            context.Entry(personal).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("IndexPersonal");
        }

        public IActionResult DetailsPersonal(int id)
        {
            var personal = context.Personais.Find(id);
            return View(personal);
        }

        public IActionResult DeletePersonal(int id)
        {
            var personal = context.Personais.Find(id);
            return View(personal);
        }

        [HttpPost]
        public IActionResult DeletePersonal(Personal personal)
        {
            context.Personais.Remove(personal);
            context.SaveChanges();
            return RedirectToAction("IndexPersonal");
        }

        /*CRUD ALUNOS*/
        public IActionResult CreateAlunos()
        {
            ViewBag.PersonalId = new SelectList(context.Personais.OrderBy(p => p.Nome), "PersonalId", "Nome"); 
            return View();
        }

        [HttpPost]
        public IActionResult CreateAlunos(Aluno Aluno)
        {
            context.Add(Aluno);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult EditAluno(int id) 
        {
            var aluno = context.Alunos.Find(id);
            ViewBag.PersonalId = new SelectList(context.Personais.OrderBy(p => p.Nome), "PersonalId", "Nome");
            return View(aluno);
        }

        [HttpPost]
        public IActionResult EditAluno(Aluno aluno)
        {
            context.Entry(aluno).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DetailsAlunos(int id)
        {
            var aluno = context.Alunos.Include(p => p.Personal).FirstOrDefault(p => p.PersonalId == id);
            return View(aluno);
        }

        public IActionResult DeleteAluno(int id)
        {
            var aluno = context.Alunos.Find(id);
            ViewBag.PersonalId = new SelectList(context.Personais.OrderBy(p => p.Nome), "PersonalId", "Nome");
            return View(aluno);
        }

        [HttpPost]
        public IActionResult DeleteAluno(Aluno aluno)
        {
            context.Alunos.Remove(aluno);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        /*CRUD VINCULO TREINO COM EXERCICIO*/



    }
}
