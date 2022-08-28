using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerformCRUD.Models
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class StudentListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Grade { get; set; }
    }
}