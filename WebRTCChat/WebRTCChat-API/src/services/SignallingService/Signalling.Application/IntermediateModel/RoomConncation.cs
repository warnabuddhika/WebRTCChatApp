namespace Signalling.Application.IntermediateModel;

using Signalling.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RoomConncation
{
	public int RoomId { get; set; }
	public Connection Connection { get; set; }
}
