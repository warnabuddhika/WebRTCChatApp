﻿namespace UserManagement.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;

public interface ITokenService
{
	Task<string> CreateTokenAsync(AppUser appUser);
}
