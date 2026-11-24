using System;

namespace p48_lessons_asp.Data.Entity;

public class Assignment
{
    public Guid Id {get;set;} = Guid.NewGuid();
    public bool Completed {get;set;}
    public string Title{get;set;} = string.Empty;
    public string? Description{get;set;}
    public DateTime CreatedAt{get;set;} = DateTime.Now;
    public DateTime DeadlineDate {get;set;}
}
