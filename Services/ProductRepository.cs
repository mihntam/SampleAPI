using Microsoft.EntityFrameworkCore;
using SampleAPI.Data;
using SampleAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyDbContext _context;
        public static int PAGE_SIZE { get; set; } = 5;

        public ProductRepository(MyDbContext context)
        {
            _context = context;
        }
        public List<ProductModel> GetAll(string search, double? from, double? to, string sortBy, int page = 1)
        {
            var allProducts = _context.Products
                .Include(product => product.Category)
                .AsQueryable();

            #region Search
            if (!string.IsNullOrEmpty(search))
            {
                allProducts = allProducts.Where(product =>
                    product.ProductName.Contains(search)
                );
            }

            #endregion

            #region Filter
            if (from.HasValue)
            {
                allProducts = allProducts.Where(product => product.Price >= from);
            }
            if (to.HasValue)
            {
                allProducts = allProducts.Where(product => product.Price <= to);
            }

            #endregion

            #region Sort
            allProducts = allProducts.OrderBy(product => product.ProductName);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "name_desc":
                        allProducts = allProducts.OrderByDescending(product => product.ProductName);
                        break;

                    case "price_asc":
                        allProducts = allProducts.OrderBy(product => product.Price);
                        break;

                    case "price_desc":
                        allProducts = allProducts.OrderByDescending(product => product.Price);
                        break;

                    default:
                        break;
                }
            }
            #endregion

            #region Page
            var result = PaginatedList<Data.Product>.Create(allProducts, page, PAGE_SIZE);

            #endregion

            var productList = result.Select(product => new ProductModel
            {
                Id = product.ProductID,
                Name = product.ProductName,
                Image = product.Image,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryID,
                CategoryName = product.Category?.Name

            }).ToList();

            return productList;
        }

        public ProductModel GetById(string id)
        {
            var product = _context.Products.Include(product => product.Category).SingleOrDefault(item => item.ProductID == Guid.Parse(id));
            if (product != null)
            {
                var result = new ProductModel
                {
                    Id = product.ProductID,
                    Name = product.ProductName,
                    Image = product.Image,
                    Description = product.Description,
                    CategoryId = product.CategoryID,
                    CategoryName = product.Category?.Name,
                    Price = product.Price
                };

                return result;
            }

            return null;
        }

        public ProductModel Add(Models.Product product)
        {
            var _product = new Data.Product
            {
                ProductID = Guid.NewGuid(),
                ProductName = product.Name,
                Image = product.Image,
                Description = product.Description,
                CategoryID = product.CategoryId,
                Price = product.Price,
            };
            _context.Add(_product);
            _context.SaveChanges();

            var catName = _context.Categories.SingleOrDefault(i => i.CategoryID == product.CategoryId);

            var result = new ProductModel
            {
                Id = _product.ProductID,
                Name = _product.ProductName,
                Image = _product.Image,
                Description = _product.Description,
                Price = _product.Price,
                CategoryId = _product.CategoryID,
                CategoryName = catName.Name,
            };

            return result;
        }

        public bool Update(string id, Models.Product product)
        {
            var _product = _context.Products.SingleOrDefault(item => item.ProductID == Guid.Parse(id));
            if (product == null)
            {
                return false;
            }

            _product.ProductName = product.Name;
            _product.Image = product.Image;
            _product.Description = product.Description;
            _product.Price = product.Price;

            _context.SaveChanges();

            return true;
        }

        public bool Delete(string id)
        {
            var product = _context.Products.SingleOrDefault(item => item.ProductID == Guid.Parse(id));
            if (product == null)
            {
                return false;
            }

            _context.Remove(product);
            _context.SaveChanges();

            return true;
        }
    }
}
