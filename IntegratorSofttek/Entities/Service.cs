using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntegratorSofttek.Entities
{
    [Table("services")]

    public class Service
    {
        [Key]
        public int Id {  get; set; }

        public string Description { get; set; }

        public bool Status  { get; set; }

        public double HourlyRate { get; set; }
    }
}
