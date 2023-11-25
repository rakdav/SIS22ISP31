using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab3
{
    public class Sales
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Required]
        public DateTime DateSale { get; set; }
        [Required]
        public string? FIO { get; set; }
        [Required]
        public int PriceList_Id { get; set; }
        [Required]
        public string? Complect { get; set; }
        [Required]
        public int Discount { get; set;}
        [Required]
        public Decimal? TotalPrice { get; set; }
    }
}
