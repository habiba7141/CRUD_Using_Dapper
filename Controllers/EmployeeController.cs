using CRUDUsingDapper.Models;
using CRUDUsingDapper.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDUsingDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepo repo;
        public EmployeeController(IEmployeeRepo repo)
        {
            this.repo = repo;
        }
        [HttpGet("GetEmployee")]
        public async Task<IActionResult> Get()
        {
            var result = await repo.GetAll();
            if (result.Count != 0)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("GetEmployeeByCode/{code}")]
        public async Task<IActionResult> GetByCode(int code)
        {
            var result = await repo.GetByCode(code);
            if (result != null)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> Create([FromBody] Employee employee)
        {
            var result = await repo.Create(employee);
            return Ok(result);

        }
        [HttpDelete("RemoveEmployee")]
        public async Task<IActionResult> Delete(int code)
        {
            var result = await repo.Delete(code);
            return Ok(result);

        }
        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> Update([FromBody]Employee employee,int code)
        {
            var result = await repo.Update(employee,code);
            return Ok(result);

        }




    }
}
