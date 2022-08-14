using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingSlot.Interface;
using ParkingSlot.ClassModule;
using ParkingSlot.ClassModule.Exception;
using ParkingSlot.Model;
using ParkingSlot.Interface;


namespace ParkingSlot.ClassModule
{
    public class ClassParkingSlot : IParkingSlot
    {
        private int _lotSize = -1;
        private ModOutput output;
        private ModInputValidasi validator;

        Dictionary<string, string> _regToColourMap;
        Dictionary<string, string> _regToTypeMap;
        Dictionary<int, string> _slotToRegMap;
        Dictionary<int, string> _slotToColourMap;
        Dictionary<int, string> _slotToTypeMap;
        Dictionary<int, DateTime> _slotToJamMulaiMap;

        

        public ClassParkingSlot()
        {
            this.output = new ModOutput();
            this.validator = new ModInputValidasi();
        }

        public void Execute(string inputString)
        {
            string[] inputs = inputString.Split(' ');
            switch (inputs[0])
            {
                case ModCommands.CREATE_PARKING_LOT_CMD:
                    {
                        CreateParkingLot(Int32.Parse(inputs[1]));
                        break;
                    }
                case ModCommands.PARK_CMD:
                    {
                        Kendaraan paramkendaraan = new Kendaraan(inputs[1], inputs[2], inputs[3], DateTime.Now);
                        Park(paramkendaraan);
                    //    throw new ParkirException("Err nya disini");
                        break;
                    }
                case ModCommands.LEAVE_CMD:
                    {
                        try
                        {
                            Leave(Int32.Parse(inputs[1]));
                        }
                        catch (System.Exception e)
                        {
                            throw new ParkirException("Please provide valid input for slot number");
                        }
                        break;
                    }
                case ModCommands.REPORT_NOPOL_WARNA:
                    {
                        GetReportNopolWarna(inputs[1]);
                        break;
                    }
                case ModCommands.REPORT_TOTAL_JENIS:
                    {
                        GetReportTotalJenis(inputs[1]);
                        break;
                    }
                case ModCommands.REPORT_SLOT_WARNA:
                    {
                        GetReportSlotWarna(inputs[1]);
                        break;
                    }
                case ModCommands.REPORT_SLOT_NOPOL:
                    {
                        GetReportSlotNopol(inputs[1]);
                        break;
                    }
                case ModCommands.STATUS_CMD:
                    {
                        GetStatus();
                        break;
                    }
                default:
                    throw new ParkirException("Please enter a valid command");

            }
        }

        public void CreateParkingLot(int lotSize)
        {
            if (lotSize > 0)
            {
                this._lotSize = lotSize;
                _regToColourMap = new Dictionary<string, string>();
                _regToTypeMap = new Dictionary<string, string>();
                _slotToRegMap = new Dictionary<int, string>();
                _slotToColourMap = new Dictionary<int, string>();
                _slotToTypeMap = new Dictionary<int, string>();
                _slotToJamMulaiMap = new Dictionary<int, DateTime>();

                output.Create(lotSize);
            }
            else
            {
                throw new ParkirCreateEx(lotSize);
            }
        }

        public void Park(Kendaraan kendaraan)
        {
            validParkingLotExists();
            string xnopol = kendaraan.vNomorPolisi;
            string xwarna = kendaraan.vWarna;
            string xjenis = kendaraan.vJenis;
            DateTime xjammulai = kendaraan.vJamMulai;

            for (int i = 1; i <= _lotSize; i++)
            {
                string temp;
                bool emptySlot = _slotToRegMap.TryGetValue(i, out temp);
                if (!emptySlot)
                {
                    _slotToRegMap.Add(i, xnopol);
                    _slotToColourMap.Add(i, xwarna);
                    _slotToTypeMap.Add(i, xjenis);
                    _slotToJamMulaiMap.Add(i, xjammulai);
                    _regToColourMap.Add(xnopol, xwarna);
                    _regToTypeMap.Add(xnopol, xjenis);

                    output.Park(i);
                    return;
                }
            }
            throw new ParkirFull();
        }

