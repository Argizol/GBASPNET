using AutoMapper;
using NetStore.Abstraction;
using NetStore.Models;
using NetStore.Models.DTO;

namespace NetStore.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;

        public ProductRepository(IMapper mapper)
        {
            _mapper = mapper;
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
                }
                return entityProduct.Id;
            }
        }

        public IEnumerable<DTOProduct> GetProducts()
        {
            using (var context = new StoreContext())
            {
                var products = context.Groups.Select(x => _mapper.Map<DTOProduct>(x)).ToList();
                
                return products;
            }
        }
    }
}
