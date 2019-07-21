using PruebaGAPWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaGAPWebApi.Repository
{
    public class StudentRepository
    {
        /// <summary>
        /// Method that fill a list with students information
        /// </summary>
        /// <param name="students">List to fill</param>
        public static void LoadStudents(List<StudentModel> students)
        {
            StudentModel student = new StudentModel();
            student = BuildStudent("Anna Taylor", 90, 85, 86, 89);
            students.Add(student);
            student = BuildStudent("Mark Smith", 82, 75, 89, 86);
            students.Add(student);
            student = BuildStudent("John Doe", 89, 76, 94, 82);
            students.Add(student);
            student = BuildStudent("Jack Jones", 93, 73, 97, 90);
            students.Add(student);
            student = BuildStudent("Melody Queens", 87, 89, 80, 83);
            students.Add(student);
            student = BuildStudent("Noah Menac", 86, 90, 83, 85);
            students.Add(student);
        }

        /// <summary>
        /// Method that fill a list with subjects information
        /// </summary>
        /// <param name="subjects">List to fill</param>
        public static void LoadSubjects(List<SubjectModel> subjects)
        {
            SubjectModel subject = new SubjectModel();
            subject = new SubjectModel
            {
                Name = "Language & Arts"
            }; 
            subjects.Add(subject);
            subject = new SubjectModel
            {
                Name = "Science"
            };
            subjects.Add(subject);
            subject = new SubjectModel
            {
                Name = "Social Studies"
            };
            subjects.Add(subject);
            subject = new SubjectModel
            {
                Name = "Maths"
            };
            subjects.Add(subject);
        }

        /// <summary>
        /// Method that returns a student object with the information provided
        /// </summary>
        /// <param name="name"> Student's name</param>
        /// <param name="mark1">Mark for "Language & Arts" subject</param>
        /// <param name="mark2">Mark for "Science" subject</param>
        /// <param name="mark3">Mark for "Social Studies" subject</param>
        /// <param name="mark4">Mark for "Math" subject</param>
        /// <returns></returns>
        private static StudentModel BuildStudent(String name, double mark1, double mark2, double mark3, double mark4)
        {
            StudentModel student = new StudentModel
            {
                Name = name
            };
            List<SubjectModel> subjects = new List<SubjectModel>();
            SubjectModel subject1 = new SubjectModel
            {
                Name = "Language & Arts",
                Grade = mark1
            };
            subjects.Add(subject1);
            SubjectModel subject2 = new SubjectModel
            {
                Name = "Science",
                Grade = mark2
            };
            subjects.Add(subject2);
            SubjectModel subject3 = new SubjectModel
            {
                Name = "Social Studies",
                Grade = mark3
            };
            subjects.Add(subject3);
            SubjectModel subject4 = new SubjectModel
            {
                Name = "Maths",
                Grade = mark4
            };
            subjects.Add(subject4);
            student.Subjects = subjects;
            return student;
        }

    }
}