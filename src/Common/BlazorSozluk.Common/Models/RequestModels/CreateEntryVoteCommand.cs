using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorSozluk.Common.ViewModels;
using MediatR;

namespace BlazorSozluk.Common.Models.RequestModels
{
    public class CreateEntryVoteCommand : IRequest<bool>
    {
        public Guid EntryId { get; set; }
        public Guid CreatedBy { get; set; }


        public VoteType VoteType { get; set; }
    }
}
