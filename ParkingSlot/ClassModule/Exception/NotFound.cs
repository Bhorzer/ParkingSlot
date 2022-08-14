using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSlot.ClassModule.Exception
{
    public class NotFound : ParkirException
    {
        public NotFound(string message = null) : base(message + "Not Found")
        {

        }
    }
}
