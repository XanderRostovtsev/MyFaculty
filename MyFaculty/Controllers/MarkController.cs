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
    public class MarkController : Controller
    {
        private readonly MyFacultyStorage _storage;
        public MarkController(MyFacultyStorage storage)
        {
            _storage = storage;
        }
        public IActionResult Index() => NotFound();

        [Authorize(Roles = "Admin, Teacher")]
        public IActionResult GroupIndex(int groupId, int subjectId)
        {
            if (_storage.GetStudentsByGroupId(groupId).Count == 0) return RedirectToAction("Create", "Student", new { groupId = groupId });
            var group = _storage.GetGroupById(groupId);
            var subject = _storage.GetSubjectById(subjectId);
            ViewBag.Subject = new SubjectViewModel
            {
                Id = subject.Id,
                Title = subject.Title,
                TeacherId = subject.TeacherId,
                GroupId = subject.GroupId,
                GroupTitle = _storage.GetGroupById(subject.GroupId).Title,
                SemesterId = subject.SemesterId,
                SemesterCount = _storage.GetSemesterById(subject.SemesterId).Count
            };
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
                    Subjects = _storage.GetSubjectsByGroupId(s.GroupId).Select(ss => new SubjectViewModel
                    {
                        Id = ss.Id,
                        Title = ss.Title,
                        SemesterCount = ss.SemesterId  
                    }).OrderBy(ss => ss.SemesterCount).ThenBy(ss => ss.Title).ToList(),
                    Marks = _storage.GetMarksByStudentId(s.Id).Where(m => m.SubjectId == subjectId).Select(m => new MarkViewModel
                    {
                        Id = m.Id,
                        Mark = m.Mark.GetValueOrDefault(),
                        SubjectId = m.SubjectId,
                        StudentId = s.Id,
                        Date = m.Date
                    }).ToList()
                }).OrderBy(s => s.LastName).ThenBy(s => s.FirstName).ThenBy(s => s.MiddleName).ToList()
            };
            return View(model);
        }

        [Authorize(Roles = "Admin, Student")]
        public IActionResult StudentIndex(int studentId)
        {
            var student = _storage.GetStudentById(studentId);
            StudentViewModel model = new StudentViewModel
            {
                Id = student.Id,
                Email = student.Email,
                FirstName = student.FirstName,
                MiddleName = student.MiddleName,
                LastName = student.LastName,
                Subjects = _storage.GetSubjectsByGroupId(student.GroupId).Select(ss => new SubjectViewModel
                {
                    Id = ss.Id,
                    Title = ss.Title,
                    TeacherId = ss.TeacherId,
                    GroupId = ss.GroupId,
                    GroupTitle = _storage.GetGroupById(ss.GroupId).Title,
                    TeacherLFM = _storage.GetTeacherById(ss.TeacherId).LastName + ' ' + _storage.GetTeacherById(ss.TeacherId).FirstName + ' ' + _storage.GetTeacherById(ss.TeacherId).MiddleName,
                    SemesterId = ss.SemesterId,
                    SemesterCount = _storage.GetSemesterById(ss.SemesterId).Count
                }).ToList(),
                Marks = _storage.GetMarksByStudentId(student.Id).Select(m => new MarkViewModel
                {
                    Id = m.Id,
                    Mark = m.Mark.GetValueOrDefault(),
                    SubjectId = m.SubjectId,
                    StudentId = student.Id,
                    Date = m.Date
                }).ToList()
            };
            return View(model);
        }

        [Authorize(Roles = "Admin, Teacher")]
        [HttpPost]
        public IActionResult AddMarkColumn(string date, int groupId, int subjectId)
        {
            var students = _storage.GetStudentsByGroupId(groupId);
            foreach(var student in students)
            {
                _storage.AddMark(new MarkEntity
                {
                    Mark = null,
                    StudentId = student.Id,
                    SubjectId = subjectId,
                    Date = date
                });
            }
            return new EmptyResult();
        }

        [Authorize(Roles = "Admin, Teacher")]
        [HttpGet]
        public IActionResult RemoveMarkColumn(string date, int groupId, int subjectId)
        {
            var studentsIds = _storage.GetStudentsByGroupId(groupId).Select(s => s.Id);
            var marks = _storage.GetMarksBySubjectId(subjectId).Where(m => m.Date == date && studentsIds.Contains(m.StudentId));
            foreach (var mark in marks)
            {
                _storage.DeleteMarkById(mark.Id);
            }
            return new EmptyResult();
        }

        [Authorize(Roles = "Admin, Teacher")]
        [HttpPost]
        public IActionResult SetMark(int studentId, int subjectId, int value, string date)
        {
            int? mark = null;
            if (value != 0) mark = value;
            var _mark = _storage.GetMarksByStudentId(studentId).Where((m => m.SubjectId == subjectId && m.Date.Equals(date))).FirstOrDefault();
            if (_mark != null)
            {
                _storage.UpdateMark(new MarkEntity
                {
                    Id = _mark.Id,
                    Mark = mark,
                    StudentId = _mark.StudentId,
                    SubjectId = _mark.SubjectId,
                    Date = date
                });
            }
            return new EmptyResult();
        }
    }
}