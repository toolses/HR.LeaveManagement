﻿using AutoMapper;
using Hr.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using Hr.LeaveManagement.Application.Persistence.Contracts;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public CreateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leaveType = _mapper.Map<LeaveType>(request.LeaveTypeDto);
            leaveType = await _leaveTypeRepository.Add(leaveType);

            return leaveType.Id;
        }
    }
}
