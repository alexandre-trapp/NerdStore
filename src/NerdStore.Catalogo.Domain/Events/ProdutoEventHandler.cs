using MediatR;
using System.Threading;
using System.Threading.Tasks;
using NerdStore.Catalogo.Domain.Repositories;

namespace NerdStore.Catalogo.Domain.Events
{
    public class ProdutoEventHandler : INotificationHandler<ProdutoAbaixoEstoqueEvent>
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoEventHandler(IProdutoRepository produtoRepository) => 
            _produtoRepository = produtoRepository;

        public async Task Handle(ProdutoAbaixoEstoqueEvent mensagem, CancellationToken cancellationToken)
        {
            var produto = _produtoRepository.ObterPorId(mensagem.AggregateId);

            // enviar um email para aquisição de mais produtos;
        }
    }
}
