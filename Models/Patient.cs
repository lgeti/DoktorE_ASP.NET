using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{
    public class Patient
    {
        public int ID { get; set; }

        public long ZZZS { get; set; }
        public string Ime { get; set; }
        public string Priimek { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
        public ICollection<Invoice>? Invoices { get; set; }
        public ICollection<BloodDonation>? BloodDonations { get; set; }
        public ICollection<Prescription>? Prescriptions { get; set; }
    }
}