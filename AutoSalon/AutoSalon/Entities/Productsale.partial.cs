using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSalon.Entities
{
    public partial class Productsale
    {
        [NotMapped]
        public decimal TotalCost { get; set; }
    }
}