using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CeApp.DataAccess;
using CeApp.DataObjects.Order;
using CeApp.Services.Product;
using CeApp.Services.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
using CollectionAssert = Microsoft.VisualStudio.TestTools.UnitTesting.CollectionAssert;

namespace CeApp.Services.Tests.Product
{
    [TestClass]
    public class ProductServiceTests
    {
        [TestCaseSource(typeof(Top5ProductsDataSet), nameof(Top5ProductsDataSet.SuccessCases))]
        public async Task Get5TopProductsAsync_ValidData_SuccessResult(Top5ProductsDataSet input)
        {
            var productService = PrepareSuccessCaseService(input.Products, input.Orders);
            var (status, topProducts) = await productService.Get5TopProductsAsync();

            Assert.AreEqual(ResultStatus.Success, status);
            var result = topProducts.Select(p => (p.Product.MerchantProductNo, p.TotalQuantity)).ToList();
            CollectionAssert.AreEquivalent(input.Result.ToList(), result);
        }

        [TestMethod]
        public async Task Get5TopProductsAsync_OrderProviderThrowsException_ErrorResult()
        {
            var productProviderMock = new Mock<IProductProvider>();
            var orderProviderMock = new Mock<IOrderProvider>();
            orderProviderMock
                .Setup(p => p.GetOrdersAsync(It.IsAny<IDictionary<string, string>>()))
                .Throws<Exception>();

            var offerProviderMock = new Mock<IOfferProvider>();

            var service = new ProductService(productProviderMock.Object, orderProviderMock.Object, offerProviderMock.Object);

            var status = (await service.Get5TopProductsAsync()).Status;

            Assert.AreEqual(ResultStatus.UnknownError, status);
        }

        [TestMethod]
        public async Task Get5TopProductsAsync_OrdersProviderReturnsNull_ErrorResult()
        {
            var productProviderMock = new Mock<IProductProvider>();
            var orderProviderMock = new Mock<IOrderProvider>();
            orderProviderMock
                .Setup(p => p.GetOrdersAsync(It.IsAny<IDictionary<string, string>>()))
                .Returns(Task.FromResult((IEnumerable<DataObjects.Order.Order>)null));

            var offerProviderMock = new Mock<IOfferProvider>();

            var service = new ProductService(productProviderMock.Object, orderProviderMock.Object, offerProviderMock.Object);

            var status = (await service.Get5TopProductsAsync()).Status;

            Assert.AreEqual(ResultStatus.UnknownError, status);
        }

        public class Top5ProductsDataSet
        {
            public IEnumerable<DataObjects.Product.Product> Products { get; set; }
            public IEnumerable<DataObjects.Order.Order> Orders { get; set; }
            public IEnumerable<(string ProductNo, int Quantity)> Result { get; set; }

            public static List<Top5ProductsDataSet> SuccessCases() => new List<Top5ProductsDataSet>
            {
                new Top5ProductsDataSet
                {
                    Products = new List<DataObjects.Product.Product>
                    {
                        new DataObjects.Product.Product {MerchantProductNo = "111"},
                        new DataObjects.Product.Product {MerchantProductNo = "222"},
                        new DataObjects.Product.Product {MerchantProductNo = "333"}
                    },
                    Orders = new List<DataObjects.Order.Order>
                    {
                        new DataObjects.Order.Order
                        {
                            Lines = new List<Line>
                            {
                                new Line {MerchantProductNo = "111", Quantity = 5},
                                new Line {MerchantProductNo = "222", Quantity = 12}
                            }
                        },
                        new DataObjects.Order.Order
                        {
                            Lines = new List<Line>
                            {
                                new Line {MerchantProductNo = "222", Quantity = 10},
                                new Line {MerchantProductNo = "333", Quantity = 28}
                            }
                        },
                    },
                    Result = new List<(string ProductNo, int Quantity)>
                    {
                        ("333", 28),
                        ("222", 22),
                        ("111", 5)
                    }
                },
                new Top5ProductsDataSet
                {
                    Products = new List<DataObjects.Product.Product>
                    {
                        new DataObjects.Product.Product {MerchantProductNo = "111"},
                        new DataObjects.Product.Product {MerchantProductNo = "222"},
                        new DataObjects.Product.Product {MerchantProductNo = "333"},
                        new DataObjects.Product.Product {MerchantProductNo = "444"},
                        new DataObjects.Product.Product {MerchantProductNo = "555"},
                        new DataObjects.Product.Product {MerchantProductNo = "666"},
                        new DataObjects.Product.Product {MerchantProductNo = "777"}
                    },
                    Orders = new List<DataObjects.Order.Order>
                    {
                        new DataObjects.Order.Order
                        {
                            Lines = new List<Line>
                            {
                                new Line {MerchantProductNo = "222", Quantity = 12},
                                new Line {MerchantProductNo = "333", Quantity = 16},
                                new Line {MerchantProductNo = "444", Quantity = 2}
                            }
                        },
                        new DataObjects.Order.Order
                        {
                            Lines = new List<Line>
                            {
                                new Line {MerchantProductNo = "555", Quantity = 10},
                                new Line {MerchantProductNo = "777", Quantity = 25}
                            }
                        },
                        new DataObjects.Order.Order
                        {
                            Lines = new List<Line>()
                        },
                        new DataObjects.Order.Order
                        {
                            Lines = new List<Line>
                            {
                                new Line {MerchantProductNo = "222", Quantity = 7},
                                new Line {MerchantProductNo = "444", Quantity = 3},
                                new Line {MerchantProductNo = "555", Quantity = 28},
                                new Line {MerchantProductNo = "666", Quantity = 1}
                            }
                        }
                    },
                    Result = new List<(string ProductNo, int Quantity)>
                    {
                        ("555", 38),
                        ("777", 25),
                        ("222", 19),
                        ("333", 16),
                        ("444", 5)
                    }
                },
                new Top5ProductsDataSet
                {
                    Products = new List<DataObjects.Product.Product>
                    {
                        new DataObjects.Product.Product {MerchantProductNo = "111"},
                        new DataObjects.Product.Product {MerchantProductNo = "222"},
                        new DataObjects.Product.Product {MerchantProductNo = "333"}
                    },
                    Orders = new List<DataObjects.Order.Order>(),
                    Result = new List<(string ProductNo, int Quantity)>()
                }
            };
        }


        private static ProductService PrepareSuccessCaseService(IEnumerable<DataObjects.Product.Product> products,
            IEnumerable<DataObjects.Order.Order> orders)
        {
            var productProviderMock = new Mock<IProductProvider>();
            productProviderMock
                .Setup(p => p.GetProductsAsync(It.IsAny<IDictionary<string, string>>()))
                .Returns(Task.FromResult(products));

            var orderProviderMock = new Mock<IOrderProvider>();
            orderProviderMock
                .Setup(p => p.GetOrdersAsync(It.IsAny<IDictionary<string, string>>()))
                .Returns(Task.FromResult(orders));

            var offerProviderMock = new Mock<IOfferProvider>();

            return new ProductService(productProviderMock.Object, orderProviderMock.Object, offerProviderMock.Object);
        }
    }
}