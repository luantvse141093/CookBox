using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CookingBox.Data.Entities;

namespace CookingBox.Data.IRepositories
{
    public interface IMetarialRepository
    {
        Task<IEnumerable<Metarial>> GetMetarials();
        Task<Metarial> GetMetarial(int id);
        Task InsertMetarial(Metarial metarial);
        Task<bool> UpdateMetarial(Metarial metarial);
        Task<bool> DeleteMetarial(int id);
    }
}
