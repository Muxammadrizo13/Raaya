using ECommerce.Domain.Commons;
using ECommerce.Domain.Entities;
using ECommmerce.Data.Configurations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ECommmerce.Data.Repositories
{
    public class GenericRepository<Tentity> : IRepositories.GenericRepository<Tentity> where Tentity : Auditable
    {
        private string path;
        public GenericRepository()
        {
            if (typeof(Tentity) == typeof(Order))
            {
                path = Database.ORDER_PATH;
            }
            else if (typeof(Tentity) == typeof(Cart))
            {
                path = Database.CART_PATH;
            }
            else if (typeof(Tentity) == typeof(Clothes))
            {
                path = Database.CLOTHES_PATH;
            }
            else if (typeof(Tentity) == typeof(User))
            {
                path = Database.USER_PATH;
            }
            else if (typeof(Tentity) == typeof(Payment))
            {
                path = Database.PAYMENT_PATH;
            }
            else
            {
                throw new NotSupportedException("bunday tipdagi model yo'q");
            }

        }
        public async Task<Tentity> CreateAsync(Tentity model)
        {

            var EnteringValues = await GetAllAsync(x => x.Id > 0);
            EnteringValues.Add(model);
            var WritingObject = JsonConvert.SerializeObject(EnteringValues, Formatting.Indented);
            await File.WriteAllTextAsync(path, WritingObject);
            return model;
        }

       
        public async Task<bool> DeleteAysnc(Predicate<Tentity> predicate)
        {
            var EnteringValues = await GetAllAsync(predicate);
            var FindingValue = EnteringValues.FirstOrDefault(x => predicate(x));
            if (FindingValue is null)
            {
                return false;
            }
            EnteringValues.Remove(FindingValue);
            string res = JsonConvert.SerializeObject(EnteringValues, Formatting.Indented);
            await File.WriteAllTextAsync(path, res);
            return true;
        }

        public async Task<List<Tentity>> GetAllAsync(Predicate<Tentity> predicate)
        {
            string models = await File.ReadAllTextAsync(path);
            if (string.IsNullOrEmpty(models))
                models = "[]";
            List<Tentity> ReturningValues = JsonConvert.DeserializeObject<List<Tentity>>(models);
            return ReturningValues.FindAll(predicate);
        }

        public async Task<Tentity> GetByIdAsync(Predicate<Tentity> predicate)
        {
            var EnteringValues = await GetAllAsync(x => predicate(x));
            var FindingValue = EnteringValues.FirstOrDefault(x => predicate(x));
            return FindingValue;
        }

        public async Task<Tentity> UpdateAsync(Predicate<Tentity> predicate, Tentity model)
        {
            var EnteringValues = await GetAllAsync(x => predicate(x));
            var FindingValue = EnteringValues.FirstOrDefault(x => predicate(x));
            if (model is not null)
            {
                int index = EnteringValues.IndexOf(FindingValue);
                FindingValue.UpdatedAt = DateTime.UtcNow;
                EnteringValues.Remove(FindingValue);
                EnteringValues.Insert(index, model);
                var json = JsonConvert.SerializeObject(EnteringValues, Formatting.Indented);
                await File.WriteAllTextAsync(path, json);
                return model;
            }
            return null;
        }
    }
}