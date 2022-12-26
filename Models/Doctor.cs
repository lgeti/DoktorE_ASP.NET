using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{
    public class Doctor
    {
        public int ID { get; set; }
        public string Ime { get; set; }
        public string Priimek { get; set; }
        public string Specialty { get; set; }
        [ForeignKey("Clinic")]
        
        public int ClinicID { get; set; }
        
        public ICollection<Appointment>? Appointments { get; set; }
        public Clinic Clinic { get; set; }
    }
}