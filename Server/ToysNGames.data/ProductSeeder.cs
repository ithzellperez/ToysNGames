using Microsoft.EntityFrameworkCore.Internal;
using ToysNGames.Data.Entities;

namespace ToysNGames.Data
{
    public class ProductSeeder
    {
        private readonly DBContext _dbContext;

        public ProductSeeder(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (!_dbContext.Products.Any())
            {
                _dbContext.Products.AddRange(
                    new Product
                    {
                        Id = 1,
                        Name = "Barbie Developer",
                        Description = "Test",
                        AgeRestriction = 12,
                        Company = "Mattel",
                        Price = 25.99M
                    },
                    new Product
                    {
                        Id = 2,
                        Name = "Mr. Potato Head",
                        Description = "Test Mr Potato",
                        AgeRestriction = 8,
                        Company = "Mattel",
                        Price = 30.99M
                    },
                    new Product
                    {
                        Id = 3,
                        Name = "Teddy Bear",
                        Description = "Big fluffy teddy bear",
                        AgeRestriction = 12,
                        Company = "Hasbro",
                        Price = 15.89M
                    }
                    );

                _dbContext.SaveChanges();
            }
        }
    }
}
