using PruebaGAPData;
using PruebaGAPWebApi.Models;
using PruebaGAPWebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PruebaGAPWebApi.Controllers
{
    public class StudentController : ApiController
    {
        private PRUEBA_GAP_DBEntities Ctx = new PRUEBA_GAP_DBEntities();
        string useDB = System.Configuration.ConfigurationManager.AppSettings["UseDB"];

        public List<StudentModel> GetStudents()
        {
            Ctx.Configuration.ProxyCreationEnabled = false;
            StudentModel studentModel = new StudentModel();
            List<StudentModel> studentList = new List<StudentModel>();
            var students = Ctx.STUDENTs.ToArray();
            foreach ( var st in students )
            {
                studentModel = new StudentModel();
                int id = st.id;

                var subjects = Ctx.STUDENT_SUBJECT
                    .Join(Ctx.SUBJECTs, x => x.subject_id, sb => sb.id, (x, sb) => new { x.student_id, sb.name, x.grade })
                    .Where(xAndsb => xAndsb.student_id == id).ToList();

                studentModel.Name = st.name;
                studentModel.Subjects = new List<SubjectModel>();
                SubjectModel subjectModel = new SubjectModel();
                foreach ( var subject in subjects)
                {
                    subjectModel = new SubjectModel
                    {
                        Name = subject.name,
                        Grade = Convert.ToDouble(subject.grade)
                    };
                    studentModel.Subjects.Add(subjectModel);
                }
                studentList.Add(studentModel);
            }
            return studentList;
        }
        
        public dynamic GetSubjects(string name)
        {
            if (!"true".Equals(useDB))
            {
                return GetSubjectsRepository(name);
            }
            else
            {
                Ctx.Configuration.ProxyCreationEnabled = false;
                var Result = Ctx.SUBJECTs.ToArray();
                if (!string.IsNullOrEmpty(name))
                {
                    Result = Result.Where(x => x.name.ToLower() == name.ToLower()).ToArray();
                }
                return Result;
            }
        }

        private dynamic GetSubjectsRepository(string name)
        {
            List<SubjectModel> subjects = new List<SubjectModel>();
            StudentRepository.LoadSubjects(subjects);
            var res = subjects;
            if (!string.IsNullOrEmpty(name))
            {
                res = res.Where(x => x.Name.ToLower() == name.ToLower()).ToList();
            }
            return res;
        }

        public List<StudentModel> GetStudentsByFilters(string name, string subject, decimal range)
        {
            if (!"true".Equals(useDB))
            {
                return GetStudentsRepository(name, subject, range);
            }
            else
            {
                Ctx.Configuration.ProxyCreationEnabled = false;
                StudentModel studentModel = new StudentModel();
                List<StudentModel> studentList = new List<StudentModel>();
                var students = Ctx.STUDENTs.ToArray();
                if (!string.IsNullOrEmpty(name))
                {
                    students = students.Where(x => x.name.Contains(name)).ToArray();
                }

                foreach (var st in students)
                {
                    studentModel = new StudentModel();
                    int id = st.id;

                    var subjects = Ctx.STUDENT_SUBJECT
                        .Join(Ctx.SUBJECTs, x => x.subject_id, sb => sb.id, (x, sb) => new { x.student_id, sb.name, x.grade })
                        .Where(xAndsb => xAndsb.student_id == id).ToList();
                    if (!string.IsNullOrEmpty(subject))
                    {
                        subjects = subjects.Where(x => x.name.ToLower() == subject.ToLower()).ToList();
                    }
                    if (range > 0)
                    {
                        subjects = subjects.Where(x => x.grade >= range).ToList();
                    }

                    studentModel.Name = st.name;
                    studentModel.Subjects = new List<SubjectModel>();
                    SubjectModel subjectModel = new SubjectModel();
                    int cantSubjects = Ctx.SUBJECTs.Count();
                    if (subjects.Count == 1 || subjects.Count == cantSubjects)
                    {
                        foreach (var sub in subjects)
                        {
                            subjectModel = new SubjectModel
                            {
                                Name = sub.name,
                                Grade = Convert.ToDouble(sub.grade)
                            };
                            studentModel.Subjects.Add(subjectModel);
                        }
                        studentList.Add(studentModel);
                    }
                }
                return studentList;
            }
        }


        private List<StudentModel> GetStudentsRepository(string name, string subject, decimal range)
        {
            StudentModel studentModel = new StudentModel();
            List<StudentModel> studentList = new List<StudentModel>();
            List<StudentModel> listResponse = new List<StudentModel>();
            StudentRepository.LoadStudents(studentList);
            if (!string.IsNullOrEmpty(name))
            {
                studentList = studentList.Where(x => x.Name.Contains(name)).ToList();
            }
            foreach (StudentModel st in studentList)
            {
                List<SubjectModel> subjects = st.Subjects;
                if (!string.IsNullOrEmpty(subject))
                {
                    subjects = subjects.Where(x => x.Name.ToLower() == subject.ToLower()).ToList();
                }
                if (range > 0)
                {
                    subjects = subjects.Where(x => Convert.ToDecimal(x.Grade) >= range).ToList();
                }
                
                if (subjects.Count == 1 || subjects.Count == 4)
                {
                    st.Subjects = subjects;
                    listResponse.Add(st);
                } 
            }
            return listResponse;
        }

    }
}
