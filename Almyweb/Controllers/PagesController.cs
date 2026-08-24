using Microsoft.AspNetCore.Mvc;

namespace Almyweb.Controllers
{
    public class PagesController : Controller
    {
        public IActionResult MyLearn()
        {
            return View();
        }
        public IActionResult Instructor()
        {
            return View();
        }
        public IActionResult CourseDetail(int id)
        {
            ViewBag.CourseId = id;
            return View();
        }
        public IActionResult Exam()
        {
            return View();
        }
    }
}
