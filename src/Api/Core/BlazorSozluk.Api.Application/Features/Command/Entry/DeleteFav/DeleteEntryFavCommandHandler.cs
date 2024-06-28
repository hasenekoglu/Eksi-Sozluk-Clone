﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Infrastructure;
using MediatR;

namespace BlazorSozluk.Api.Application.Features.Command.Entry.DeleteFav
{
    public class DeleteEntryFavCommandHandler : IRequestHandler<DeleteEntryFavCommand,bool>
    {
        public async Task<bool> Handle(DeleteEntryFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName:SozlukConstants.FavExchangeName,
                exchangeType:SozlukConstants.DefaultExchangeType,
                queueName:SozlukConstants.DeleteEntryFavQueueName,obj: new DeleteEntryFavEvent()
                {
                    CreatedBy = request.UserId,
                    EntryId = request.EntryId
                });
            return await Task.FromResult(true);
        }
    }
}
