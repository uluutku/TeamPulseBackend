using HRAppBackend.Application.Dto.TokenDTOs;
using HRAppBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Application.Services.Abstract
{
    public interface ITokenService
    {
        Task<TokenDTO> CreateAccessToken(AppUser user);
        Task<string> ExtractEmailFromToken(Microsoft.Extensions.Primitives.StringValues token);

    }
}
