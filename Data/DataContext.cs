using System;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using p48_lessons_asp.Data.Entity;

namespace p48_lessons_asp.Data;

public class DataContext:DbContext
{
    public DataContext(DbContextOptions options):base(options){}

    public DbSet<Assignment> Assignments {get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var now = new DateTime(2026, 11, 24, 0, 0, 0, DateTimeKind.Local); //Set this date as the current date for this to work as intended.
        var time_left = now.AddDays(7);
        var expired = now.AddDays(-1);

        modelBuilder.Entity<Assignment>().HasData(
            new Assignment
            {
                Id = Guid.Parse("376bafb4-0956-41fb-a8dc-7ccc0ce77482"),
                Title = "Completed Task",
                Completed = true,
                Description = "A completed task",
                CreatedAt = now,
                DeadlineDate = time_left
            },
            new Assignment
            {
                Id = Guid.Parse("31bbe80d-b560-4e1b-82e7-313aa6b08a5f"),
                Title = "Expired Task",
                Completed = false,
                Description = "An expired task.",
                CreatedAt = now,
                DeadlineDate = expired
            },
            new Assignment
            {
                Id = Guid.Parse("00040993-0902-445c-95d8-6a950012dcfd"),
                Title = "Uncompleted Task",
                Completed = false,
                Description = "An uncompleted task.",
                CreatedAt = now,
                DeadlineDate = time_left
            }
        );
    }
}
