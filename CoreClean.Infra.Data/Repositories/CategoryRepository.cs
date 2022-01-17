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
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ProjectDbContext projectDbContext)
                : base(projectDbContext)
        {
        }
    }
    
}
