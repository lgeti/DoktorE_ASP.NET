using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{
    public class BloodDonation
    {
        public int ID { get; set; }
        public DateTime date { get; set; }
        public int? PatientID { get; set; }
        public ApplicationUser? Owner { get; set; }
        
        public Patient? Patient { get; set; }
    }
}