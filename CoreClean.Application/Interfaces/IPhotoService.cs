using CoreClean.Domain.Abstractions;
using CoreClean.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreClean.Application.Interfaces
{
    public interface IPhotoService
    { 
        //IUnitOfWork GetP();

        void AddPhoto(Photo photo);
        void DeletePhoto(Photo photo);
        void UpdatePhoto(Photo photo);
        void Save();

        Photo Get(Guid id);
        IEnumerable<Photo> GetAll();
        IEnumerable<Photo> Find(Expression<Func<Photo, bool>> predicate);
    }
}
