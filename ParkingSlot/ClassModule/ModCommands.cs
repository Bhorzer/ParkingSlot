using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSlot.ClassModule
{
    public class ModCommands
    {
        public const string CREATE_PARKING_LOT_CMD = "create_parking_lot";
        public const string PARK_CMD = "park";
        public const string LEAVE_CMD = "leave";
        public const string STATUS_CMD = "status";
        public const string REPORT_NOPOL_WARNA = "registration_numbers_for_vehicles_with_colour";
        public const string REPORT_TOTAL_JENIS = "type_of_vehicles";
        public const string REPORT_NOPOL_ODDPLATE = "registration_numbers_for_vehicles_with_ood_plate";
        public const string REPORT_NOPOL_EVENTPLATE = "registration_numbers_for_vehicles_with_event_plate"; 
        public const string REPORT_SLOT_WARNA = "slot_numbers_for_vehicles_with_colour";
        public const string REPORT_SLOT_NOPOL = "slot_number_for_registration_number";


    }
}

