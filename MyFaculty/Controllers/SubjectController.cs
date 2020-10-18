using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyFaculty.Data.Context.Entity;
using MyFaculty.Data.Storage;
using MyFaculty.ViewModels;

namespace MyFaculty.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SubjectController : Controller
    {
        private readonly MyFacultyStorage _storage;

        public SubjectController(MyFacultyStorage storage)
        {
            _storage = storage;
        }
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Teacher");
        }

        [HttpGet]
        public IActionResult Create(int? teacherId, int? groupId, int? semesterId)
        {
            if (_storage.GetAllSemesters().Count == 0) return RedirectToAction("Create", "Semester", new { Count = 1, StartYear = DateTime.Now.Year, EndYear = DateTime.Now.Year });
            if (_storage.GetAllGroups().Count == 0) return RedirectToAction("Create", "Group");
            var teachers = _storage.GetAllTeachers();
            ViewBag.Teachers = new SelectList(teachers.Select(t => new
            {
                Id = t.Id,
                LFM = string.Join(' ', t.LastName, t.FirstName, t.MiddleName)
            }), "Id", "LFM");
            var semesters = _storage.GetAllSemesters();
            ViewBag.Semesters = new SelectList(semesters.Select(s => new
            {
                Id = s.Id,
                Title = string.Join(' ', s.Count, s.StartYear, '-', s.EndYear)
            }), "Id", "Title");
            var groups = _storage.GetAllGroups();
            ViewBag.Groups = new SelectList(groups, "Id", "Title");
            return View(new SubjectViewModel());
        }

        [HttpPost]
        public IActionResult Create(SubjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                _storage.AddSubject(new SubjectEntity
                {
                    SemesterId = model.SemesterId,
                    GroupId = model.GroupId,
                    Title = model.Title,
                    TeacherId = model.TeacherId,
                });
                return RedirectToAction("Index");
            }
            var teachers = _storage.GetAllTeachers();
            ViewBag.Teachers = new SelectList(teachers.Select(t => new
            {
                Id = t.Id,
                LFM = string.Join(' ', t.LastName, t.FirstName, t.MiddleName)
            }), "Id", "LFM");
            var semesters = _storage.GetAllSemesters();
            ViewBag.Semesters = new SelectList(semesters.Select(s => new
            {
                Id = s.Id,
                Title = string.Join(' ', s.Count, s.StartYear, '-', s.EndYear)
            }), "Id", "Title");
            var groups = _storage.GetAllGroups();
            ViewBag.Groups = new SelectList(groups, "Id", "Title");
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(SubjectEntity subject)
        {
            var teachers = _storage.GetAllTeachers();
            ViewBag.Teachers = new SelectList(teachers.Select(t => new
            {
                Id = t.Id,
                LFM = string.Join(' ', t.LastName, t.FirstName, t.MiddleName)
            }), "Id", "LFM");
            var semesters = _storage.GetAllSemesters();
            ViewBag.Semesters = new SelectList(semesters.Select(s => new
            {
                Id = s.Id,
                Title = string.Join(' ', s.Count, s.StartYear, '-', s.EndYear)
            }), "Id", "Title");
            var groups = _storage.GetAllGroups();
            ViewBag.Groups = new SelectList(groups, "Id", "Title");
            var _subject = _storage.GetSubjectById(subject.Id);
            if (_subject != null)
            {
                return View(new SubjectViewModel
                {
                    Id = _subject.Id,
                    Title = _subject.Title,
                    TeacherId = _subject.TeacherId,
                    GroupId = _subject.GroupId,
                    GroupTitle = _storage.GetGroupById(_subject.GroupId).Title,
                    SemesterId = _subject.SemesterId,
                    SemesterCount = _storage.GetSemesterById(_subject.SemesterId).Count
                });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(SubjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                _storage.UpdateSubject(new SubjectEntity
                {
                    Id = model.Id,
                    SemesterId = model.SemesterId,
                    GroupId = model.GroupId,
                    Title = model.Title,
                    TeacherId = model.TeacherId
                });
                return RedirectToAction("Index");
            }
            var teachers = _storage.GetAllTeachers();
            ViewBag.Teachers = new SelectList(teachers.Select(t => new
            {
                Id = t.Id,
                LFM = string.Join(' ', t.LastName, t.FirstName, t.MiddleName)
            }), "Id", "LFM");
            var semesters = _storage.GetAllSemesters();
            ViewBag.Semesters = new SelectList(semesters.Select(s => new
            {
                Id = s.Id,
                Title = string.Join(' ', s.Count, s.StartYear, '-', s.EndYear)
            }), "Id", "Title");
            var groups = _storage.GetAllGroups();
            ViewBag.Groups = new SelectList(groups, "Id", "Title");
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(SubjectViewModel model)
        {
            if(model.Marks != null)
            {
                foreach(var mark in model.Marks)
                {
                    _storage.DeleteMarkById(mark.Id);
                }
            }
            _storage.DeleteSubjectById(model.Id);
            return RedirectToAction("Index");
        }

    }
}