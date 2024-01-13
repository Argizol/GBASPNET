using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NetStore.Abstraction;
using NetStore.Models;
using NetStore.Models.DTO;
using System.Text;


namespace NetStore.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly ILogger _logger;
        public ProductRepository(IMapper mapper, IMemoryCache cache, ILogger logger)
        {
            _mapper = mapper;
            _cache = cache;
            _logger = logger;
        }

        public int AddProduct(DTOProduct product)
        {
            using (var context = new StoreContext())
            {
                var entityProduct = context.Products.FirstOrDefault(x => x.Name.ToLower() == product.Name.ToLower());
                if (entityProduct is null)
                {
                    entityProduct = _mapper.Map<Product>(product);
                    context.Products.Add(entityProduct);
                    context.SaveChanges();
                    _cache.Remove("products");
                }
                return entityProduct.Id;
            }
        }

        public IEnumerable<DTOProduct> GetProducts()
        {
            using (var context = new StoreContext())
            {
                if (_cache.TryGetValue("products", out List<DTOProduct> products))
                {
                    return products;
                }

                _cache.Set("products", products, TimeSpan.FromMinutes(30));

                var stat = _cache.GetCurrentStatistics().ToString();
                File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "productcache.txt"), stat);

                products = context.Groups.Select(x => _mapper.Map<DTOProduct>(x)).ToList();

                return products;
            }
        }
        public string GetProductsCSV()
        {
            var sb = new StringBuilder();
            var products = GetProducts();

            foreach (var product in products)
            {
                sb.AppendLine($"{product.Id},{product.Name}, {product.Description}");
            }

            return sb.ToString();
        }

        public string GetСacheStatCSV()
        {
            var result = _cache.GetCurrentStatistics().ToString();

            return result;

        }
    }
}
