﻿using DRK.ProgDec.UI.Models;
using DRK.ProgDec.UI.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DRK.ProgDec.UI.Controllers
{
    public class ProgramController : Controller
    {
        private readonly IWebHostEnvironment _host;

        public ProgramController(IWebHostEnvironment host)
        {
            _host = host;
        }




        public IActionResult Index()
        {
            ViewBag.Title = "List of all Programs";
            return View(ProgramManager.Load());
        }

        public IActionResult Details(int id)
        {
            var item = ProgramManager.LoadById(id);
            ViewBag.Title = "Details for " + item.Description;
            return View(item);
        }

        public IActionResult Create()
        {

            ViewBag.Title = "create program";

            ProgramVM programVM = new ProgramVM();

            programVM.Program = new BL.Models.Program();

            programVM.DegreeTypes = DegreeTypeManager.Load();



            if (Authenticate.isAuthenticated(HttpContext))
            {
                return View(programVM);

            }
            else
            {
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
        }

        [HttpPost]

        public IActionResult Create(ProgramVM programVM)
        {
            try
            {
                int result = ProgramManager.Insert(programVM.Program);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }

        }

        public IActionResult Edit(int id)
        {
            if (Authenticate.isAuthenticated(HttpContext))
            {

                ProgramVM programVM = new ProgramVM();

                programVM.Program = ProgramManager.LoadById(id);

                programVM.DegreeTypes = DegreeTypeManager.Load();

                ViewBag.Title = "edit " + programVM.Program.Description;



                return View(programVM);

            }
            else
            {
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }

        }

        [HttpPost]

        public IActionResult Edit(int id, ProgramVM programVM, bool rollback = false)
        {
            try
            {
                // process image

                if (programVM.File != null)
                {
                    programVM.Program.ImagePath = programVM.File.FileName;

                    string path = _host.WebRootPath + "\\images\\";

                    using (var stream = System.IO.File.Create(path = programVM.File.FileName))
                    {
                        programVM.File.CopyTo(stream);
                        ViewBag.Message = "File uploaded sucessfully";
                    }

                }


                int result = ProgramManager.Update(programVM.Program, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(programVM);
            }
        }

        public IActionResult Delete(int id)
        {
            var item = ProgramManager.LoadById(id);
            ViewBag.Title = "Delete";
            return View(item);

        }

        [HttpPost]

        public IActionResult Delete(int id, BL.Models.Program program, bool rollback = false)
        {
            try
            {
                int result = ProgramManager.Delete(id, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(program);
            }
        }



    }

}
