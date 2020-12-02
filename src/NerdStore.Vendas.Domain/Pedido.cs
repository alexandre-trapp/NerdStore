using System;
using System.Linq;
using System.Collections.Generic;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Vendas.Domain
{
    public class Pedido : Entity, IAggregateRoot
    {
        public int Codigo { get; private set; }
        public Guid ClienteId { get; private set; }
        public Guid? VoucherId { get; set; }
        public bool VoucherUtilizado { get; set; }
        public decimal Desconto { get; private set; }
        public decimal ValorTotal { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public PedidoStatus PedidoStatus { get; private set; }

        private readonly List<PedidoItem> _pedidoItems;

        // EF Relation.
        public Voucher Voucher { get; private set; }
        public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems;

        public Pedido(
            Guid clienteId, 
            bool voucherUtilizado, 
            decimal desconto, 
            decimal valorTotal)
        {
            ClienteId = clienteId;
            VoucherUtilizado = voucherUtilizado;
            Desconto = desconto;
            ValorTotal = valorTotal;

            _pedidoItems = new List<PedidoItem>();
        }

        protected Pedido() => _pedidoItems = new List<PedidoItem>();

        public void AplicarVoucher(Voucher voucher)
        {
            Voucher = voucher;
            VoucherUtilizado = true;
            CalcularValorPedido();
        }

        public void CalcularValorPedido()
        {
            ValorTotal = PedidoItems.Sum(p => p.CalcularValor());
            CalcularValorTotalDesconto();
        }

        public void CalcularValorTotalDesconto()
        {
            if (!VoucherUtilizado)
                return;

            decimal desconto = 0;
            var valor = ValorTotal;

            if (Voucher.TipoDescontoVoucher == TipoDescontoVoucher.Porcentagem)
            {
                if (Voucher.Percentual.HasValue)
                {
                    desconto = (valor * Voucher.Percentual.Value) / 100;
                    valor -= desconto;
                }
            }
            else
            {
                if (Voucher.ValorDesconto.HasValue)
                {
                    desconto = Voucher.ValorDesconto.Value;
                    valor -= desconto;
                }
            }

            ValorTotal = valor < 0 ? 0 : valor;
            Desconto = desconto;
        }

        public bool PedidoItemExistente(PedidoItem item) =>
            _pedidoItems.Any(p => p.ProdutoId == item.ProdutoId);

        public void AdicionarItem(PedidoItem item)
        {
            if (!item.Valido())
                return;

            item.AssociarPedido(Id);

            if (PedidoItemExistente(item))
                item = ObterPedidoItemExistenteERemoverDaLista(item);

            item.CalcularValor();
            _pedidoItems.Add(item);

            CalcularValorPedido();
        }

        private PedidoItem ObterPedidoItemExistenteERemoverDaLista(PedidoItem item)
        {
            var itemExistente = _pedidoItems.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);
            itemExistente.AdicionarUnidades(item.Quantidade);

            _pedidoItems.Remove(itemExistente);

            return itemExistente;
        }

        public void RemoverItem(PedidoItem item)
        {
            if (!item.Valido())
                return;

            var itemExistente = PedidoItems.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);

            if (itemExistente == null)
                throw new DomainException("O item não pertence ao pedido.");

            _pedidoItems.Remove(itemExistente);

            CalcularValorPedido();
        }
    }
}
