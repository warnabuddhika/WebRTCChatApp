using Signalling.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Signalling.Application.Dtos;

    public class RoomDto
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public int CountMember { get; set; }
	    public ICollection<Connection> Connections { get; set; } = new List<Connection>();
}
