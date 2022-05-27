using AutoMapper;
using Hr.LeaveManagement.Application.DTOs.LeaveType.Validators;
using Hr.LeaveManagement.Application.Exceptions;
using Hr.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using Hr.LeaveManagement.Application.Persistence.Contracts;
using Hr.LeaveManagement.Application.Responses;
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
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, BaseCommandResponse>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public CreateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.LeaveTypeDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation failed";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            }

            var leaveType = _mapper.Map<LeaveType>(request.LeaveTypeDto);
            leaveType = await _leaveTypeRepository.Add(leaveType);

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = leaveType.Id;

            return response;
        }
    }
}
