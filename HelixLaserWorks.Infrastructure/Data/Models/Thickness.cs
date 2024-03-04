using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelixLaserWorks.Infrastructure.Data.Models
{
    public class Thickness
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public double Value { get; set; }

        public ICollection<MaterialThickness> MaterialThicknesses { get; set; } = new List<MaterialThickness>();
    }
}
