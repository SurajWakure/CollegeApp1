using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Collegeapp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    ///[EnableCors(PolicyName = "AllowOnlyMicrosoft")]
    [Authorize(AuthenticationSchemes = "LoginForMicrosoftUsers")]
    public class MicrosoftController : ControllerBase
    {

    }
}
