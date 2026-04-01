using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSalon.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string? Patronymic { get; set; }
        public DateOnly? Birthday { get; set; }
        public DateTime Registrationtime { get; set; }
        public string? Email { get; set; }
        public string Phone { get; set; } = null!;
        public char Gendercode { get; set; }
        public string? Photopath { get; set; }

        public Gender GendercodeNavigation { get; set; } = null!;
        public ICollection<Productsale> Productsales { get; set; } = new List<Productsale>();

        public string FIO => $"{Lastname} {Firstname} {Patronymic}".Trim();
    }
}