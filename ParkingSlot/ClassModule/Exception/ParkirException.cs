using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSlot.ClassModule.Exception
{
    public class ParkirException : System.Exception
    {
        string _message;
        public ParkirException(string message) : base(message)
        {
            this._message = message;
        }
    }
}
