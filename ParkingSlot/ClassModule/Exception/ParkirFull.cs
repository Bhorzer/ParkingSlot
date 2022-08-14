using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSlot.ClassModule.Exception
{
    public class ParkirFull : ParkirException
    {
        public ParkirFull() : base("Sorry, parking's slot is full")
        {

        }
    }
}
