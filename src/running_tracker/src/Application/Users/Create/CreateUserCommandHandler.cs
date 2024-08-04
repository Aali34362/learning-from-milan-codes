using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Users;
using SharedKernel;

namespace Application.Users.Create;

internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        Result<Email> emailResult = Email.Create(command.Email);
        if (emailResult.IsFailure)
        {
            return Result.Failure<Guid>(emailResult.Error);
        }

        Email email = emailResult.Value;
        if (!await _userRepository.IsEmailUniqueAsync(email))
        {
            return Result.Failure<Guid>(UserErrors.EmailNotUnique);
        }

        var name = new Name(command.Name);
        var user = User.Create(email, name, command.HasPublicProfile);

        _userRepository.Insert(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
