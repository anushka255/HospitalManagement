using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/*
NAME

    Error Controller - Directs user with information on what type of error that they ran into 
    
SYNOPSIS
    
    ##GET METHODS
    public ActionResult<string> GetSecret()
    public ActionResult<AppUser> GetNotFound()
    public ActionResult<string> GetServerError()
    public ActionResult<string> GetBadRequest()

DESCRIPTION
    
    This is a global error handler. A base class that provides centralized exception handling. 
    
*/

public class ErrorController : BaseApiController
{
    private readonly DataContext _context;

    //Constructor for error controller class 
    //Instantiates the data context class
    public ErrorController(DataContext context)
    {
        _context = context;
    }

    //End point for all authorization based error
    [Authorize]
    [HttpGet("auth")]
    public ActionResult<string> GetSecret()
    {
        return "secret text";
    }
    
    //End point for when server cannot find the requested resource
    [HttpGet("not-found")]
    public ActionResult<AppUser> GetNotFound()
    {
        //This is always going to be false 
        //So this function is always going to return not found 
        //Because there is no user with a -1 username 
        var thing = _context.Users.Find(-1);

        if (thing == null) return NotFound();

        return Ok(thing);
    }
    
    //Endpoint to define server errors 
    [HttpGet("server-error")]
    public ActionResult<string> GetServerError()
    {
        //This is always going to be false 
        //So this function is always going to return the error message
        //Because there is no user with a -1 username 
        var thing = _context.Users.Find(-1);
        var thingToReturn = thing.ToString();
        return thingToReturn;
    }

    //End point for 400 bad response 
    //Returns not a good request when called 
    [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequest()
    {
        return BadRequest("This was not a good request");
    }
}