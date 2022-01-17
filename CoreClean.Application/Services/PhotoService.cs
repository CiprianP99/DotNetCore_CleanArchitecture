using CoreClean.Application.Interfaces;
using CoreClean.Domain.Abstractions;
using CoreClean.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace CoreClean.Application.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PhotoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IUnitOfWork GetP()
        {
            return _unitOfWork;
        }

        public void AddPhoto(Photo photo)
        {
            _unitOfWork.Photos.Create(photo);
        }

        public void DeletePhoto(Photo photo)
        {
            _unitOfWork.Photos.Delete(photo);
        }

        public void UpdatePhoto(Photo photo)
        {
            _unitOfWork.Photos.Update(photo);
        }

        public void Save()
        {
            _unitOfWork.Complete();
        }

        public Photo Get(Guid id)
        {
            return _unitOfWork.Photos.Get(id);
        }

        public IEnumerable<Photo> GetAll()
        {
           return _unitOfWork.Photos.GetAll();
        }

        public IEnumerable<Photo> Find(Expression<Func<Photo, bool>> predicate)
        {
            return _unitOfWork.Photos.Find(predicate);
        }

       
    }
}
