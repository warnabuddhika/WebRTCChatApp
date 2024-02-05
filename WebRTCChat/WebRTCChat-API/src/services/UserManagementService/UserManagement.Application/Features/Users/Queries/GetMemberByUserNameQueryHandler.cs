namespace UserManagement.Application.Features.Users.Queries;
using AutoMapper;

using MediatR;
using System;
using System.Threading.Tasks;
using UserManagement.Application.Dtos;
using UserManagement.Domain.Dtos;
using UserManagement.Domain.Interfaces;

public class GetMemberByUserNameQueryHandler : IRequestHandler<GetMemberByUserNameQuery, MemberDto>
{
	private readonly IUserRepository _userRepository;
	private readonly IMapper _mapper;

	public GetMemberByUserNameQueryHandler(IUserRepository userRepository, IMapper mapper)
	{
		_userRepository = userRepository;
		_mapper = mapper;
	}

	public async Task<MemberDto> Handle(GetMemberByUserNameQuery request, CancellationToken cancellationToken)
	{
		var member = await _userRepository.GetMemberAsync(request.UserName);
		return _mapper.Map<MemberDto>(member);
	}
}

