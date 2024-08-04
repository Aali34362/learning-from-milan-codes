using Application.Data;
using Application.Messaging;
using Modules.Training.Domain.Invitations;
using Shared.Results;

namespace Modules.Training.Application.Invitations.GetInvitationById;

/// <summary>
/// Represents the <see cref="GetInvitationByIdQuery"/> handler.
/// </summary>
internal sealed class GetInvitationByIdQueryHandler : IQueryHandler<GetInvitationByIdQuery, InvitationResponse>
{
    private readonly ISqlQueryExecutor _sqlQueryExecutor;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetInvitationByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="sqlQueryExecutor">The SQL connection factory.</param>
    public GetInvitationByIdQueryHandler(ISqlQueryExecutor sqlQueryExecutor) => _sqlQueryExecutor = sqlQueryExecutor;

    /// <inheritdoc />
    public async Task<Result<InvitationResponse>> Handle(GetInvitationByIdQuery request, CancellationToken cancellationToken) =>
        await Result.Success(request)
            .Bind(async query => Result.Create(await GetInvitationByIdAsync(query)))
            .MapFailure(() => InvitationErrors.NotFound(new InvitationId(request.InvitationId)));

    private async Task<InvitationResponse?> GetInvitationByIdAsync(GetInvitationByIdQuery query) =>
        await _sqlQueryExecutor.FirstOrDefaultAsync<InvitationResponse>(
            @"SELECT id, email, sender_first_name, sender_last_name, status
              FROM training.invitations
              WHERE id = @InvitationId",
            new { query.InvitationId });
}