        public void Leave(int slot)
        {
            validParkingLotExists();
            if (slot > _lotSize || slot < 1)
            {
                throw new InvalidCommand("The given slot doesn't exist. ");
            }
            
            string NoPol;
            DateTime vJamMulai = DateTime.Now,vJamSelesai = DateTime.Now;
            Decimal biayaparkir = 0;

            
            if (_slotToRegMap.TryGetValue(slot, out NoPol))
            {

                foreach (var key in _slotToJamMulaiMap.Keys)
                {
                    if (key == slot )
                    {
                        vJamMulai = _slotToJamMulaiMap[key];
                    }
                
                }

                vJamSelesai = DateTime.Now;

                TimeSpan selisih = vJamSelesai.Subtract(vJamMulai);
                
                biayaparkir = (Math.Ceiling((decimal)selisih.TotalHours) * 1000);

                _slotToRegMap.Remove(slot);
                _slotToColourMap.Remove(slot);
                _slotToTypeMap.Remove(slot);
                _slotToJamMulaiMap.Remove(slot);
                _regToColourMap.Remove(NoPol);
                _regToTypeMap.Remove(NoPol);
                output.UnPark(slot,NoPol,biayaparkir);
                return;
            }
            else
            {
                throw new ParkirException("No car is parked in the mentioned slot. ");
            }
        }

        public void GetStatus()
        {
            validParkingLotExists();
            int slotLength = ModParkingSlotHeader.SlotNomorText.Length, 
                NoPolLength = ModParkingSlotHeader.NomorPolisiText.Length,
                WarnaLength = ModParkingSlotHeader.WarnaText.Length,
                JenisLength = ModParkingSlotHeader.JenisText.Length,
                JamMulaiLength = ModParkingSlotHeader.JamMulaiText.Length;
                
            foreach (var slot in _slotToRegMap.Keys)
            {
                slotLength = (slot.ToString().Length > slotLength) ? slot.ToString().Length : slotLength;
                NoPolLength = (_slotToRegMap[slot].Length > NoPolLength) ? _slotToRegMap[slot].Length : NoPolLength;
                WarnaLength = (_slotToColourMap[slot].Length > WarnaLength) ? _slotToColourMap[slot].Length : WarnaLength;
                JenisLength = (_slotToTypeMap[slot].Length > JenisLength) ? _slotToTypeMap[slot].Length : JenisLength;
                JamMulaiLength = (_slotToJamMulaiMap[slot].ToString().Length > JamMulaiLength) ? _slotToJamMulaiMap[slot].ToString().Length : JamMulaiLength;

            }
            string slotJarak = "";
            for (int i = 0; i < (slotLength - ModParkingSlotHeader.SlotNomorText.Length + 7); i++)
            {
                slotJarak += " ";
            }
            string regJarak = "";
            for (int i = 0; i < NoPolLength - ModParkingSlotHeader.NomorPolisiText.Length + 7; i++)
            {
                regJarak += " ";
            }
            string warnaJarak = "";
            for (int i = 0; i < WarnaLength - ModParkingSlotHeader.WarnaText.Length + 7; i++)
            {
                warnaJarak += " ";
            }
            string jenisJarak = "";
            for (int i = 0; i < JenisLength - ModParkingSlotHeader.JenisText.Length + 7; i++)
            {
                jenisJarak += " ";
            }
            string jamJarak = "";
            for (int i = 0; i < JamMulaiLength - ModParkingSlotHeader.JamMulaiText.Length + 1; i++)
            {
                jamJarak += " ";
            }

            // Headernya
            output.Print(ParkingVisual.AlignOutput(ModParkingSlotHeader.SlotNomorText, slotLength) + ParkingVisual.AlignOutput(ModParkingSlotHeader.NomorPolisiText, NoPolLength) + ParkingVisual.AlignOutput(ModParkingSlotHeader.WarnaText, WarnaLength) + ParkingVisual.AlignOutput(ModParkingSlotHeader.JenisText, JenisLength) + ParkingVisual.AlignOutput(ModParkingSlotHeader.JamMulaiText, JamMulaiLength));
            foreach (var key in _slotToRegMap.Keys)
            {
                output.Print(string.Format("{0}{1}{2}{3}{4}", ParkingVisual.AlignOutput(key.ToString(), slotLength), ParkingVisual.AlignOutput(_slotToRegMap[key], NoPolLength), ParkingVisual.AlignOutput(_slotToColourMap[key], WarnaLength), ParkingVisual.AlignOutput(_slotToTypeMap[key], JenisLength), ParkingVisual.AlignOutput(_slotToJamMulaiMap[key].ToString("dd-MMM-yyyy HH:mm:ss"), JamMulaiLength)));
            }

            Console.Write("\n$ ");
            return;
        }


