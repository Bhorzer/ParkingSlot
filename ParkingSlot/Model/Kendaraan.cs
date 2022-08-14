using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSlot.Model
{
    public class Kendaraan
    {
        public string vNomorPolisi { get; set; }
        public string vWarna { get; set; }
        public string vJenis { get; set; }
        public DateTime vJamMulai { get; set; }


        public Kendaraan(string NoPol, string warna, string jenis, DateTime jammulai)

        {
            vNomorPolisi = NoPol;
            vWarna = warna;
            vJenis = jenis;
            vJamMulai = jammulai;

        }
    }
}
