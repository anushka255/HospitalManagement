using API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
/*
NAME

    Base API Controller

SYNOPSIS
    
    ##THIS CONTROLLER HAS NO METHODS

DESCRIPTION
    
  It is a simple api controller that acts as base to guide end point and routing for the rest of the controllers
    
*/


//Logs to see when the user was previously active
[ServiceFilter(typeof(LogUserActivity))]
[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
}