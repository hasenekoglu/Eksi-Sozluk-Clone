using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;

namespace BlazorSozluk.Api.Application.Features.Command.User.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserComand,Guid>
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public UpdateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<Guid> Handle(UpdateUserComand request, CancellationToken cancellationToken)
        {
            var dbUser = await userRepository.GetByIdAsync(request.Id);
            
            if (dbUser is null)
                throw new DatabaseValidationException("User not found!");
            var dbEmailAddress = dbUser.EmailAddress;
            var emailChanged = string.CompareOrdinal(dbEmailAddress,request.EmailAddress)!=0;

            mapper.Map(request, dbUser);
            var rows = await userRepository.UpdateAsync(dbUser);

            //Check if email changed
            if (rows>0 && emailChanged)
            {
                var @event = new UserEmailChangedEvent()
                {
                    OldEmailAddress = null,
                    NewEmailAddress = dbUser.EmailAddress
                };
                QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.UserExchangeName,
                                                    exchangeType: SozlukConstants.DefaultExchangeType,
                                                    queueName: SozlukConstants.UserEmailChangedQueueName,
                                                    obj: @event);


                dbUser.EmailConfirmed = false;
                await userRepository.UpdateAsync(dbUser);
            }

            return dbUser.Id;
        }
    }
}
