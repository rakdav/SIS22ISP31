using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Models
{
    public class Sales
    {
        public int Id { get; set; }
        public DateTime DateSale { get; set; }
        public string? FIO { get; set; }
        public int PriceList_Id { get; set; }
        public PriceList? PriceList { get; set; }
        public string? Complect { get; set; }
        public int Discount { get; set; }
        public decimal? TotalPrice { get; set; }

    }
}
