﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorSozluk.Common.ViewModels;

namespace BlazorSozluk.Common.Events.EntryComment
{
    public class CreateEntryCommentVoteEvent 
    {
        public Guid EntryCommentId { get; set; }
        public VoteType VoteType { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
