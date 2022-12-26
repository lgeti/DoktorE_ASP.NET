using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace web.Models
{
    public class Invoice
    {
        public int ID { get; set; }
        public int sum { get; set; }
        public int PatientID { get; set; }
        public ApplicationUser? Owner { get; set; }

        [ForeignKey("Appointment")]
        public int AppointmentID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public Appointment Appointment { get; set; }
        public Patient? Patient { get; set; }
    }
}