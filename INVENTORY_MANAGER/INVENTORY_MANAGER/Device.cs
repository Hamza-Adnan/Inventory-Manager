using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INVENTORY_MANAGER
{
    public class Device
    {
        [Key]
        public int Id { get; set; }
        public string DeviceType { get; set; }
        public string DeviceName { get; set; }
        public string SerialNumber { get; set; }
        public string IssuedPersonName { get; set; }
        public string IssuedPersonIdNumber { get; set; }
        public DateTime DateIssued { get; set; }
        public DateTime DateDue { get; set; }
        public bool Received { get; set; }
        public DateTime? DateReturned { get; set; } // Nullable DateTime

        // Constructor to set default values
        public Device()
        {
            DateIssued = DateTime.Now;
            DateDue = DateTime.Now.AddDays(30); // Default due date is 30 days from issuance
            Received = false; // Device not received by default
        }
    }
}

