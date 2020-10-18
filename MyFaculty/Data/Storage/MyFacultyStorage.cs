using Microsoft.EntityFrameworkCore;
using MyFaculty.Data.Context;
using MyFaculty.Data.Context.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFaculty.Data.Storage
{
    public class MyFacultyStorage
    {
        private readonly MyFacultyContext _context;
        public MyFacultyStorage(MyFacultyContext context)
        {
            _context = context;
        }

        #region Semesters
        public void AddSemester(SemesterEntity semester)
        {
            _context.Semesters.Add(semester);
            _context.SaveChanges();
        }
        public SemesterEntity GetSemesterById(int id)
        {
            return _context.Semesters.Where(s => s.Id == id).FirstOrDefault();
        }
        public List<SemesterEntity> GetAllSemesters()
        {
            return _context.Semesters.ToList();
        }
        public void UpdateSemester(SemesterEntity semester)
        {
            _context.Entry(semester).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }
        public void DeleteSemesterById(int id)
        {
            _context.Semesters.Remove(GetSemesterById(id));
            _context.SaveChanges();
        }
        #endregion

        #region Teachers
        public void AddTeacher(TeacherEntity teacher)
        {
            _context.Teachers.Add(teacher);
            _context.SaveChanges();
        }
        public TeacherEntity GetTeacherById(int id)
        {
            return _context.Teachers.Where(t => t.Id == id).FirstOrDefault();
        }
        public List<TeacherEntity> GetAllTeachers()
        {
            return _context.Teachers.ToList();
        }
        public void UpdateTeacher(TeacherEntity teacher)
        {
            _context.Entry(teacher).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }
        public void DeleteTeacherById(int id)
        {
            _context.Teachers.Remove(GetTeacherById(id));
            _context.SaveChanges();
        }
        #endregion

        #region Students
        public void AddStudent(StudentEntity student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }
        public StudentEntity GetStudentById(int id)
        {
            return _context.Students.Where(s => s.Id == id).FirstOrDefault();
        }
        public List<StudentEntity> GetStudentsByGroupId(int groupId)
        {
            return _context.Students.Where(s => s.GroupId == groupId).ToList();
        }
        public List<StudentEntity> GetAllStudents()
        {
            return _context.Students.ToList();
        }
        public void UpdateStudent(StudentEntity student)
        {
            _context.Entry(student).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }
        public void DeleteStudentById(int id)
        {
            _context.Students.Remove(GetStudentById(id));
            _context.SaveChanges();
        }
        #endregion

        #region Groups
        public void AddGroup(GroupEntity group)
        {
            _context.Groups.Add(group);
            _context.SaveChanges();
        }
        public GroupEntity GetGroupById(int id)
        {
            return _context.Groups.Where(g => g.Id == id).FirstOrDefault();
        }
        public void UpdateGroup(GroupEntity group)
        {
            _context.Entry(group).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }
        public List<GroupEntity> GetAllGroups()
        {
            return _context.Groups.ToList();
        }
        public void DeleteGroupById(int id)
        {
            _context.Groups.Remove(GetGroupById(id));
            _context.SaveChanges();
        }
        #endregion

        #region Subjects
        public void AddSubject(SubjectEntity subject)
        {
            _context.Subjects.Add(subject);
            _context.SaveChanges();
        }
        public SubjectEntity GetSubjectById(int id)
        {
            return _context.Subjects.Where(s => s.Id == id).FirstOrDefault();
        }
        public List<SubjectEntity> GetSubjectsByGroupId(int groupId)
        {
            return _context.Subjects.Where(s => s.GroupId == groupId).ToList();
        }
        public List<SubjectEntity> GetSubjectsByTeacherId(int TeacherId)
        {
            return _context.Subjects.Where(s => s.TeacherId == TeacherId).ToList();
        }
        public List<SubjectEntity> GetSubjectsBySemesterId(int SemesterId)
        {
            return _context.Subjects.Where(s => s.SemesterId == SemesterId).ToList();
        }
        public List<SubjectEntity> GetAllSubjects()
        {
            return _context.Subjects.ToList();
        }
        public void UpdateSubject(SubjectEntity subject)
        {
            _context.Entry(subject).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }
        public void DeleteSubjectById(int id)
        {
            _context.Subjects.Remove(GetSubjectById(id));
            _context.SaveChanges();
        }
        #endregion

        #region Marks
        public void AddMark(MarkEntity mark)
        {
            _context.Marks.Add(mark);
            _context.SaveChanges();
        }
        public MarkEntity GetMarkById(int id)
        {
            return _context.Marks.Where(m => m.Id == id).FirstOrDefault();
        }
        public List<MarkEntity> GetMarksByStudentId(int studentId)
        {
            return _context.Marks.AsNoTracking().Where(m => m.StudentId == studentId).ToList();
        }
        public List<MarkEntity> GetMarksBySubjectId(int subjectId)
        {
            return _context.Marks.Where(m => m.SubjectId == subjectId).ToList();
        }
        public void UpdateMark(MarkEntity mark)
        {
            _context.Entry(mark).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }
        public void DeleteMarkById(int id)
        {
            _context.Marks.Remove(GetMarkById(id));
            _context.SaveChanges();
        }
        #endregion
    }
}
