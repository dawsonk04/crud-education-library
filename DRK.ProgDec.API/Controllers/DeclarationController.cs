using DRK.ProgDec.BL;
using Microsoft.AspNetCore.Mvc;

namespace DRK.ProgDec.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeclarationController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<BL.Models.Declaration> Get()
        {
            return DeclarationManager.Load();
        }

        [HttpGet("{id}")]
        public BL.Models.Declaration Get(int id)
        {
            return DeclarationManager.LoadById(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] BL.Models.Declaration declaration)
        {
            try
            {
                int results = DeclarationManager.Insert(declaration);
                return Ok(results);
                // Returns a 200
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("id")]
        public IActionResult Put(int id, [FromBody] BL.Models.Declaration declaration)
        {
            try
            {
                int results = DeclarationManager.Update(declaration);
                return Ok(results);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            try
            {
                int results = DeclarationManager.Delete(id);
                return Ok(results);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
