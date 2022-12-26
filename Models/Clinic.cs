using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{
    public class Clinic
    {
        public int ID { get; set; }
        public string typeOfClinic { get; set; }

        public ICollection<Doctor>? Doctors { get; set; }
    }
}