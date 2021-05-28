using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public MessageRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }
        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);
        }

        public void DeleteMessage(Message message)
        {
            _context.Messages.Remove(message);
        }

        public async Task<Message> GetMessage(int id)
        {
            return await _context.Messages
                    .Include(u=>u.Sender)
                    .Include(u=>u.Recipient)
                    .SingleOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<PageList<MessageDTO>> GetMessagesForUser(MessageParams messageParams)
        {
            var query = _context.Messages
                            .OrderByDescending(m => m.MessageSent) // order by most recent mesage sent according to time
                            .AsQueryable();

            query = messageParams.Container switch
            {
                "Inbox" => query.Where(u => u.Recipient.UserName == messageParams.Username && u.RecipientDelted == false), // list of messages revieved to this user
                "Outbox" => query.Where(u => u.Sender.UserName == messageParams.Username && u.SenderDeleted == false),    // list of messages sent from this user
                  _=> query.Where(u => u.Recipient.UserName == messageParams.Username && u.DateRead == null && u.RecipientDelted == false)
            };

            var messages = query.ProjectTo<MessageDTO>(_mapper.ConfigurationProvider);

            return await PageList<MessageDTO>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<IEnumerable<MessageDTO>> GetMessageThread(string currentUsername, string recipientUsername)
        {
             // we are gonna get message from both side of conversation,
             // mark any of the message that currenently have not been read,
             // set message into Memory then map it to MEssageDTO

             var messages = await _context.Messages
                                .Include(u=>u.Sender).ThenInclude(p=>p.Photos)
                                .Where(
                                  m=>m.Recipient.UserName == currentUsername && m.RecipientDelted == false
                                  && m.Sender.UserName == recipientUsername 
                                  ||
                                  m.Recipient.UserName == recipientUsername && m.SenderDeleted == false
                                  && m.Sender.UserName == currentUsername
                                 )
                                 .OrderBy(m=>m.MessageSent)
                                 .ToListAsync();
            
            var unreadMessages = messages.Where(m=>m.DateRead == null
                                    && m.Recipient.UserName == currentUsername).ToList();

            if(unreadMessages.Any())
            {
                foreach(var message in unreadMessages)
                {
                    message.DateRead = DateTime.Now;
                }

                await _context.SaveChangesAsync();
            }

            return _mapper.Map<IEnumerable<MessageDTO>>(messages);


        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}