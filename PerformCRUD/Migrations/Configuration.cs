namespace PerformCRUD.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using PerformCRUD.Models;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PerformCRUD.Data.PerformCRUDContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PerformCRUD.Data.PerformCRUDContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.StudentLists.AddOrUpdate(x => x.Id,
                new StudentList() { Id = 1, Name = "Tom Hanks", Age = 16, Grade = 12 },
                new StudentList() { Id = 2, Name = "Samuel L Jackson", Age = 12, Grade = 8 },
                new StudentList() { Id = 3, Name = "Robert Downey Jr", Age = 8, Grade = 4 }
                );
        }
    }
}
