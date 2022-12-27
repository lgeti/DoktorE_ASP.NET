using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{
    public class Prescription
    {
        public int ID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? expiryDate { get; set; }
        public int PatientID { get; set; }
        public string? drugs { get; set; }
        public ApplicationUser? Owner { get; set; }
        public Patient? Patient { get; set; }
    }
} 