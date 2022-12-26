using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{
    public class Appointment
    {

        public int ID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? DoctorsNote { get; set; }

        public int? PatientID { get; set; }
        public int? DoctorID { get; set; }
        public int? PrescriptionID { get; set; }
        public int? InvoiceID { get; set; }
        public ApplicationUser? Owner { get; set; }

        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }
        public Prescription? Prescription { get; set; }
        public Invoice? Invoice { get; set; }
    }
}