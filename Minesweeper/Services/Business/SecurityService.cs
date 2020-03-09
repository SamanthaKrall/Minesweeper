using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Minesweeper.Services.Business
{
    public class SecurityService
    {
        public bool Authenticate(Models.UserModel user)
        {
            Data.SecurityDAO service = new Data.SecurityDAO();
            return service.FindByUser(user);
        }
        public bool Insert(Models.UserModel user)
        {
            Data.SecurityDAO insert = new Data.SecurityDAO();
            return insert.NewUser(user);
        }
    }
}