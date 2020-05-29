using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using ToysNGames.Data;
using ToysNGames.Data.Entities;

namespace ToysNGames.test
{
    [TestClass]
    public class RepositoryTest
    {
        #region AddProduct

        [TestMethod]
        public async Task AddProduct_ReturnsTrue()
        {
            //Arrange            
            Mock<DBContext> dbContext = new Mock<DBContext>(LoadContextOptions());
            Mock<ILogger<ProductRepository>> logger = new Mock<ILogger<ProductRepository>>();
            dbContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(10);
            IProductRepository repository = new ProductRepository(dbContext.Object, logger.Object);
           
            //Act
            var result = await repository.Add(new Product() { Id = 10, Name = "test", AgeRestriction = 10, Company = "testing", Description = "none", Price = 89.98m });

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task AddProduct_ReturnsFalse()
        {
            //Arrange
            Mock<DBContext> dbContext = new Mock<DBContext>(LoadContextOptions());            
            Mock<ILogger<ProductRepository>> logger = new Mock<ILogger<ProductRepository>>();
            dbContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);
            IProductRepository repository = new ProductRepository(dbContext.Object, logger.Object);
            
            //Act
            var result = await repository.Add(new Product() { Id = 10, Name = "test", AgeRestriction = 10, Company = "testing", Description = "none", Price = 89.98m });

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task AddProduct_Exception_ReturnsFalse()
        {
            //Arrange
            Mock<DBContext> dbContext = new Mock<DBContext>(LoadContextOptions());
            Mock<ILogger<ProductRepository>> logger = new Mock<ILogger<ProductRepository>>();
            dbContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).Throws(new ArgumentNullException());
            IProductRepository repository = new ProductRepository(dbContext.Object, logger.Object);

            //Act
            var result = await repository.Add(null);

            //Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region UpdateProduct

        [TestMethod]
        public async Task UpdateProduct_ReturnsTrue()
        {
            //Arrange            
            Mock<DBContext> dbContext = new Mock<DBContext>(LoadContextOptions());
            Mock<ILogger<ProductRepository>> logger = new Mock<ILogger<ProductRepository>>();
            dbContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(2);
            IProductRepository repository = new ProductRepository(dbContext.Object, logger.Object);

            //Act
            var result = await repository.Update(new Product() { Id = 2, Name = "test", AgeRestriction = 10, Company = "testing", Description = "none", Price = 89.98m });

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task UpdateProduct_ReturnsFalse()
        {
            //Arrange            
            Mock<DBContext> dbContext = new Mock<DBContext>(LoadContextOptions());
            Mock<ILogger<ProductRepository>> logger = new Mock<ILogger<ProductRepository>>();
            dbContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);
            IProductRepository repository = new ProductRepository(dbContext.Object, logger.Object);

            //Act
            var result = await repository.Update(new Product() { Id = 2, Name = "test", AgeRestriction = 10, Company = "testing", Description = "none", Price = 89.98m });

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task UpdateProduct_Exception_ReturnsFalse()
        {
            //Arrange            
            Mock<DBContext> dbContext = new Mock<DBContext>(LoadContextOptions());
            Mock<ILogger<ProductRepository>> logger = new Mock<ILogger<ProductRepository>>();
            dbContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).Throws(new ArgumentNullException());
            IProductRepository repository = new ProductRepository(dbContext.Object, logger.Object);

            //Act
            var result = await repository.Update(null);

            //Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region DeleteProduct

        [TestMethod]
        public async Task DeleteProduct_ReturnsTrue()
        {
            //Arrange            
            Mock<DBContext> dbContext = new Mock<DBContext>(LoadContextOptions());
            Mock<ILogger<ProductRepository>> logger = new Mock<ILogger<ProductRepository>>();
            dbContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(2);
            IProductRepository repository = new ProductRepository(dbContext.Object, logger.Object);

            //Act
            var result = await repository.Delete(new Product() { Id = 2, Name = "test", AgeRestriction = 10, Company = "testing", Description = "none", Price = 89.98m });

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task DeleteProduct_ReturnsFalse()
        {
            //Arrange            
            Mock<DBContext> dbContext = new Mock<DBContext>(LoadContextOptions());
            Mock<ILogger<ProductRepository>> logger = new Mock<ILogger<ProductRepository>>();
            dbContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);
            IProductRepository repository = new ProductRepository(dbContext.Object, logger.Object);

            //Act
            var result = await repository.Delete(new Product() { Id = 2, Name = "test", AgeRestriction = 10, Company = "testing", Description = "none", Price = 89.98m });

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task DeleteProduct_Exception_ReturnsFalse()
        {
            //Arrange            
            Mock<DBContext> dbContext = new Mock<DBContext>(LoadContextOptions());
            Mock<ILogger<ProductRepository>> logger = new Mock<ILogger<ProductRepository>>();
            dbContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).Throws(new ArgumentException());
            IProductRepository repository = new ProductRepository(dbContext.Object, logger.Object);

            //Act
            var result = await repository.Delete(null);

            //Assert
            Assert.IsFalse(result);
        }
        #endregion

        private DbContextOptions<DBContext> LoadContextOptions()
        {
            return new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "Products Test")
                .Options;
        }
    }
}
