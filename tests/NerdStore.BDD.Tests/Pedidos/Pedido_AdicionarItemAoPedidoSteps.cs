using System;
using NUnit.Framework;
using TechTalk.SpecFlow;
using NerdStore.Catalogo.Domain.Entities;
using NerdStore.Core.DomainObjects;

namespace NerdStore.BDD.Tests.Pedidos
{
    [Binding]
    public class Pedido_AdicionarItemAoPedidoSteps
    {
        private Produto _produto;
        private Vendas.Domain.Pedido _pedido;
        private Vendas.Domain.PedidoItem _pedidoItem;

        [Given(@"que um produto esteja disponível no estoque")]
        public void DadoQueUmProdutoEstejaDisponivelNoEstoque()
        {
            // arrange
            _produto = new Produto("produto teste", "teste", true, 10m, Guid.NewGuid(), DateTime.Now, "imagem.png",
                new Dimensoes(1m, 1m, 1m));

            // act 
            _produto.ReporEstoque(10);

            // assert
            Assert.IsTrue(_produto.PossuiEstoque(10));
        }

        [When(@"O usuário adicionar uma ou mais unidades ao pedido")]
        public void QuandoOUsuarioAdicionarUmaOuMaisUnidadesAoPedido()
        {
            // arrange 
            _pedido = new Vendas.Domain.Pedido(Guid.NewGuid(), false, 0m, 0m);
            _pedidoItem = new Vendas.Domain.PedidoItem(_produto.Id, _produto.Nome,
                quantidade: 10,
                valorUnitario: 10m);

            // act
            _pedido.AdicionarItem(_pedidoItem);

            // assert
            Assert.IsTrue(_pedido.PedidoItemExistente(_pedidoItem));
        }

        [Then(@"O valor total do pedido será exatamente o valor do item adicionado multiplicado pela quantidade")]
        public void EntaoOValorTotalDoPedidoSeraExatamenteOValorDoItemAdicionadoMultiplicadoPelaQuantidade()
        {
            // act
            _pedido.CalcularValorPedido();

            // assert
            Assert.AreEqual(100m, _pedido.ValorTotal);
        }
        
        [When(@"O usuário adicionar um item acima da quantidade máxima permitida")]
        public void QuandoOUsuarioAdicionarUmItemAcimaDaQuantidadeMaximaPermitida()
        {
            // arrange 
            _produto.DebitarEstoque(_produto.QuantidadeEstoque);
            _produto.ReporEstoque(10);

            _pedido = new Vendas.Domain.Pedido(Guid.NewGuid(), false, 0m, 0m);
            _pedidoItem = new Vendas.Domain.PedidoItem(_produto.Id, _produto.Nome,
                quantidade: 11,
                valorUnitario: 10m);

            // act
            _pedido.AdicionarItem(_pedidoItem);

            // assert
            Assert.IsTrue(_pedido.PedidoItemExistente(_pedidoItem));
        }
        
        [Then(@"Receberá uma mensagem de erro mencionando que foi ultrapassada a quantidade limite")]
        public void EntaoReceberaUmaMensagemDeErroMencionandoQueFoiUltrapassadaAQuantidadeLimite()
        {
            // act / assert
            Assert.Throws<DomainException>(() =>  
                _produto.DebitarEstoque(_pedidoItem.Quantidade));
        }
    }
}
