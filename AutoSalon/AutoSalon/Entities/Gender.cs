using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSalon.Entities
{
    public class Gender
    {
        public char Code { get; set; } // 'М' или 'Ж'
        public string Name { get; set; } = null!;

        public ICollection<Client> Clients { get; set; } = new List<Client>();
    }
}