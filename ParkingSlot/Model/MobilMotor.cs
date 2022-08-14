using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingSlot.Model
{
    public class MobilMotor : Kendaraan
    {
        public MobilMotor(string NoPol, string jenis, string warna, DateTime jammulai) : base(NoPol, jenis, warna, jammulai)
        {
        }
    }


}
