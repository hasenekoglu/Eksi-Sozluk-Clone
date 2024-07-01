using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using MediatR;

namespace BlazorSozluk.Api.Application.Features.Queries.GetMainPageEntries
{
    public class GetMainPageEntriesQuery : BasePagedQuery,IRequest<PagedViewModel<GetEntryDetailViewModel>>
    {
        public Guid? UserId { get; set; }

        public GetMainPageEntriesQuery(Guid? userId,int page, int pageSize) : base(page, pageSize)
        {
            UserId = userId;
        }
    }
}
