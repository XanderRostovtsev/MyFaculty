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
    public class StudentController : Controller
    {
        private readonly MyFacultyStorage _storage;
        public StudentController(MyFacultyStorage storage)
        {
            _storage = storage;
        }

        [Authorize(Roles = "Admin, Student")]
        public IActionResult Index()
        {
            if (User.IsInRole("Student"))
                return RedirectToAction("StudentIndex", "Mark", 
                                        new {
                                            studentId = _storage.GetAllStudents().Where(s => s.Email == User.Identity.Name).FirstOrDefault().Id
                                        });
            var groups = _storage.GetAllGroups();
            List<GroupViewModel> model = groups.Select(g => new GroupViewModel
            {
                Id = g.Id,
                Title = g.Title,
                Students = _storage.GetStudentsByGroupId(g.Id).Select(s => new StudentViewModel
                {
                    Id = s.Id,
                    Email = s.Email,
                    FirstName = s.FirstName,
                    MiddleName = s.MiddleName,
                    LastName = s.LastName,
                    GroupId = s.GroupId
                }).OrderBy(s => s.LastName).ThenBy(s => s.FirstName).ThenBy(s => s.MiddleName).ToList()
            }).OrderBy(g => g.Title).ToList();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create(int groupId)
        {
            var groups = _storage.GetAllGroups();
            ViewBag.Groups = new SelectList(groups, "Id", "Title");
            return View(new StudentViewModel());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(StudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                _storage.AddStudent(new StudentEntity {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    GroupId = model.GroupId
                });
                return RedirectToAction("Index");
            }
            var groups = _storage.GetAllGroups();
            ViewBag.Groups = new SelectList(groups, "Id", "Title");
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(StudentEntity student)
        {
            var groups = _storage.GetAllGroups();
            ViewBag.Groups = new SelectList(groups, "Id", "Title");
            var _student = _storage.GetStudentById(student.Id);
            if (_student != null)
            {
                return View(new StudentViewModel
                {
                    Id = _student.Id,
                    Email = _student.Email,
                    FirstName = _student.FirstName,
                    MiddleName = _student.MiddleName,
                    LastName = _student.LastName,
                    GroupId = _student.GroupId
                });
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(StudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                _storage.UpdateStudent(new StudentEntity
                {
                    Id = model.Id,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    GroupId = model.GroupId
                });
                return RedirectToAction("Index");
            }
            var groups = _storage.GetAllGroups();
            ViewBag.Groups = new SelectList(groups, "Id", "Title");
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(StudentViewModel model)
        {
            if(model.Marks != null)
            {
                foreach(var mark in model.Marks)
                {
                    _storage.DeleteMarkById(mark.Id);
                }
            }
            _storage.DeleteStudentById(model.Id);
            return RedirectToAction("Index");
        }
    }
}