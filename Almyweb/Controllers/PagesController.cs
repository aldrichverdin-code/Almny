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
        public IActionResult CourseManagement(int id)
        {
            ViewBag.CourseId = id;
            return View();
        }
        public IActionResult ViewExam(string student, string exam)
        {
            ViewBag.StudentName = student ?? "John Doe";
            ViewBag.ExamName = exam ?? "Final Exam";
            return View();
        }

        public IActionResult GradeExam(string student, string exam)
        {
            ViewBag.StudentName = student ?? "John Doe";
            ViewBag.ExamName = exam ?? "Final Exam";
            return View();
        }
        public IActionResult ExamStaff()
        {
            return View();
        }
    }
}
