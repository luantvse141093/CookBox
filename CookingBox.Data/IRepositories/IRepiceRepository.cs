using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CookingBox.Data.Entities;

namespace CookingBox.Data.IRepositories
{
    public interface IRepiceRepository
    {
        Task<IEnumerable<Repice>> GetRepices();
        Task<Repice> GetRepice(int id);
        Task InsertRepice(Repice repice);
        Task<bool> UpdateRepice(Repice repice);
        Task<bool> DeleteRepice(int id);
    }
}
