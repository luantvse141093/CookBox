using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookingBox.Data.Entities;


namespace CookingBox.Data.IRepositories
{
    public interface IDishRepository
    {
        Task<IEnumerable<Dish>> GetDishes();
        Task<Dish> GetDish(int id);
        Task InsertDish(Dish Dish);
        Task<bool> UpdateDish(Dish Dish);
        Task<bool> DeleteDish(int id);
    }
}
