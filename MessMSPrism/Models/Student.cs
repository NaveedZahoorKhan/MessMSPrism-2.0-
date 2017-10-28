using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessMSPrism.Models
{
    public class Student
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cnic { get; set; }
        public int RoomNo { get; set; }
        public string QrPath { get; set; }
        public string ImgPath { get; set; }
         

        // has many attendances
        public virtual IEnumerable<Attendance> Attendances { get; set; }

    }
}
