using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IMessageRepository
    {
         void AddMessage(Message message);
         void DeleteMessage(Message message);
         Task<Message> GetMessage(int id);
         Task<PageList<MessageDTO>> GetMessagesForUser(MessageParams messageParams);
         Task<IEnumerable<MessageDTO>> GetMessageThread(string currentUsername, string recipientUsername);
         Task<bool> SaveAllAsync();
         
    }
}