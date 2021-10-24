using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CookingBox.Data.Entities;

namespace CookingBox.Data.IRepositories
{
    public interface ITasteRepository
    {
        Task<IEnumerable<Taste>> GetTastes();
        Task<Taste> GetTaste(int id);
        Task InsertTaste(Taste taste);
        Task<bool> UpdateTaste(Taste taste);
        Task<bool> DeleteTaste(int id);
    }
}
