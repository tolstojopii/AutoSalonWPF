using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSalon.Entities
{
    public class Productphoto
    {
        public int Id { get; set; }
        public int Productid { get; set; }
        public string Photopath { get; set; } = null!;

        public Product Product { get; set; } = null!;
    }
}