using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messaging.Domain.Dtos;

    public class RoomDto
    {
        public Guid RoomId { get; set; }
        public string RoomName { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public int CountMember { get; set; }
    }
