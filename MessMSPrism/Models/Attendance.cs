using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessMSPrism.Resources.Enums;
namespace MessMSPrism.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public ShifId ShifId { get; set; }

        // has one student
        public virtual Student Student{ get; set; }


    }
}
