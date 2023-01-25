using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace tech_test_payment_api.Models
{
    public class Venda
    {
        //Id autoincrementado no JSON
        public int Id { get; set;}
        public Vendedor Vendedor { get; set; }
        public List<ItemVendido> ItensVendido { get; set; }
        public DateTime Data { get; set; }
        //Colocar o status sem espaco
        public EnumStatusVenda Status {get; set;}
        
    }
}