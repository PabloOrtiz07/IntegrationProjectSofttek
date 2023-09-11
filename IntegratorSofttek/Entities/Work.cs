using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntegratorSofttek.Entities
{
    [Table("works")]

    public class Work
    {
        [Key]
        public int Id{ get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("Id")]
        public  int project { get; set; }

        [ForeignKey("Id")]
        public int service { get; set; }

        public int HoursQuantity { get; set; }

        public double HourlyRate { get; set; }

        public double Cost { get; set; }


    }
}
