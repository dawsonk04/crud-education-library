﻿using DRK.ProgDec.BL;
using Microsoft.AspNetCore.Mvc;

namespace DRK.ProgDec.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<BL.Models.Program> Get()
        {
            return ProgramManager.Load();
        }

        [HttpGet("{id}")]
        public BL.Models.Program Get(int id)
        {
            return ProgramManager.LoadById(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] BL.Models.Program program)
        {
            try
            {
                int results = ProgramManager.Insert(program);
                return Ok(results);
                // Returns a 200
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("id")]
        public IActionResult Put(int id, [FromBody] BL.Models.Program program)
        {
            try
            {
                int results = ProgramManager.Update(program);
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
                int results = ProgramManager.Delete(id);
                return Ok(results);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
