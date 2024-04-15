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
            return View(context.Alunos.Include(x => x.Treinos).Include(x => x.Personal));
        }
        public IActionResult IndexPersonal()
        {
            return View(context.Personais);
        }
        public IActionResult IndexExercicio()
        {
            return View(context.Exercicios);
        }

        /*CRUD TREINO*/

        public IActionResult CreateTreinos()
        {
            ViewBag.Alunos = new SelectList(context.Alunos.OrderBy(x => x.Nome), "AlunoId", "Nome");
            ViewBag.Exercicios = new MultiSelectList(context.Exercicios.OrderBy(x => x.Nome), "ExercicioId", "Nome");
            return View();
        }

        [HttpPost]
        public IActionResult CreateTreinos(Treino treino, int[] ExerciciosSelecionados)
        {
            treino.Data = DateTime.Now;
            treino.Exercicios = new List<Exercicio>();
            if (ExerciciosSelecionados != null)
            {
                foreach (var exercicioId in ExerciciosSelecionados)
                {
                    var exercicio = context.Exercicios.Find(exercicioId);
                    if (exercicio != null)
                    {
                        treino.Exercicios.Add(exercicio);
                    }
                }
            }

            context.Treinos.Add(treino);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult DeleteTreinos(int id)
        {
            var treino = context.Treinos.Include(x => x.Aluno).FirstOrDefault(x => x.TreinoId == id);

            var alunoId = treino.AlunoId;

            context.Treinos.Remove(treino);
            context.SaveChanges();
            return RedirectToAction("Index", new { id = alunoId });
        }

        public IActionResult DetailsTreino(int id)
        {
            var treino = context.Treinos.Where(x => x.AlunoId == id).Include(x => x.Aluno).Include(x => x.Exercicios).ToList();
            ViewBag.Aluno = context.Alunos.Find(id).Nome;
            return View(treino);
        }

        public IActionResult EditTreino(int id)
        {
            var treino = context.Treinos.Include(x => x.Aluno).Include(x => x.Exercicios).FirstOrDefault(x => x.TreinoId == id);
            ViewBag.Exercicios = new MultiSelectList(context.Exercicios.OrderBy(x => x.Nome), "ExercicioId", "Nome");

            return View(treino);
        }

        [HttpPost]
        public IActionResult EditTreino(int treinoId, int[] ExerciciosSelecionados)
        {
            var treino = context.Treinos.Include(x => x.Aluno).Include(x => x.Exercicios).FirstOrDefault(x => x.TreinoId == treinoId);
            treino.Exercicios = null;
            context.SaveChanges();
            treino.Exercicios = new List<Exercicio>();
            if (ExerciciosSelecionados != null)
                foreach (var exercicioId in ExerciciosSelecionados)
                {
                    var exercicio = context.Exercicios.Find(exercicioId);
                    if (exercicio != null)
                        treino.Exercicios.Add(exercicio);
                }

            treino.Data = DateTime.Now;
            context.SaveChanges();

            return RedirectToAction("DetailsTreino", new { id = treino.AlunoId });
        }
        /*--------------------------------------------------------------------------------------------------------------------------*/

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
        /*--------------------------------------------------------------------------------------------------------------------------*/

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
        /*--------------------------------------------------------------------------------------------------------------------------*/

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

        /*--------------------------------------------------------------------------------------------------------------------------*/



    }
}
