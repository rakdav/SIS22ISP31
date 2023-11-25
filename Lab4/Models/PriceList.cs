using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Models
{
    public class PriceList
    {
        public int Id { get; set; }
        public string? Marka { get; set; }
        public string? Complect { get; set; }
        public decimal? Price { get; set; }
    }
}
