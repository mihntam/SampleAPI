using SampleAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Services
{
    public interface IProductRepository
    {
        List<ProductModel> GetAll(string search, double? from, double? to, string sortBy, int page = 1);
        ProductModel GetById(string id);
        ProductModel Add(Product product);
        bool Update(string id, Product product);
        bool Delete(string id);
    }
}
