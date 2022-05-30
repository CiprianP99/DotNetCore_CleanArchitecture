using CoreClean.Domain.Abstractions;
using CoreClean.Domain.Models;
using CoreClean.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClean.Infra.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ProjectDbContext projectDbContext)
            :base(projectDbContext)
        {

        }
    }

 
}
