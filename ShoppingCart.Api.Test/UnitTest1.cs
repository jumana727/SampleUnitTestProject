using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using sample_api.Controllers;
using sample_api.Interface;
using sample_api.Services;
using sample_api.Models;


namespace ProjectNamespace.Test
{
    [TestClass]
    public class ClassNameTest
    {
        IProductsReference _repository;
        ProductsController controller;
        public ClassNameTest()
        {
            _repository = A.Fake<IProductsReference>();
            controller = new ProductsController(_repository);
        }
        [TestMethod]
        public void MethodName()
        {
            
            A.CallTo(() => _repository.GetById("12")).Returns( new Product() { Id = new string("12"),
                    Name = "Orange Juice", Manufacturer="Orange Tree", Price = 5.00M });
            
            IActionResult res = controller.Get("12");
            
            var result = res as OkObjectResult;
 
            var prod = result.Value as Product;

            prod.Id.Should().Be("12");
        }

        [TestMethod]
        public void ProductsController_GetAllProducts_testApi()
        {
            A.CallTo(() => _repository.GetAllItems()).Returns( new List<Product>(){
                 new Product() { Id = new string("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                    Name = "Orange Juice", Manufacturer="Orange Tree", Price = 5.00M },
                new Product() { Id = new string("815accac-fd5b-478a-a9d6-f171a2f6ae7f"),
                    Name = "Diary Milk", Manufacturer="Cow", Price = 4.00M },
                new Product() { Id = new string("33704c4a-5b87-464c-bfb6-51971b4d18ad"),
                    Name = "Frozen Pizza", Manufacturer="Uncle Mickey", Price = 12.00M }
            });
            IActionResult result = controller.GetProducts();
            var resultObj = result as OkObjectResult;
            var products = resultObj.Value as List<Product>;
            products.Count().Should().Be(3);
        }

        [TestMethod]
        public void TestProductService()
        {
            IProductsReference product = new ProductsService();
            IEnumerable<Product> products = product.GetAllItems();
            List<Product> cart = ProductsService._shoppingCart;
            int count = cart.Count();
            count.Should().Be(3);
            product.Should().NotBeNull();
        }


    }
}
