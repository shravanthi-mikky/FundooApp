using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace FundooApplication.Controllers
{
    [Authorize]
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {

    }
}
