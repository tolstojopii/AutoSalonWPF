using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;


namespace AutoSalon.Entities
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public decimal Cost { get; set; }
        public string? Description { get; set; }
        public string? Mainimage { get; set; } // путь к файлу или base64
        public BitArray Isactive { get; set; } = null!;
        public int? Manufacturerid { get; set; }

        public Manufacturer? Manufacturer { get; set; }
        public ICollection<Productphoto> Productphotos { get; set; } = new List<Productphoto>();
        public ICollection<Productsale> Productsales { get; set; } = new List<Productsale>();
    }
}