using Application.Data;
using Application.Messaging;
using Shared.Results;

namespace Modules.Training.Application.Invitations.GetInvitations;

/// <summary>
/// Represents the <see cref="GetInvitationsQuery"/> handler.
/// </summary>
internal sealed class GetInvitationsQueryHandler : IQueryHandler<GetInvitationsQuery, List<InvitationResponse>>
{
    private readonly ISqlQueryExecutor _sqlQueryExecutor;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetInvitationsQueryHandler"/> class.
    /// </summary>
    /// <param name="sqlQueryExecutor">The SQL query executor.</param>
    public GetInvitationsQueryHandler(ISqlQueryExecutor sqlQueryExecutor) => _sqlQueryExecutor = sqlQueryExecutor;

    /// <inheritdoc />
    public async Task<Result<List<InvitationResponse>>> Handle(GetInvitationsQuery request, CancellationToken cancellationToken) =>
        await Result.Create(request)
            .Bind(async query => Result.Create(await GetInvitationByIdAsync(query)))
            .Map(invitations => invitations.ToList());

    private async Task<IEnumerable<InvitationResponse>> GetInvitationByIdAsync(GetInvitationsQuery query) =>
        await _sqlQueryExecutor.QueryAsync<InvitationResponse>(
            @"SELECT id, email, status, created_on_utc, modified_on_utc
              FROM training.invitations
              WHERE trainer_id = @TrainerId
              ORDER BY created_on_utc DESC",
            new { query.TrainerId });
}
