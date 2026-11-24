using System;

namespace p48_lessons_asp.Data.DTO;

public class AssignmentDTO
{
    public bool Completed {get;set;}
    public string Title{get;set;} = string.Empty;
    public string? Description{get;set;}
    public string CreatedAt{get;set;}
    public string Deadline {get;set;}
}
