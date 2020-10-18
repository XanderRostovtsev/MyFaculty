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
    public class GroupController : Controller
    {
        private readonly MyFacultyStorage _storage;
        public GroupController(MyFacultyStorage storage)
        {
            _storage = storage;
        }
        public IActionResult Index(int? groupId)
        {
            if (groupId == null) return RedirectToAction("Index", "Student");
            var group = _storage.GetGroupById(groupId.Value);
            GroupViewModel model = new GroupViewModel
            {
                Id = group.Id,
                Title = group.Title,
                Students = _storage.GetStudentsByGroupId(group.Id).Select(s => new StudentViewModel
                {
                    Id = s.Id,
                    Email = s.Email,
                    FirstName = s.FirstName,
                    MiddleName = s.MiddleName,
                    LastName = s.LastName,
                    GroupId = s.GroupId
                }).OrderBy(s => s.LastName).ThenBy(s => s.FirstName).ThenBy(s => s.MiddleName).ToList()
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new GroupViewModel());
        }

        [HttpPost]
        public IActionResult Create(GroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                _storage.AddGroup(new GroupEntity
                {
                    Title = model.Title
                });
                return RedirectToAction("Index");
            }            
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(GroupEntity group)
        {
            var _group = _storage.GetGroupById(group.Id);
            if (_group != null)
            {
                return View(new GroupViewModel
                {
                    Title = _group.Title
                });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(GroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                _storage.UpdateGroup(new GroupEntity
                {
                    Id = model.Id,
                    Title = model.Title
                });
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(GroupViewModel model)
        {
            if (model.Students != null)
            {
                foreach (var student in model.Students)
                {
                    _storage.DeleteStudentById(student.Id);
                }
            }
            _storage.DeleteGroupById(model.Id);
            return RedirectToAction("Index");
        }
    }
}