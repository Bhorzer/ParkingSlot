using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingSlot.Model;

namespace ParkingSlot.Interface
{
    public interface IParkingSlot
    {
        void Execute(string inputSring);
        void CreateParkingLot(int lotSize);
        void Park(Kendaraan kendaraan);
        void Leave(int slot);
        void GetStatus();
        void GetReportNopolWarna(string warna);
        void GetReportTotalJenis(string jenis);
        void GetReportSlotWarna(string warna);
        void GetReportSlotNopol(string Nopol);
        bool Validate(string command);

    }

}
