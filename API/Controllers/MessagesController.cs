using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/*
NAME

    Message Controller - End point for users to send and receive messages
    
SYNOPSIS

    ##POST METHODS
     public async Task<ActionResult<MessageDTO>> CreateMessage(CreateMessageDTO createMessageDto)
    
    
    ##GET METHODS
    public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMessagesForUser([FromQuery] MessageParams messageParams)
    public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMessageThread(string username)

DESCRIPTION
    
    This is and endpoint for messaging feature of the application. The get method retrieves the of messages posted by other users
    and the post method sends the message to the receipt. 
    
*/

[Authorize]
public class MessagesController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IMessageRepository _messageRepository;
    private readonly IUserRepository _userRepository;

    //Constructor for messages controller class
    //Instantiates an object for the user repository, message repository and the mapper class
    public MessagesController(IUserRepository userRepository, IMessageRepository messageRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _messageRepository = messageRepository;
        _mapper = mapper;
    }

    //HTTP post method to create a message 
    //Takes an object from the create message data transfer object class as a parameter 
    //Returns the message if success and a bad request if failed 
    [HttpPost]
    public async Task<ActionResult<MessageDTO>> CreateMessage(CreateMessageDTO createMessageDto)
    {
        //User name of the sender
        //Checks if the intended recipient is the user logged in 
        //Returns bad request if true
        var username = User.GetUsername();
        if (username == createMessageDto.RecipientUsername.ToLower())
            return BadRequest("You cannot send messages to yourself");

        //Username of the sender and the recipent 
        var sender = await _userRepository.GetUserByUsernameAsync(username);
        var recipient = await _userRepository.GetUserByUsernameAsync(createMessageDto.RecipientUsername);

        //Returns a 404 error if recipient not found 
        if (recipient == null) return NotFound();

        //Instantiates new message object
        //Assigns variable according to what the user sent out 
        var message = new Message
        {
            Sender = sender,
            Recipient = recipient,
            SenderUsername = sender.UserName,
            RecipientUsername = recipient.UserName,
            Content = createMessageDto.Content
        };

        //Adds message to the message entity 
        _messageRepository.AddMessage(message);

        //Returns the message if success
        //Returns failed to send message if failure 
        if (await _messageRepository.SaveAllAsync()) return Ok(_mapper.Map<MessageDTO>(message));

        return BadRequest("Failed to Send Message");
    }

    //Get method to retrieve a message sent out by other users
     //Queries the message parameters to and returns the value accordingly 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMessagesForUser([FromQuery] MessageParams messageParams)
    {
        //Username of the recipient
        messageParams.Username = User.GetUsername();
        //Returns messages for the recipient from the message repository 
        var messages = await _messageRepository.GetMessagesForUser(messageParams);

        //Returns the messages according to the queries sent out by the pagination header 
        Response.AddPaginationHeader(messages.CurrentPage, messages.PageSize,
            messages.TotalCount, messages.TotalPages);

        return messages;
    }

    //End point to get the thread of messages between two users
    [HttpGet("thread/{{username}}")]
    public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMessageThread(string username)
    {
        //The user requesting for the thread
        var currentUsername = User.GetUsername();

        //Retrieves the thread from the message repository and returns it 
        return Ok(await _messageRepository.GetMessageThread(currentUsername, username));
    }
}