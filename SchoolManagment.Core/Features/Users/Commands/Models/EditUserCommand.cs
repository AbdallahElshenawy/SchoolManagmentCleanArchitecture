﻿using MediatR;
using SchoolManagment.Core.Bases;

namespace SchoolManagment.Core.Features.Users.Commands.Models
{
    public class EditUserCommand : IRequest<Response<string>>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
