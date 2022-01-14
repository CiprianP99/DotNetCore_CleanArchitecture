using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClean.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        IPhotoRepository Photos { get; }
        int Complete();
    }
}
