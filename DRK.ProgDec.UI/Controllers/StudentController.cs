using Microsoft.AspNetCore.Mvc;

namespace DRK.ProgDec.UI.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "List of students";
            return View(StudentManager.Load());
        }

        public IActionResult Details(int id)
        {
            var item = StudentManager.LoadById(id);
            ViewBag.Title = "Details for " + item.FullName;

            return View(StudentManager.LoadById(id));
        }

        public IActionResult Create()
        {
            ViewBag.Title = "Create a student";
            return View();
        }

        [HttpPost]

        public IActionResult Create(Student student)
        {
            try
            {
                int result = StudentManager.Insert(student);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Create a program";
                ViewBag.Error = ex.Message;
                throw;
            }

        }

        public IActionResult Edit(int id)
        {


            return View(StudentManager.LoadById(id));

        }

        [HttpPost]

        public IActionResult Edit(int id, Student student, bool rollback = false)
        {
            try
            {
                int result = StudentManager.Update(student, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(student);
            }
        }

        public IActionResult Delete(int id)
        {
            return View(StudentManager.LoadById(id));

        }

        [HttpPost]

        public IActionResult Delete(int id, Student student, bool rollback = false)
        {
            try
            {
                int result = StudentManager.Delete(id, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(student);
            }
        }



    }

}
