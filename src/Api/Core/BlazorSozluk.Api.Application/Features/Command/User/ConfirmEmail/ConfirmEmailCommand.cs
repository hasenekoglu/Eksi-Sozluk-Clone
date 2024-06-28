﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using MediatR;

namespace BlazorSozluk.Api.Application.Features.Command.User.ConfirmEmail
{
     public class ConfirmEmailCommand : IRequest<bool>
    {
        public Guid ConfirmationId { get; set; }

    }

     public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand,bool>
     {
         private readonly IUserRepository _userRepository;
         private readonly IEmailConfirmationRepository _emailConfirmationRepository;

         public ConfirmEmailCommandHandler(IUserRepository userRepository, IEmailConfirmationRepository emailConfirmationRepository)
         {
             _userRepository = userRepository;
             _emailConfirmationRepository = emailConfirmationRepository;
         }

         public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
         {
             var confirmation = await _emailConfirmationRepository.GetByIdAsync(
                 request.ConfirmationId);
             
             if (confirmation == null)
                    throw new DatabaseValidationException("Confirmation not found!");

             var dbUser = await _userRepository.GetSingleAsync(i => i.EmailAddress == confirmation.NewEmailAddress);

             if(dbUser is null)
                 throw new DatabaseValidationException("User not found with this email!");

             if (dbUser.EmailConfirmed)
                 throw new DatabaseValidationException("Email address is already confirmed!");

             dbUser.EmailConfirmed = true;
             await _userRepository.UpdateAsync(dbUser);

             return true;
         }

     }
}
