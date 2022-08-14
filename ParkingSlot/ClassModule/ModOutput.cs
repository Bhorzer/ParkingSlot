using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSlot.ClassModule
{
    public class ModOutput
    {
        public void Create(int lotSize)
        {
            Console.WriteLine("Created a parking lot with {0} slots", lotSize);
            Console.Write("\n$ ");
        }

        public void Park(int slot)
        {
            Console.WriteLine("Allocated slot number: {0}", slot);
            Console.Write("\n$ ");
        }

        public void UnPark(int slot, string nopol, Decimal harga)
        {

            string formatfee = harga.ToString("#,##0");

            Console.WriteLine("Slot number {0} is free, parking fee {1} is Rp. {2}", slot, nopol, formatfee);
            Console.Write("\n$ ");
        }

        public void Print(string s)
        {
            Console.WriteLine(s);
        //    Console.WriteLine("");
        //   Console.Write("\n$ ");
        }


    }
}

