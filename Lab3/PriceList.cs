using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab3
{
    public class PriceList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Required]
        public string? Marka { get; set; }
        [Required]
        public string? Complect { get; set; }
        [Required]
        public decimal? Price { get; set; }
    }
}
