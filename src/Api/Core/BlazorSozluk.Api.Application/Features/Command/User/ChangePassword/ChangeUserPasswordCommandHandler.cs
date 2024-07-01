using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using MediatR;

namespace BlazorSozluk.Api.Application.Features.Command.User.ChangePassword
{
    public class ChangeUserPasswordCommandHandler :IRequestHandler<ChangeUserPasswordComand,bool>
    {
        private readonly IUserRepository _userRepository;

        public ChangeUserPasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public  async Task<bool> Handle(ChangeUserPasswordComand request, CancellationToken cancellationToken)
        {
            if (!request.UserId.HasValue)
                throw new ArgumentException(nameof(request.UserId));

            var dbUser = await _userRepository.GetByIdAsync(request.UserId.Value);
            if (dbUser is null)
                throw new DatabaseValidationException("User not found");

            var encPass = PasswordEncryptor.Encrpt(request.NewPassword);
            if (dbUser.Password != encPass)
                throw new DatabaseValidationException("Old password wrong!");

            dbUser.Password = encPass;
            await _userRepository.UpdateAsync(dbUser);
            
            return true;
        }
    }
}
