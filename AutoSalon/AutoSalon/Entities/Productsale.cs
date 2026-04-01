using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSalon.Entities
{
    public partial class Productsale
    {
        public int Id { get; set; }
        public DateTime Saledate { get; set; }
        public int Productid { get; set; }
        public int Quantity { get; set; }
        public int Clientid { get; set; }  // прямая связь с клиентом

        public Product Product { get; set; } = null!;
        public Client Client { get; set; } = null!;
    }
}