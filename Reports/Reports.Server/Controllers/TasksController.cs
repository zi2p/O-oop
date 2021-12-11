using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reports.Server.Services;

namespace Reports.Server.Controllers
{
    
    [ApiController]
    [Route("/tasks")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public TasksController(IEmployeeService service)
        {
            _service = service;
        }
        
    }

    public class CreateTaskRequestModel
    {
        public string Name { get; set; }
        public int Priority { get; set; }
    }
}