using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Lesson18.Data.Models;
using Lesson18.Data.Models.Users;

namespace Lesson18.Data.EF;

public class ShopDbContext : DbContext {
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }

    public ShopDbContext(DbContextOptions<ShopDbContext> options)
        : base(options)
    {
    }
    
    public Task<Customer[]> SelectCustomers()
    {
        return Customers.ToArrayAsync();
    }
    public async Task InsertCustomer(Customer customer) {
        Customers.Add(customer);
        await SaveChangesAsync();
    }

    public async Task UpdateCustomer(Customer customer) {
        var foundCustomer = await Customers.FindAsync(customer.Id);

        if (foundCustomer == null)
            throw new ArgumentException($"Customer with id={customer.Id} not found");

        foundCustomer.FirstName = customer.FirstName;
        foundCustomer.LastName = customer.LastName;
        foundCustomer.Age = customer.Age;
        foundCustomer.Country = customer.Country;

        await SaveChangesAsync();
    }

    public async Task DeleteCustomer(int id) {
        var foundCustomer = Customers.AsNoTracking().FirstOrDefault(row => row.Id == id);
        
        if (foundCustomer == null)
            throw new ArgumentException($"Customer with id={id} not found");
        
        Customers.Remove(foundCustomer);
        await SaveChangesAsync();
    }

    // public IList<Order?> SelectOrders() => throw new NotImplementedException();
    // public async Task InsertOrder(Order order) => throw new NotImplementedException();
    // public async Task UpdateOrder(Order order) => throw new NotImplementedException();
    // public async Task DeleteOrder(int id) => throw new NotImplementedException();

    public Task<Product[]> SelectProducts() => Products.ToArrayAsync();

    public async Task InsertProduct(Product product)
    {
        Products.Add(product);
        await SaveChangesAsync();
    }
    
    public async Task UpdateProduct(Product product) {
        var foundProduct = Products.AsNoTracking().FirstOrDefault(row => row.Id == product.Id);

        if (foundProduct == null)
            throw new ArgumentException($"Product with id={product.Id} not found");

        foundProduct.Name = product.Name;
        foundProduct.Description = product.Description;
        foundProduct.Price = product.Price;
        foundProduct.ProductType = product.ProductType;
        Products.Update(foundProduct);
        await SaveChangesAsync();
    }
    
    public async Task DeleteProduct(int id) {
        var foundProduct = Products.AsNoTracking().FirstOrDefault(row => row.Id == id);

        if (foundProduct == null)
            throw new ArgumentException($"Product with id={id} not found");
        
        Products.Remove(foundProduct);
        await SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Product>()
            .Property(product => product.ProductType)
            .HasConversion(
                productType => productType.ToString(),
                productType => (ProductType)Enum.Parse(typeof(ProductType), productType)
            );
        modelBuilder.Entity<User>()
            .HasData(new User
            {
                Id = new Guid("831627e6-e6ed-4754-b8f6-91be5cd221ec"),
                FirstName = "Админ",
                LastName = "Главный",
                Login = "admin",
                Password = "Admin12+",
                Role = UserRole.Admin()
            });
        modelBuilder.Entity<User>()
            .HasData(new User
            {
                Id = new Guid("cda15a3f-38ea-4049-8d99-091ae31cc783"),
                FirstName = "Пользователь",
                LastName = "Обычный",
                Login = "user",
                Password = "User12+",
                Role = UserRole.Common()
            });
    }
}
