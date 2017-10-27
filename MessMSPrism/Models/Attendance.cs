using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessMSPrism.Resources.Enums;
namespace MessMSPrism.Models
{
    class Attendance
    {
        public int StudentId { get; set; }
        public DateTime Time { get; set; }
        public ShifId ShifId { get; set; }



    }
}
