using System;
using System.Collections.Generic;
using System.Text;
using TodoItem2.Model.Entities;
namespace TodoItem2.Services.Repositories
{
    public interface IItemRepository
    {
        public IEnumerable<Item> GetAll();
        public Item Create(Item item);
        public Item GetById(int id);
        public Item Update(Item item);
        public Item Delete(int id);
    }
}
