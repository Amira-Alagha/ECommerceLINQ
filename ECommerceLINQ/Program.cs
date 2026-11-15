using ECommerceLINQ.DataAccess;
namespace ECommerceLINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            var customers = dbContext.Customers.AsQueryable();
            /*foreach (var customer in customers)
            {
                Console.WriteLine($"Customer ID: {customer.Id}, Name: {customer.FullName}, Email: {customer.Email}");
            }*/
            var c = dbContext.Customers.Where(x => x.Id == 1);
           //  Console.WriteLine(c);

        }
    }
}
