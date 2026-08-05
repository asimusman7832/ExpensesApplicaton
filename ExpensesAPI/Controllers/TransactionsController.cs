using ExpensesAPI.Data;
using ExpensesAPI.DTOs;
using ExpensesAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController(AppDbContext context) : ControllerBase
    {
        [HttpGet("All")]
        public ActionResult GetAll()
        {
            var allTrans = context.Transactions.ToList();

            return Ok(allTrans);
        }

        [HttpGet("Details/{id}")]
        public IActionResult Get(int id)
        {
            var transDB = context.Transactions.FirstOrDefault(x => x.Id == id);
            if (transDB == null)
                return NotFound();

            return Ok(transDB);
        }

        [HttpPost("Create")]
        public IActionResult CreateTransaction([FromBody] PostTransactionDTO payload)
        {
            var newTrans = new Transaction()
            {
                Amount = payload.Amount,
                Type = payload.Type,
                Category = payload.Category,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            context.Transactions.Add(newTrans);
            context.SaveChanges();
            return Ok();
        }

        [HttpPut("Update/{id}")]
        public IActionResult UpdateTransaction(int id, [FromBody] PutTransactionDTO payload)
        {
            var transDb = context.Transactions.FirstOrDefault(x => x.Id == id);

            if (transDb == null)
                return NotFound();

            transDb.Amount = payload.Amount;
            transDb.Type = payload.Type;
            transDb.Category = payload.Category;
            transDb.UpdatedAt = DateTime.UtcNow;

            context.Transactions.Update(transDb);
            context.SaveChanges();

            return Ok(transDb);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteTransaction(int id)
        {
            var transDb = context.Transactions.FirstOrDefault(x => x.Id == id);

            if (transDb == null)
                return NotFound();

            context.Transactions.Remove(transDb);
            context.SaveChanges();

            return Ok();
        }
    }
}
