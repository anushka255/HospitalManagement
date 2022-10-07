using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class MessageRepository : IMessageRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public MessageRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public void AddMessage(Message message)
    {
        _context.Messages.Add(message);
    }

    public async Task<Message> GetMessage(int id)
    {
        return await _context.Messages.FindAsync(id);
    }

    public async Task<PagedList<MessageDTO>> GetMessagesForUser(MessageParams messageParams)
    {
        var query = _context.Messages
            .OrderByDescending(m => m.MessageSent)
            .ProjectTo<MessageDTO>(_mapper.ConfigurationProvider)
            .AsQueryable();

        query = messageParams.Container switch
        {
            "Inbox" => query.Where(u => u.RecipientUsername == messageParams.Username),
            "Outbox" => query.Where(u => u.SenderUsername == messageParams.Username),
            _ => query.Where(u => u.RecipientUsername ==
                messageParams.Username && u.DateRead == null)
        };

        return await PagedList<MessageDTO>.CreateAsync(query, messageParams.PageNumber, messageParams.PageSize);
    }

    public async Task<IEnumerable<MessageDTO>> GetMessageThread(string currentUsername, string recipientUsername)
    {
        var messages = await _context.Messages
            .Include(u => u.Sender).ThenInclude(p => p.Photo)
            .Include(u => u.Recipient).ThenInclude(p => p.Photo)
            .Where(m => (m.Recipient.UserName == currentUsername
                         && m.Sender.UserName == recipientUsername)
                        || (m.Recipient.UserName == recipientUsername
                            && m.Sender.UserName == currentUsername))
            .OrderBy(m => m.MessageSent)
            .ToListAsync();

        var unreadMessages = messages.Where(m => m.DateRead == null
                                                 && m.Recipient.UserName == currentUsername).ToList();

        if (unreadMessages.Any())
        {
            foreach (var message in unreadMessages) message.DateRead = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        return _mapper.Map<IEnumerable<MessageDTO>>(messages);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}