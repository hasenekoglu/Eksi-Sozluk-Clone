using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Infrastructure;
using MediatR;

namespace BlazorSozluk.Api.Application.Features.Command.Entry.DeleteVote
{       
    public class DeleteEntryVoteCommandHandler :IRequestHandler<DeleteEntryVoteCommand,bool>
    {
        public  async Task<bool> Handle(DeleteEntryVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName:SozlukConstants.VoteExchangeName,
                exchangeType:SozlukConstants.DefaultExchangeType,queueName:SozlukConstants.DeleteEntryVoteQueueName,obj:new DeleteEntryVoteEvent()
                {
                    EntryId = request.EntryId,
                    CreatedBy = request.UserId
                });
            return await Task.FromResult(true);
        }
    }
}
