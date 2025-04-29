using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController:ControllerBase
    {
        [HttpGet(template: "notfound")]// GET: /api/Buggy/notfound
        public IActionResult GetNotFoundRequest() {

            // Code
            return NotFound(); // 404
        }

        // Code

        [HttpGet(template: "servererror")] // GET: /api/Buggy/servererror
        public IActionResult GetSeverErrorRequest()

        {
            throw new Exception();
            return Ok(); // 404
        }
        [HttpGet(template: "badrequest")] // GET: /api/Buggy/badrequest

        public IActionResult GetBadRequest()
        {
            // Code
            return BadRequest();
        }
        [HttpGet(template: "badrequest/{id}")] // GET: /api/Buggy/badrequest/ahmed
public IActionResult GetBadRequest(int id) // Validation Error
        {
            // Code
            return BadRequest(); // 400
        }

        [HttpGet(template: "unauthorized")] // GET: /api/Buggy/unauthorized
        
public IActionResult GetUnauthorizedRequest()
        {
            // Code
            return Unauthorized(); // 401// 400
        }
    }
}
