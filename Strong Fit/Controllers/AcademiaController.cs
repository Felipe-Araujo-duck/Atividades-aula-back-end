using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            return View(context.Alunos);
        }

        public IActionResult IndexPersonal()
        {
            return View(context.Personais);
        }

        public IActionResult CreatePersonal()
        {
            return View();
        }
        public IActionResult CreatePersonal(Personal personal) 
        {
            context.Add(personal);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult CreateAlunos()
        {
            return View();
        }
        public IActionResult CreateAlunos(Aluno Aluno)
        {
            context.Add(Aluno);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        
    }
}
