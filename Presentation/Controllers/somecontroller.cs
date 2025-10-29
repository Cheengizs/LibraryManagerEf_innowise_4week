using Microsoft.AspNetCore.Mvc;

namespace LibrayManagerEf.Controllers;


[ApiController]
[Route("api/v1")]
public class somecontroller : ControllerBase
{
    
    [HttpGet("somecontroll")]
    public async Task<IActionResult> PIDORASIN(int piska)
    {
        
        return Ok();
    }
}