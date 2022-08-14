using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingSlot.ClassModule.Exception;

namespace ParkingSlot.ClassModule
{
    public class ModInputValidasi
    {
        Dictionary<string, int> validCommands; 
        public ModInputValidasi()
        {
            validCommands = new Dictionary<string, int>();
            validCommands.Add(ModCommands.CREATE_PARKING_LOT_CMD, 1);
            validCommands.Add(ModCommands.PARK_CMD, 3);
            validCommands.Add(ModCommands.LEAVE_CMD, 1);
            validCommands.Add(ModCommands.STATUS_CMD, 0);
            validCommands.Add(ModCommands.REPORT_NOPOL_WARNA, 1);
            validCommands.Add(ModCommands.REPORT_TOTAL_JENIS, 1);
            validCommands.Add(ModCommands.REPORT_SLOT_WARNA, 1);
            validCommands.Add(ModCommands.REPORT_SLOT_NOPOL, 1);
        }

        public bool Validasi(string inputString)
        {
            bool valid = true;
            try
            {
                string[] input = inputString.Split(' ');
                int paramCount;
                if (validCommands.TryGetValue(input[0], out paramCount))
                {
                    // Cek Parameter
                    switch (input.Length)
                    {
                        case 1:
                            if (paramCount != 0)
                                throw new InvalidCommand(); 
                            break;
                        case 2:
                            if (paramCount != 1)
                                throw new InvalidCommand(); 
                            break;
                        case 4:
                            if (paramCount != 3)
                                throw new InvalidCommand(); 
                            break;
                        default:
                            throw new InvalidCommand();
                    }
                }

            }
            catch (ParkirException e)
            {
                throw new InvalidCommand();
            }
            return valid;

        }
    }
}
