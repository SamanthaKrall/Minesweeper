using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Minesweeper.Models;
using Minesweeper.Services.Data;

namespace Minesweeper.Services.Business
{
    public class SecurityService
    {
        public bool Authenticate(Models.UserModel user)
        {
            Data.SecurityDAO service = new Data.SecurityDAO();
            return service.FindByUser(user);
        }

        public string Register(RegisterModel user)
        {
            SecurityDAO service = new SecurityDAO();
            return service.Create(user);
        }
    }
}