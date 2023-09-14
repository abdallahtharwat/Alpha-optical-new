﻿using Alpha.DataAccess.Repository.IRepository;
using Alpha.Models.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Alpha_Optical.Areas.Admin.Controllers
{
    [Area("Admin")]
    // authorize for route 
    public class OurClientController : Controller
    {

        private readonly IUnitofWork _UnitofWork;
        private readonly IWebHostEnvironment _WebHostEnvironment;
        public OurClientController(IUnitofWork unitofWork, IWebHostEnvironment webHostEnvironment)
        {
            _UnitofWork = unitofWork;
            _WebHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _UnitofWork.OurClient.GetAllasync());
        }


        public  IActionResult Create()
        {
            return View();
        }

        // post 
        [HttpPost]
        public async Task<IActionResult> Create(OurClient header, IFormFile? file)
        {
            //Validation for sliver side
            if (ModelState.IsValid)
            {
                string wwwRootPath = _WebHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product\");


                    // change photos and delete old photo if Exists
                    if (!string.IsNullOrEmpty(header.Image))
                    {
                        var oldimagepath = Path.Combine(wwwRootPath, header.Image.TrimStart('\\'));
                        if (System.IO.File.Exists(oldimagepath))
                        {
                            System.IO.File.Delete(oldimagepath);
                        }
                    }
                    // change piets to file and  (( Save the photo))
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))  // where to have save the file
                    {
                        file.CopyTo(fileStream);      // that will copy the file in the new location|| that we added  in line 76
                    }

                    header.Image = @"\images\product\" + fileName;
                }


                try
                {
                    var featuretest = await _UnitofWork.OurClient.Addasync(header);
                    if (featuretest)
                    {
                        TempData["success"] = "  Create successfully";
                        return RedirectToAction("Index");
                    }

                }
                catch (Exception)
                {


                }

            }
            return View();
        }



        // get Edit
        public async Task<IActionResult> Edit(int id)
        {

            // how to create edit btton
            if (id == 0)
            {
                return NotFound();
            }

            var featuretest = await _UnitofWork.OurClient.Getasync(id);
            if (featuretest == null)
            {
                return NotFound();
            }
            featuretest.hiddenImageUrl = featuretest.Image;
            return View(featuretest);
        }



        // post Edit
        [HttpPost]
        public async Task<IActionResult> Edit(int id, OurClient header, IFormFile? file)
        {

            if (id != header.Id)
            {
                return NotFound();
            }

            //Validation for sliver side
            if (ModelState.IsValid)
            {

                string wwwRootPath = _WebHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product\");


                    // change photos and delete old photo if Exists
                    if (!string.IsNullOrEmpty(header.Image))
                    {
                        var oldimagepath = Path.Combine(wwwRootPath, header.Image.TrimStart('\\'));
                        if (System.IO.File.Exists(oldimagepath))
                        {
                            System.IO.File.Delete(oldimagepath);
                        }
                    }
                    // change piets to file and  (( Save the photo))
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))  // where to have save the file
                    {
                        file.CopyTo(fileStream);      // that will copy the file in the new location|| that we added  in line 76
                    }

                    header.Image = @"\images\product\" + fileName;
                }

                if (header.Image == null && header.hiddenImageUrl == null)
                {
                    TempData["error"] = "You Need choose Image Again";
                    return RedirectToAction("Edit");
                }

                if (header.Image == null && header.hiddenImageUrl != null)
                {
                    header.Image = header.hiddenImageUrl;
                }



                try
                {
                    var featuretest = await _UnitofWork.OurClient.Updateasync(header);
                    if (featuretest)
                    {
                        TempData["success"] = "  Update successfully";
                        return RedirectToAction("Index");
                    }


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await IsExist(header.Id)))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }

                }

            }
            return View(header);
        }



        public async Task<bool> IsExist(int id)
        {
            return await _UnitofWork.OurClient.HeadersAnyAsync(id);
        }






        public async Task<JsonResult> JsonDelete(int? id)
        {
            if (id == null)
            {
                return Json("Failed");

            }
            var result = await _UnitofWork.OurClient.Deleteasync(id.Value);
            if (result)
            {
                TempData["success"] = "  Removed successfully";
                return Json("Removed");

            }
            return Json("Failed");
        }


        public async Task<JsonResult> JsonDuplicate(int? id)
        {
            if (id == null)
            {
                return Json("Failed");

            }
            var result = await _UnitofWork.OurClient.Duplicateasync(id.Value);
            if (result)
            {

                _UnitofWork.Save();
                TempData["success"] = "  duplicate successfully";
                return Json("Duplicated");
            }
            return Json("Failed");

        }






    }
}