        public void GetReportNopolWarna(string warna)
        {
            validParkingLotExists();
            warna = warna.ToLower();
            List<string> cars = new List<string>();
            foreach (var reg in _regToColourMap.Keys)
            {
                if (_regToColourMap[reg].ToLower().Equals(warna))
                {
                    cars.Add(reg);
                }
            }
            if (cars.Count == 0)
            {
                throw new NotFound();
            }
            output.Print(ParkingVisual.PemisahKoma(cars));
            return;
        }
        public void GetReportTotalJenis(string jenis)
        {
            validParkingLotExists();
            jenis = jenis.ToLower();
           
            int hitung = 0;
            foreach (var reg in _regToTypeMap.Keys)
            {

                if (_regToTypeMap[reg].ToLower().Equals(jenis))
                {
                    hitung += 1;
                }
            }
            if (hitung == 0)
            {
      
                throw new NotFound();
            }
            output.Print(hitung.ToString());
            return;
        }
        public void GetRegistrationNumsForType(string type)
        {
            validParkingLotExists();
            type = type.ToLower();
            List<string> cars = new List<string>();
            foreach (var reg in _regToTypeMap.Keys)
            {
                if (_regToTypeMap[reg].ToLower().Equals(type))
                {
                    cars.Add(reg);
                }
            }
            if (cars.Count == 0)
            {
                throw new NotFound();
            }
            output.Print(ParkingVisual.PemisahKoma(cars));
            return;
        }

        public void GetReportSlotNopol(string Nopol)
        {
            validParkingLotExists();
            Nopol = Nopol.ToLower();
            foreach (var slot in _slotToRegMap.Keys)
            {
                if (_slotToRegMap[slot].ToLower().Equals(Nopol))
                {
                    output.Print(slot.ToString());
                    return;
                }
            }
            throw new NotFound();
        }

        public void GetReportSlotWarna(string warna)
        {
            validParkingLotExists();
            warna = warna.ToLower();
            List<string> kendaraan = new List<string>();
            foreach (var reg in _regToColourMap.Keys)
            {
                if (_regToColourMap[reg].ToLower().Equals(warna))
                {
                    kendaraan.Add(reg);
                }
            }
            if (kendaraan.Count == 0)
            {
                throw new NotFound();
            }

            List<string> slots = new List<string>();
            foreach (var slot in _slotToRegMap.Keys)
            {
                if (kendaraan.Contains(_slotToRegMap[slot]))
                {
                    slots.Add(slot.ToString());
                }
            }

            if (slots.Count == 0)
            {
                throw new NotFound();
            }
            output.Print(ParkingVisual.PemisahKoma(slots));
            return;
        }

        public void GetSlotNumsForType(string type)
        {
            validParkingLotExists();
            type = type.ToLower();
            List<string> cars = new List<string>();
            foreach (var reg in _regToTypeMap.Keys)
            {
                if (_regToTypeMap[reg].ToLower().Equals(type))
                {
                    cars.Add(reg);
                }
            }
            if (cars.Count == 0)
            {
                throw new NotFound();
            }

            List<string> slots = new List<string>();
            foreach (var slot in _slotToRegMap.Keys)
            {
                if (cars.Contains(_slotToRegMap[slot]))
                {
                    slots.Add(slot.ToString());
                }
            }

            if (slots.Count == 0)
            {
                throw new NotFound();
            }
            output.Print(ParkingVisual.PemisahKoma(slots));
            return;
        }

        public bool Validate(string command)
        {
            return validator.Validasi(command);
        }

        private bool validParkingLotExists()
        {
            if (_lotSize == -1)
            {
                throw new ParkirException("You must need to create the parking lot first");
            }
            return true;
        }
    }
}

