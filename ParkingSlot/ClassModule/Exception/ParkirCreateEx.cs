using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSlot.ClassModule.Exception
{
    public class ParkirCreateEx : ParkirException
    {
        public ParkirCreateEx(int i) : base(string.Format("Can not create a parking lot of invalid size : {0}", i.ToString()))
        {
        }
    }
}
