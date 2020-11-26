using MediatR;
using System.Threading.Tasks;
using NerdStore.Core.Messages;

namespace NerdStore.Core
{
    public class MediatrHandler : IMediatrHandler
    {
        private readonly IMediator _mediator;

        public MediatrHandler(IMediator mediator) => 
            _mediator = mediator;

        public async Task PublicarEvento<T>(T evento) where T : Event
        {
            await _mediator.Publish(evento);
        }
    }
}
