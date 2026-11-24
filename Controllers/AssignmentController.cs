using System.Text;
using Microsoft.AspNetCore.Mvc;
using p48_lessons_asp.Data;
using p48_lessons_asp.Data.DTO;
using p48_lessons_asp.Data.Entity;
using p48_lessons_asp.Models;

namespace p48_lessons_asp.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly string AuthenticationToken = Convert.ToBase64String(Encoding.UTF8.GetBytes("COOLIO")); 

        public AssignmentController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult TasksReturn([FromQuery] bool? completed, [FromQuery] bool? expired, [FromQuery] bool? current)
        {
            if (completed is not null && completed.Value)
            {
                var data = _dataContext.Assignments.Where(task => task.Completed).Select(task => new AssignmentDTO
                {
                    Title = task.Title,
                    Description = task.Description,
                    Completed = task.Completed,
                    CreatedAt = $"{task.CreatedAt.Month}/{task.CreatedAt.Day}/{task.CreatedAt.Year}",
                    Deadline = $"{task.DeadlineDate.Month}/{task.DeadlineDate.Day}/{task.DeadlineDate.Year}"
                }).ToList();
                return Ok(new
                {
                    status = 200,
                    message = "Ok",
                    data
                });
            }
            if (expired is not null && expired.Value)
            {
                var data = _dataContext.Assignments.Where(task => task.DeadlineDate < DateTime.Now).Select(task => new AssignmentDTO
                {
                    Title = task.Title,
                    Description = task.Description,
                    Completed = task.Completed,
                    CreatedAt = $"{task.CreatedAt.Month}/{task.CreatedAt.Day}/{task.CreatedAt.Year}",
                    Deadline = $"{task.DeadlineDate.Month}/{task.DeadlineDate.Day}/{task.DeadlineDate.Year}"
                }).ToList();
                return Ok(new
                {
                   status = 200,
                   message = "Ok",
                   data 
                });
            }
            if (current is not null && current.Value)
            {
                var data = _dataContext.Assignments.Where(task => !task.Completed).Select(task => new AssignmentDTO
                {
                    Title = task.Title,
                    Description = task.Description,
                    Completed = task.Completed,
                    CreatedAt = $"{task.CreatedAt.Month}/{task.CreatedAt.Day}/{task.CreatedAt.Year}",
                    Deadline = $"{task.DeadlineDate.Month}/{task.DeadlineDate.Day}/{task.DeadlineDate.Year}"
                }).ToList();
                return Ok(new
                {
                    status = 200,
                    message = "Ok",
                    data
                });
            }
            return Ok(new
            {
                status = 200,
                message = "Ok",
                data = _dataContext.Assignments.Select(task => new AssignmentDTO
                {
                    Title = task.Title,
                    Description = task.Description,
                    Completed = task.Completed,
                    CreatedAt = $"{task.CreatedAt.Month}/{task.CreatedAt.Day}/{task.CreatedAt.Year}",
                    Deadline = $"{task.DeadlineDate.Month}/{task.DeadlineDate.Day}/{task.DeadlineDate.Year}"
                }).ToList()
            });
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult TaskIdReturn(int id)
        {
            id--;
            var count = _dataContext.Assignments.Count();
            if (id >= 0 && id < count)
            {
                var assignments = _dataContext.Assignments.OrderBy(task => task.CreatedAt).Select(task => new AssignmentDTO
                {
                    Title = task.Title,
                    Description = task.Description,
                    Completed = task.Completed,
                    CreatedAt = $"{task.CreatedAt.Month}/{task.CreatedAt.Day}/{task.CreatedAt.Year}",
                    Deadline = $"{task.DeadlineDate.Month}/{task.DeadlineDate.Day}/{task.DeadlineDate.Year}"
                }).ToList();

                return Ok(new
                {
                    status = 200,
                    message = "Ok",
                    data = assignments[id]
                });
            }
            return new ObjectResult(new
            {
               status = 400,
               message = "Error",
               error = $"Id must be between 1 and {count+1}" 
            }){StatusCode=400};   
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreateTask([FromBody] CreateAssignmentModel? model)
        {
            var headerToken = HttpContext.Request.Headers["Token"][0];
            if (headerToken != AuthenticationToken)
            {
                return new ObjectResult(new
                {
                    status = 403,
                    message = "Error",
                    error = "Incorrect token"
                }){StatusCode=403};
            }

            if (model is not null)
            {
                var now = DateTime.Now;
                var newAssignment = new Assignment
                {
                    Title = model.Title,
                    Completed = model.Completed,
                    Description = model.Description,
                    CreatedAt = now,
                    DeadlineDate = new DateTime(now.Year, now.Month, now.Day+model.DeadlineDays, 23, 59, 59)
                };
                _dataContext.Assignments.Add(newAssignment);
                _dataContext.SaveChanges();
                return Ok(new
                {
                   status = 200,
                   message = "Task added" 
                });
            }
            return new ObjectResult(new
            {
                status = 400,
                message = "Bad request",
                error = "No model found"
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult TaskIdDelete(int id)
        {
            id--;
            var headerToken = HttpContext.Request.Headers["Token"][0];
            if (headerToken != AuthenticationToken)
            {
                return new ObjectResult(new
                {
                    status = 403,
                    message = "Error",
                    error = "Incorrect token"
                }){StatusCode=403};
            }

            var count = _dataContext.Assignments.Count();
            if (id >= 0 && id < count)
            {
                var assignments = _dataContext.Assignments.OrderBy(task => task.CreatedAt).ToList();
                var assignment = assignments[id];
                _dataContext.Assignments.Remove(assignment);
                _dataContext.SaveChanges();

                return Ok(new
                {
                    status = 200,
                    message = "Task Deleted"
                });
            }
            return new ObjectResult(new
            {
               status = 400,
               message = "Error",
               error = $"Id must be between 1 and {count+1}" 
            }){StatusCode=400};   
        }
    }
}
