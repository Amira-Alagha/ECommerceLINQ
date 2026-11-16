using ECommerceLINQ.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
namespace ECommerceLINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            var order = dbContext.Orders.Where(o => o.Status == Models.OrderStatus.Delivered).Select(o => new
            {
                o.Id,
                o.Customer.FullName,
                Total = o.Items.Sum(i => i.LineTotal)
            }).OrderByDescending(o => o.Total);

            //##############################################################
            var groupsOfOrders = dbContext.Orders.GroupBy(o => o.Status).Select(g => new { Status = g.Key, percentage = (double)g.Count() / dbContext.Orders.Count() });
            //##############################################################
            var poductsWithHighPrice = dbContext.Products.Where(p => p.Price > p.Category.Products.Average(pr => pr.Price));
            //##############################################################
            var productsNeverOrdered = dbContext.Products.Where(p => p.OrderItems == null || p.OrderItems.Count == 0);
            //##############################################################
            var customerswithdate = dbContext.Customers.Where(c => c.RegisteredAt < DateTime.Now.AddMonths(-1) && c.Orders.Count >= 1);
            //##############################################################
            var customersWithCancelledOrders = dbContext.Customers.Where(c => c.Orders.Count(o => o.Status == Models.OrderStatus.Cancelled) > 2)
                .Select(c => new { c.FullName, CountOfCancelled = c.Orders.Count(o => o.Status == Models.OrderStatus.Cancelled) });
            //##############################################################

            var customersWithHighValueOrders = dbContext.Customers.Select(c => new { c.FullName, OrderCount = c.Orders.Count(), TotalSpent = c.Orders.Sum(o => o.Items.Sum(i => i.LineTotal)), AverageOrderValue = c.Orders.Average(o => o.Items.Sum(i => i.LineTotal)) }).OrderByDescending(c => c.TotalSpent).Take(5);

            //##############################################################
            var OrdersIn2025 = dbContext.Orders.Where(o => o.OrderDate.Year == 2025).GroupBy(o => o.OrderDate.Month).Select(o => new { Year = 2025, Month = o.Key, Count = o.Count(), Revenue = o.Sum(i => i.Items.Sum(j => j.LineTotal)) }).OrderBy(o => o.Month);


            //##############################################################
            var RevenuePerCategory = dbContext.Categories.Select(c => new { c.Name, Revenue = c.Products.Sum(p => p.OrderItems.Sum(oi => oi.LineTotal)) }).OrderByDescending(c => c.Revenue);
            //##############################################################
            var CustomersWithNoOrders = dbContext.Customers.Where(c => c.Orders.Count == 0).GroupBy(c => c.City).Select(g => new { g.Key, CustomerCount = g.Count() }).OrderByDescending(g => g.CustomerCount);
            //##############################################################
            var ProductsWithReviews = dbContext.Products.Where(p => p.Reviews.Count >= 10 && p.Reviews.Average(r => r.Rating) >= 4.5).Select(p => new { p.Name, p.Category, ReviewCount = p.Reviews.Count, AverageRating = p.Reviews.Average(r => r.Rating) });
            //##############################################################
            var CategoryWith2ExclusiveProducts = dbContext.Categories.Select(c => new
            {
                c.Name,
                TopProducts = c.Products.OrderByDescending(p => p.Price).Take(2)
            });
            //##############################################################
            var ProductsWithLowStock = dbContext.Products.Select(p => new { p.Name, p.Stock, PendingQuantity = p.OrderItems.Where(oi => oi.Order.Status == Models.OrderStatus.Pending).Sum(oi => oi.Quantity) }).Where(p => p.Stock < p.PendingQuantity);

            //##############################################################

            var CustomersWithHighSpending = dbContext.Customers.Where(c => c.Orders.Sum(o => o.Items.Sum(i => i.LineTotal)) > c.Orders.Average(o => o.Items.Sum(i => i.LineTotal)));
            //##############################################################
            var CategoryBestSellingProduct = dbContext.Categories.Select(c => new
            {
                c.Name,
                BestSellingProduct = c.Products.OrderByDescending(p => p.OrderItems.Sum(oi => oi.Quantity)).FirstOrDefault()
            });

            //##############################################################
          
           var CutomersWithLAllProductsInLaptop = dbContext.Customers.Where(c => dbContext.Products.Where(p => p.Category.Name == "Laptops").All(p => c.Orders.SelectMany(o => o.Items).Any(i => i.Id == p.Id)));
            //##############################################################
            var ReviewsFromCustomersNeverBoughtIt = dbContext.Reviews.Where(r => !r.Customer.Orders.SelectMany(o => o.Items).Any(i => i.Id == r.Product.Id));

            //##############################################################
            var CustomersWithallOrderCount = dbContext.Customers.Select(c => new { c.FullName, CountOfOrders = c.Orders.Count });
            //##############################################################
            //var Top5ProductPairs = dbContext.Orders.SelectMany(o => o.Items)


















        }
    }
}