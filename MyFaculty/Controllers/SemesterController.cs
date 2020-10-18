using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Data.Context.Entity;
using MyFaculty.Data.Storage;
using MyFaculty.ViewModels;

namespace MyFaculty.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SemesterController : Controller
    {
        private readonly MyFacultyStorage _storage;
        public SemesterController(MyFacultyStorage storage)
        {
            _storage = storage;
        }
        public IActionResult Index()
        {
            var semesters = _storage.GetAllSemesters();
            List<SemesterViewModel> model = semesters.Select(s => new SemesterViewModel
            {
                Id = s.Id,
                Count = s.Count,
                StartYear = s.StartYear,
                EndYear = s.EndYear
            }).OrderBy(s => s.StartYear).ThenBy(s => s.EndYear).ThenBy(s => s.Count).ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create(int Count, int StartYear, int EndYear)
        {
            return View(new SemesterViewModel());
        }

        [HttpPost]
        public IActionResult Create(SemesterViewModel model)
        {
            if (ModelState.IsValid)
            {
                _storage.AddSemester(new SemesterEntity
                {
                    Count = model.Count,
                    StartYear = model.StartYear,
                    EndYear = model.EndYear
                });
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(SemesterViewModel model)
        {
            var subjects = _storage.GetSubjectsBySemesterId(model.Id);
            foreach(var subject in subjects)
            {
                var marks = _storage.GetMarksBySubjectId(subject.Id);
                foreach(var mark in marks)
                {
                    _storage.DeleteMarkById(mark.Id);
                }
                _storage.DeleteSubjectById(subject.Id);
            }
            _storage.DeleteSemesterById(model.Id);
            return RedirectToAction("Index");
        }
    }
}