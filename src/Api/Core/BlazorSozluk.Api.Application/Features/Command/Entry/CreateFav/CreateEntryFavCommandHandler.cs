using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Events.EntryComment;
using BlazorSozluk.Common.Infrastructure;
using MediatR;

namespace BlazorSozluk.Api.Application.Features.Command.Entry.CreateFav
{
    public class CreateEntryFavCommandHandler : IRequestHandler<CreateEntryFavCommand,bool>
    {


        public Task<bool> Handle(CreateEntryFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName:SozlukConstants.FavExchangeName,
                                            exchangeType:SozlukConstants.DefaultExchangeType,
                                            queueName:SozlukConstants.CreateEntryFavQueueName,
                                            obj: new CreateEntryFavEvent()
                                            {
                                                EntryId = request.EntryId.Value,
                                                CreatedBy = request.UserId.Value
                                                
                                            });

            return Task.FromResult(true);
        }
    }
}
