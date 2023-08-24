using Microsoft.AspNetCore.Mvc;
using jsonProj; 
using jsonProj.Models;
using jsonProj.Repositories;
using jsonProj.DTOs;

namespace jsonProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdInfoController : ControllerBase
    {
        private readonly Operations _operations;
        private readonly ILogger<IdInfoController> _logger;

        public IdInfoController(Operations operations, ILogger<IdInfoController> logger) 
        {
            _operations = operations;
            _logger = logger; 
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                var info = _operations.FindInfo(id);
                if (info != null)
                {
                    return Ok(info);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the GET request.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        [HttpPost]
        public IActionResult Post([FromBody] IdInfo info)
        {
            try
            {
                _operations.InsertInfo(info);
                return CreatedAtAction(nameof(Get), new { id = info.ID }, info);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the POST request.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] UpdateInfoDTO updateInfo)
        {
            try
            {
                _operations.UpdateInfo(id, updateInfo.context, updateInfo.Info);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the PUT request.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var info = _operations.FindInfo(id);
                if (info == null)
                {
                    return NotFound();
                }
                _operations.DeleteInfo(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the DELETE request.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

    }
}
