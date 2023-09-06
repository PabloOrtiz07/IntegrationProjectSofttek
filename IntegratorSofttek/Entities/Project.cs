using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntegratorSofttek.Entities
{
    [Table("projects")]
    public class Project
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public int Estado { get; set; }
        
    }
}
