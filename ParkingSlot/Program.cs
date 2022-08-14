using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using ParkingSlot.Model;
using ParkingSlot.ClassModule;
using ParkingSlot.ClassModule.Exception;
using ParkingSlot.Interface;

namespace ParkingSlot
{
    public class Program
    {
        public static IParkingSlot provider;


        static void Main()
        {
            
            provider = new ClassParkingSlot();
            ParkingVisual.vHome();
            ParkingVisual.PrintCommands();
                while (true)
                {
                    try
                    {
                        var input = Console.ReadLine();
                        if (input.ToLower().Equals("exit"))
                        {
                            break;
                        }
                        else
                        {
                            if (provider.Validate(input))
                            {
                                try
                                {
                                    provider.Execute(input);
                                }
                                catch (ParkirException e)
                                {
                                    Console.WriteLine(e.Message);
                                    Console.Write("\n$ ");
                                }
                            }
                            else
                            {
                                ParkingVisual.PrintCommands();
                            }
                        }

                    }
                    catch (System.Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.Write("\n$ ");
                    }

                }
        }




    }
}
