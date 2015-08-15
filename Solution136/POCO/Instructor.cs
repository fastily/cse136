using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCO
{
    public class Instructor
    {
        public int instructor_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string title { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
