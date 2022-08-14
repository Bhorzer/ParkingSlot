using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSlot.ClassModule.Exception
{
    public class InvalidCommand : ParkirException
    {
        public InvalidCommand(string message = null) : base(message + "Please enter a valid command")
        {

        }

    }
    public class InvalidCommand2 : ParkirException
    {
        public InvalidCommand2(string message = null) : base(message + "XPlease enter a valid command")
        {

        }

    }
}
