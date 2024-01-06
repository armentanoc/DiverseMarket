using SalesApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.DomainLayer.Service
{
    public static class UserService
    {
        public static string GetUserFullNameById(long userId)
        {
            return UserDB.GetUserFullNameById(userId);
        }
    }
}
