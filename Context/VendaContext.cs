using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using tech_test_payment_api.Models;

namespace tech_test_payment_api.Context
{
    public class VendaContext : DbContext
    {
        public VendaContext(DbContextOptions<VendaContext> options) : base(options){
        }
        public VendaContext(){}
        public DbSet<Venda> Vendas {get; set;}
        public DbSet<Vendedor> Vendedores {get; set;}
        public DbSet<ItemVendido> ItensVendidos {get; set;}
    }
}