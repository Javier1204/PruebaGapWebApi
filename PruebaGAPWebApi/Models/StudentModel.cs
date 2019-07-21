using PruebaGAPData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaGAPWebApi.Models
{
    public class StudentModel
    {

        public string Name { get; set; }

        public List<SubjectModel> Subjects { get; set; }

    }
}