using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CookingBox.Data.Entities;

namespace CookingBox.Data.IRepositories
{
    public interface ITasteDetailRepository
    {
        Task<IEnumerable<TasteDetail>> GetTasteDetails();
        Task<TasteDetail> GetTasteDetail(int id);
        Task InsertTasteDetail(TasteDetail tasteDetail);
        Task<bool> UpdateTasteDetail(TasteDetail tasteDetail);
        Task<bool> DeleteTasteDetail(int id);
    }
}
