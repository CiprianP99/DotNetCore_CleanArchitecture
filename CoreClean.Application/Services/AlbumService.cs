using CoreClean.Application.Interfaces;
using CoreClean.Domain.Abstractions;
using CoreClean.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreClean.Application.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AlbumService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddAlbum(Album album)
        {
            _unitOfWork.Albums.Create(album);
        }

        public void DeleteAlbum(Album album)
        {
            _unitOfWork.Albums.Delete(album);
        }

        public void UpdateAlbum(Album album)
        {
            _unitOfWork.Albums.Update(album);
        }

        public void Save()
        {
            _unitOfWork.Complete();
        }

        public Album Get(Guid id)
        {
            return _unitOfWork.Albums.Get(id);
        }

        public IEnumerable<Album> GetAll()
        {
            return _unitOfWork.Albums.GetAll();
        }

        public IEnumerable<Album> Find(Expression<Func<Album, bool>> predicate)
        {
            return _unitOfWork.Albums.Find(predicate);
        }

        public IEnumerable<Album> GetAlbumByUserId(Guid id)
        {
            return _unitOfWork.Albums.Find(a => a.UserId == id).ToList();
        } 
    }
}
