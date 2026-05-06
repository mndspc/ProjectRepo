using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class AdminInfoService : IAdminInfoService<AdminInfo>
    {
        public async Task<bool> ValidateAdmin(AdminInfo entity)
        {
            using (EcommContext dbContext=new EcommContext())
            {
                return await dbContext.AdminInfos.AnyAsync(user=>user.EmailId.Equals(entity.EmailId) && user.Password.Equals(entity.Password));
            }
        }
    }
}
