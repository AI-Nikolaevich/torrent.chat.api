using System.ComponentModel.DataAnnotations;

namespace Torrent.Chat.Api.Storage.Models
{
    public class EntityChat
    {
        public required string UserName { get; set; }
        public required string Message { get; set; }
    }
}
