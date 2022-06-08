using CoreClean.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace CoreClean.Application.Interfaces
{
    public interface IAlbumService
    {
        void AddAlbum(Album album);
        void DeleteAlbum(Album album);
        void UpdateAlbum(Album album);
        void Save();

        Album Get(Guid id);
        IEnumerable<Album> GetAll();
        IEnumerable<Album> Find(Expression<Func<Album, bool>> predicate);
        IEnumerable<Album> GetAlbumByUserId(Guid id);

    }
}
