using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSlot
{
    public class ParkingVisual
    {
        public static string AlignOutput(string first, int second)
        {
            int n = second + 5 - first.Length;
            string s = string.Empty;
            for (int i = 0; i < n; i++)
            {
                s += " ";
            }
            return first + s;
        }

        public static void vHome()
        {
            Console.WriteLine("\n\n");
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("-------------  Parking Slot - By Indra Adji  -------------");
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("\n\n");
        }

        public static void PrintCommands()
        {
            Console.WriteLine("\nType your commands : (Enter 'exit' to EXIT Parking Application)");
            Console.WriteLine("\n");
            Console.Write("\n$ ");
        }

        public static string PemisahKoma(List<string> list)
        {
            string ans = "";
            for (int i = 0; i < list.Count; i++)
            {
                ans += list[i];
                if (i != list.Count - 1)
                {
                    ans += ", ";
                }
            }
            return ans;
        }
    }
}
