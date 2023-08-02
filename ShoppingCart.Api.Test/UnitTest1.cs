using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using sample_api.Controllers;
using sample_api.Interface;
using sample_api.Services;
using sample_api.Models;

[assembly: Parallelize(Workers = 9, Scope = ExecutionScope.MethodLevel)]
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
            //Arrange
            A.CallTo(() => _repository.GetById("12")).Returns( new Product() { Id = new string("12"),
                    Name = "Orange Juice", Manufacturer="Orange Tree", Price = 5.00M });
            //Act
            IActionResult res = controller.Get("12");
            
            var result = res as OkObjectResult;
 
            var prod = result.Value as Product;
            //Assert
            prod.Id.Should().Be("12");
        }

        [TestMethod]
        public void ProductsController_GetAllProducts_testApi()
        {
            //Arrange
            A.CallTo(() => _repository.GetAllItems()).Returns( new List<Product>(){
                 new Product() { Id = new string("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                    Name = "Orange Juice", Manufacturer="Orange Tree", Price = 5.00M },
                new Product() { Id = new string("815accac-fd5b-478a-a9d6-f171a2f6ae7f"),
                    Name = "Diary Milk", Manufacturer="Cow", Price = 4.00M },
                new Product() { Id = new string("33704c4a-5b87-464c-bfb6-51971b4d18ad"),
                    Name = "Frozen Pizza", Manufacturer="Uncle Mickey", Price = 12.00M }
            });
            //Act
            IActionResult result = controller.GetProducts();
            var resultObj = result as OkObjectResult;
            var products = resultObj.Value as List<Product>;
            //Assert
            products.Count().Should().Be(3);
        }

        [TestMethod]
        public void TestProductService()
        {
            //Arrange
            IProductsReference product = new ProductsService();
            IEnumerable<Product> products = product.GetAllItems();
            List<Product> cart = ProductsService._shoppingCart;
            //Act
            int count = cart.Count();
            //Assert
            count.Should().Be(3);
            product.Should().NotBeNull();
        }

        [DataTestMethod]
        [DataRow("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200","Orange Juice")]
        [DataRow("815accac-fd5b-478a-a9d6-f171a2f6ae7f","Diary Milk")]
        [DataRow("33704c4a-5b87-464c-bfb6-51971b4d18ad","Frozen Pizza")]
        public void ProductsService_GetById_Should_Return_Product_object(string productId,string expectedProductName)
        {
            //Arrange
            IProductsReference productService = new ProductsService();  
            //Act
            Product product = productService.GetById(productId);
            //Assert
            product.Should().NotBeNull();
            product.Name.Should().Be(expectedProductName);
        }

        [TestMethod]
        [DynamicData(nameof(ProductsData))]
        public void ProductsService_GetById_test(string productId,string expectedProductName)
        {
            //Arrange
            IProductsReference productService = new ProductsService();  
            //Act
            Product product = productService.GetById(productId);
            //Assert
            product.Should().NotBeNull();
            product.Name.Should().Be(expectedProductName);
        }

        public static IEnumerable<object[]> ProductsData
        {
            get
            {
                return new[]
                { 
                    new object[] { "ab2bd817-98cd-4cf3-a80a-53ea0cd9c200","Orange Juice" },
                    new object[] { "815accac-fd5b-478a-a9d6-f171a2f6ae7f","Diary Milk" },
                    new object[] {"33704c4a-5b87-464c-bfb6-51971b4d18ad","Frozen Pizza"},
                };
            }
        }



    }
}
