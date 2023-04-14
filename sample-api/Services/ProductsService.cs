using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sample_api.Interface;
using sample_api.Models;

namespace sample_api.Services
{
    public class ProductsService : IProductsReference
    {
            public static  List<Product> _shoppingCart;
    public ProductsService()
    {
        _shoppingCart = new List<Product>()
            {
                new Product() { Id = new string("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                    Name = "Orange Juice", Manufacturer="Orange Tree", Price = 5.00M },
                new Product() { Id = new string("815accac-fd5b-478a-a9d6-f171a2f6ae7f"),
                    Name = "Diary Milk", Manufacturer="Cow", Price = 4.00M },
                new Product() { Id = new string("33704c4a-5b87-464c-bfb6-51971b4d18ad"),
                    Name = "Frozen Pizza", Manufacturer="Uncle Mickey", Price = 12.00M }
            };
    }
public IEnumerable<Product> GetAllItems()
    {
        return _shoppingCart;
    }
    public Product Add(Product newItem)
    {
        Random rand = new Random();
        
        newItem.Id = rand.Next(100000) + "dhcfi";
        _shoppingCart.Add(newItem);
        return newItem;
    }
    public Product GetById(string id)
    {
        return _shoppingCart.Where(a => a.Id == id).FirstOrDefault();
    }
    public void Remove(string id)
    {
        var existing = _shoppingCart.First(a => a.Id == id);
        _shoppingCart.Remove(existing);
    }
    }
}