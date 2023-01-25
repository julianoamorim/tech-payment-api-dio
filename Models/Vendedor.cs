using System;

namespace tech_test_payment_api.Models
{
    public class Vendedor
    {
        //Id autoincrementado no JSON
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
    }
}