using ExpensesAPI.Data;
using ExpensesAPI.Data.Services;
using ExpensesAPI.DTOs;
using ExpensesAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExpensesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    [Authorize]
    public class TransactionsController(ITransactionsService transService) : ControllerBase
    {
        [HttpGet("All")]
        public ActionResult GetAll()
        {
            var nameIdentifierClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(nameIdentifierClaim))
                return BadRequest("User ID is missing");

            if (!int.TryParse(nameIdentifierClaim, out int userId))
                return BadRequest("Invalid User ID");

            var allTrans = transService.GetAll(userId);

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
            var nameIdentifierClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(string.IsNullOrEmpty(nameIdentifierClaim))
                return BadRequest("User ID is missing");

            if(!int.TryParse(nameIdentifierClaim, out int userId))
                return BadRequest("Invalid User ID");

            var newTrans = transService.Add(payload, userId);

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
