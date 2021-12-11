using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reports.DAL.Entities;
using Reports.Server.Services;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<Employee> Create([FromQuery] string name)
        {
            return await _service.Create(name);
        }

        [HttpGet("all employees")]
        public async Task<ActionResult<DbSet<Employee>>> GetEmployees()
        {
            return _service.Employees();
        }
        [HttpGet("delete")]
        public async Task<ActionResult<Employee>> Delete(int id)
        {
            return _service.Delete(id);
        }

        [HttpGet("{list tasks}")]
        public async Task<ActionResult<List<TaskModel>>> ListForEmployee(Employee employee)
        {
            return _service.ListForEmployee(employee);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployees(int id)
        {
            return _service.FindById(id);
        }
        [HttpPost("task/")]
        public async Task<ActionResult<TaskModel>> Create()
        {
             return await _service.CreateTask();
        }
        [HttpGet("task/all tasks")]
        public async Task<ActionResult<DbSet<TaskModel>>> GetList()
        {
            return _service.GetList();
        }
        
        [HttpGet("task/{id}")]
        public async Task<ActionResult<TaskModel>> FindTaskById(int id)
        {
            return _service.GetTaskById(id);
        }

        
        [HttpGet("task/{date born}")]
        public async Task<ActionResult<List<TaskModel>>> FindTaskByDateBorn(DateTime born)
        {
            return _service.FindTaskByDateBorn(born);
        }

        [HttpGet("task/{date update}")]
        public async Task<ActionResult<List<TaskModel>>> FindTaskByLastUpdate(DateTime update)
        {
            return _service.FindTaskByLastUpdate(update);
        }

        [HttpGet("task/{employee}")]
        public async Task<ActionResult<List<TaskModel>>> FindTaskByEmployee(Employee employee)
        {
            return _service.FindTaskByEmployee(employee);
        }

        [HttpGet("task/{commenter}")]
        public async Task<ActionResult<TaskModel>> FindTaskByEmployeeComment(Employee employee)
        {
            return _service.FindTaskByEmployeeComment(employee);
        }

        [HttpGet("task/{change positions}")]
        public async Task<ActionResult<string>> ChangePositions(TaskModel task)
        {
            return _service.ChangePositions(task);
        }

        [HttpGet("task/{week tasks}")]
        public async Task<ActionResult<DbSet<TaskModel>>> WeekTasks()
        {
            return _service.WeekTasks();
        }
        
        
        [HttpPost("reports")]
        public async Task<Report> CrearedReport()
        {
            return _service.WeekReport();
        }
        [HttpGet("reports")]
        public async Task<ActionResult<Report>> GetReport(Report report)
        {
            return _service.GetReport(report);
        }
        [HttpGet("reports/{week report}")]
        public async Task<Report> WeekReport()
        {
            return _service.WeekReport();
        }
    }
}