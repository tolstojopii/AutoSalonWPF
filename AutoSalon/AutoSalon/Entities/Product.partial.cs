using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSalon.Entities
{
    public partial class Product
    {

        [NotMapped]

        public string MainImagePath { get; set; } = "";
        [NotMapped]

        public string IsActiveText { get; set; } = "";
    }
}