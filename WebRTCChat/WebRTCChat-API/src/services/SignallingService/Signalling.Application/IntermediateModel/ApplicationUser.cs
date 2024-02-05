namespace Signalling.Application.IntermediateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ApplicationUser
{
	public string Id { get; set; }
	public string Name { get; set; }
	public string Email { get; set; }
	public string Username { get; set; }
	public bool Active { get; set; }
	public bool Locked { get; set; }
}
	
