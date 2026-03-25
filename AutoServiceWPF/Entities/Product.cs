using System;
using System.Collections;
using System.Collections.Generic;

namespace AutoServiceWPF.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public decimal Cost { get; set; }

    public string? Description { get; set; }

    public string? Mainimage { get; set; }

    public BitArray Isactive { get; set; } = null!;

    public int? Manufacturerid { get; set; }

    public virtual ICollection<Attachedproduct> Attachedproducts { get; set; } = new List<Attachedproduct>();

    public virtual Manufacturer? Manufacturer { get; set; }

    public virtual ICollection<Productphoto> Productphotos { get; set; } = new List<Productphoto>();

    public virtual ICollection<Productsale> Productsales { get; set; } = new List<Productsale>();
}
