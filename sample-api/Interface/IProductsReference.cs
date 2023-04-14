using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sample_api.Models;


namespace sample_api.Interface
{
    public interface IProductsReference
    {
        static List<Product> _shoppingCart;
        IEnumerable<Product> GetAllItems();
        Product Add(Product newItem);
        Product GetById(string id);
        void Remove(string id);
    }
}