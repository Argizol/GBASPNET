using NetStore.Models.DTO;

namespace NetStore.Abstraction
{
    public interface IProductRepository
    {
        public int AddProduct(DTOProduct product);
        public string GetProductsCSV();
        public string GetСacheStat();

        public IEnumerable<DTOProduct> GetProducts();

    }
}
