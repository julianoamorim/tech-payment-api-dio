using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tech_test_payment_api.Context;
using tech_test_payment_api.Models;

namespace tech_test_payment_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendaController : ControllerBase
    {
        private readonly VendaContext _context;

        public VendaController(VendaContext context)
        {
            _context = context;
        }

        [HttpPost("RealizarVenda")]
        public IActionResult Criar(Venda venda)
        {
            if (venda.Vendedor.Nome == "" || venda.Vendedor.Telefone == "" || venda.Vendedor.Cpf == "" || venda.Vendedor.Email == "")
            {
                return BadRequest(new { Erro = "Todos os campos do vendedor devem ser prenxidos." });
            }
            if(venda.ItensVendido == null || !venda.ItensVendido.Any()){ //Any verifica se o objeto e vazio
                return BadRequest(new { Erro = "Deve haver pelo menos um item vendido." });
            }
            /* if(ModelState.IsValid)
                return BadRequest(new { Erro = "Os campos do Vendedor nao podem ser nulos e Deve haver pelo menos um item vendido" }); */
            _context.Vendas.Add(venda);
            _context.SaveChanges();
            return Ok(venda);
            //return CreatedAtAction(nameof(ObterPorId), new { id = venda.Id }, venda);
        }

        [HttpGet("ObterPorId/{id}")]
        public IActionResult ObterPorId(int id)
        {
            var venda = _context.Vendas
                .Include(d => d.Vendedor)
                .Include(d => d.ItensVendido)
                .Where(c => c.Id.Equals(id));
            if (venda == null)
                return NotFound();
            return Ok(venda);
        }

        [HttpGet("ObterTodasVendas")]
        public IActionResult ObterTodos()
        {
            var vendas = _context.Vendas
                .Include(d => d.Vendedor)
                .Include(d => d.ItensVendido)
                .ToList();
            return Ok(vendas);
        }

        [HttpPut("AlterarStatus/{id}")]
        public IActionResult Atualizar(int id, Venda vendaAtualizada){
            var vendaBanco = _context.Vendas.Find(id);
            if(vendaBanco == null)
                return NotFound();
            
            if(vendaBanco.Status == EnumStatusVenda.AguardandoPagamento){
                if(vendaAtualizada.Status != EnumStatusVenda.PagamentoAprovado && vendaAtualizada.Status != EnumStatusVenda.Cancelada){
                    return BadRequest(new { Erro = "O status pode apenas ser atualizado para 'PagamentoAprovado' ou 'Cancelado'" });
                }
            }
            else if(vendaBanco.Status == EnumStatusVenda.PagamentoAprovado){
                if(vendaAtualizada.Status != EnumStatusVenda.EnviadoParaTransportador && vendaAtualizada.Status != EnumStatusVenda.Cancelada){
                    return BadRequest(new { Erro = "O status pode apenas ser atualizado para 'EnviadoParaTransportador' ou 'Cancelado'" });
                }
            }
            else if(vendaBanco.Status == EnumStatusVenda.EnviadoParaTransportador){
                if(vendaAtualizada.Status != EnumStatusVenda.Entregue ){
                    return BadRequest(new { Erro = "O status pode apenas ser atualizado para 'Entregue'" });
                }
            }
            else if(vendaBanco.Status == EnumStatusVenda.Cancelada){
                    return BadRequest(new { Erro = "O status 'Cancelado' nao pode ser alterado" });
            }
            else if(vendaBanco.Status == EnumStatusVenda.Entregue){
                    return BadRequest(new { Erro = "O status 'Entregue' nao pode ser alterado" });
            }
            
            vendaBanco.Status = vendaAtualizada.Status;
            _context.SaveChanges();
            return Ok("Status atualizado para: "+vendaBanco.Status);
        }
    }
}