using ExpensesAPI.Data;
using ExpensesAPI.Data.Services;
using ExpensesAPI.DTOs;
using ExpensesAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class TransactionsController(ITransactionsService transService) : ControllerBase
    {
        [HttpGet("All")]
        public ActionResult GetAll()
        {
            var allTrans = transService.GetAll();

            return Ok(allTrans);
        }

        [HttpGet("Details/{id}")]
        public IActionResult Get(int id)
        {
            var transDB = transService.GetById(id);

            if (transDB == null)
                return NotFound();

            return Ok(transDB);
        }

        [HttpPost("Create")]
        public IActionResult CreateTransaction([FromBody] PostTransactionDTO payload)
        {
            var newTrans = transService.Add(payload);

            return Ok(newTrans);
        }

        [HttpPut("Update/{id}")]
        public IActionResult UpdateTransaction(int id, [FromBody] PutTransactionDTO payload)
        {
            var updatedTrans = transService.Update(id, payload);

            if (updatedTrans == null)
                return NotFound();

            return Ok(updatedTrans);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteTransaction(int id)
        {
            transService.Delete(id);

            return Ok();
        }
    }
}
