using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tech_test_payment_api.Models
{
    public class ItemVendido
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public float Preco { get; set; }
        public int Quantidade { get; set; }
        
    }
}