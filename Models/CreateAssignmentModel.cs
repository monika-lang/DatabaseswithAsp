using System;

namespace p48_lessons_asp.Models;

public class CreateAssignmentModel
{
    public bool Completed {get;set;}
    public string Title{get;set;} = string.Empty;
    public string? Description{get;set;}
    public int DeadlineDays {get;set;}
}
