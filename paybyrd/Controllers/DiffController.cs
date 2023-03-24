using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using paybyrd.Entities;
using paybyrd.Entities.Request;
using paybyrd.Interfaces.Services;
using paybyrd.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace paybyrd.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class DiffController : ControllerBase
    {
        private readonly ILogger<DiffController> _logger;
        private readonly IDiffService _diffService;

        public DiffController(ILogger<DiffController> logger, IDiffService diffService)
        {
            _diffService = diffService;
            _logger = logger;
        }

        [HttpPost("Left")]
        public IActionResult  Left(DiffRequest diff)
        {
            try
            {
                var retorno = _diffService.SaveLeft(diff);
                return Ok(retorno);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost("Right")]
        public IActionResult Right(DiffRequest diff)
        {
            try
            {
               _diffService.SaveRight(diff);
                return Ok(diff);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult Compare(int id = 1, bool IgnoreUpperCaseLowerCase = true)
        {
            try
            {
               var retorno = _diffService.GetDiff(id, IgnoreUpperCaseLowerCase);
                return Ok(retorno);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }
    }
}
