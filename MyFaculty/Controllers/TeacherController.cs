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
    public class TeacherController : Controller
    {
        private readonly MyFacultyStorage _storage;
        public TeacherController(MyFacultyStorage storage)
        {
            _storage = storage;
        }

        [Authorize(Roles = "Admin, Teacher")]
        public IActionResult Index()
        {
            var teachers = _storage.GetAllTeachers();
            List<TeacherViewModel> model = teachers.Select(t => new TeacherViewModel
            {
                Id = t.Id,
                Email = t.Email,
                FirstName = t.FirstName,
                MiddleName = t.MiddleName,
                LastName = t.LastName,
                Grade = t.Grade,
                Subjects = _storage.GetSubjectsByTeacherId(t.Id).Select(s => new SubjectViewModel
                {
                    Id = s.Id,
                    Title = s.Title,
                    TeacherId = s.TeacherId,
                    GroupId = s.GroupId,
                    GroupTitle = _storage.GetGroupById(s.GroupId).Title,
                    SemesterId = s.SemesterId,
                    SemesterCount = _storage.GetSemesterById(s.SemesterId).Count
                }).OrderBy(s => s.SemesterCount).ThenBy(s => s.GroupTitle).ToList()
            }).OrderBy(t => t.LastName).ThenBy(t => t.FirstName).ThenBy(t => t.LastName).ToList();
            if (User.IsInRole("Teacher")) model = model.Where(t => t.Email == User.Identity.Name).ToList();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View(new TeacherViewModel());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(TeacherViewModel model)
        {
            if (ModelState.IsValid)
            {
                _storage.AddTeacher(new TeacherEntity
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    Grade = model.Grade
                });
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(TeacherEntity teacher)
        {
            var _teacher = _storage.GetTeacherById(teacher.Id);
            if (_teacher != null)
            {
                return View(new TeacherViewModel
                {
                    Id = _teacher.Id,
                    Email = _teacher.Email,
                    FirstName = _teacher.FirstName,
                    MiddleName = _teacher.MiddleName,
                    LastName = _teacher.LastName,
                    Grade = _teacher.Grade
                });
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(TeacherViewModel model)
        {
            if (ModelState.IsValid)
            {
                _storage.UpdateTeacher(new TeacherEntity
                {
                    Id = model.Id,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    Grade = model.Grade
                });
                return RedirectToAction("Index");
            }
            var groups = _storage.GetAllGroups();
            ViewBag.Groups = new SelectList(groups, "Id", "Title");
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(TeacherViewModel model)
        {
            if(model.Subjects != null)
            {
                foreach(var subject in model.Subjects)
                {
                    _storage.DeleteSubjectById(subject.Id);
                }
            }
            _storage.DeleteTeacherById(model.Id);
            return RedirectToAction("Index");
        }
    }
}