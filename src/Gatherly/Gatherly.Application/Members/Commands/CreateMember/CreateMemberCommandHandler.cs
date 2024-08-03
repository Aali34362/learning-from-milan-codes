using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.ValueObjects;
using MediatR;

namespace Gatherly.Application.Members.Commands.CreateMember;

internal sealed class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMemberCommandHandler(
        IMemberRepository memberRepository,
        IUnitOfWork unitOfWork)
    {
        _memberRepository = memberRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        var firstNameResult = FirstName.Create(request.FirstName);

        if (firstNameResult.IsFailure)
        {
            // Log error
            return Unit.Value;
        }

        var member = new Member(
            Guid.NewGuid(),
            request.Email,
            firstNameResult.Value,
            request.LastName);

        _memberRepository.Add(member);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}