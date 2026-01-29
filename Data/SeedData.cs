using Microsoft.EntityFrameworkCore;
using SimpleStoreSite.Models;

namespace SimpleStoreSite.Data;

public static class SeedData
{
    public static void EnsureSeeded(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (db.Categories.Any()) return;

        var cat1 = new Category { Name = "General" };
        var cat2 = new Category { Name = "Electronics" };

        db.Categories.AddRange(cat1, cat2);
        db.SaveChanges();

        db.Products.AddRange(
            new Product { Name = "Notebook", Price = 2.50m, CategoryId = cat1.Id },
            new Product { Name = "Headphones", Price = 45m, CategoryId = cat2.Id }
        );

        db.SaveChanges();
    }
}
