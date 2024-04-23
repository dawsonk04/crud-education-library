using Microsoft.AspNetCore.Mvc;

namespace DRK.ProgDec.UI.Controllers
{
    public class DeclarationController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "List of declarations";
            return View(DeclarationManager.Load());
        }
        // use in checkpoint 5
        // filter the declaration by programId
        public IActionResult Browse(int id)
        {
            var results = ProgramManager.LoadById(id);
            ViewBag.Title = "List of " + results;
            return View(nameof(Index), DeclarationManager.Load(id));
        }

        public IActionResult Details(int id)
        {
            var item = DeclarationManager.LoadById(id);
            ViewBag.Title = "Details";
            return View(item);
        }

        public IActionResult Create()
        {
            ViewBag.Title = "Create";
            return View();
        }

        [HttpPost]

        public IActionResult Create(Declaration declaration)
        {
            try
            {
                int result = DeclarationManager.Insert(declaration);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }

        }

        public IActionResult Edit(int id)
        {
            var item = DeclarationManager.LoadById(id);
            ViewBag.Title = "Edit";
            return View(item);

        }

        [HttpPost]

        public IActionResult Edit(int id, Declaration declaration, bool rollback = false)
        {
            try
            {
                int result = DeclarationManager.Update(declaration, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(declaration);
            }
        }

        public IActionResult Delete(int id)
        {
            var item = DeclarationManager.LoadById(id);
            ViewBag.Title = "Delete";
            return View(item);

        }

        [HttpPost]

        public IActionResult Delete(int id, Declaration declaration, bool rollback = false)
        {
            try
            {
                int result = DeclarationManager.Delete(id, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(declaration);
            }
        }



    }

}
