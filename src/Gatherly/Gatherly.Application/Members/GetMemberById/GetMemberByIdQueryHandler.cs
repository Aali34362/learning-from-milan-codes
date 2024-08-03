using Dapper;
using Gatherly.Application.Abstractions;
using Gatherly.Application.Abstractions.Messaging;
using Gatherly.Domain.Entities;
using Gatherly.Domain.Errors;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;
using Microsoft.Data.SqlClient;

namespace Gatherly.Application.Members.GetMemberById;

internal sealed class GetMemberByIdQueryHandler
    : IQueryHandler<GetMemberByIdQuery, MemberResponse>
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public GetMemberByIdQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Result<MemberResponse>> Handle(
        GetMemberByIdQuery request,
        CancellationToken cancellationToken)
    {
        await using SqlConnection sqlConnection = _connectionFactory
            .CreateConnection();

        MemberResponse? memberResponse = await sqlConnection
            .QueryFirstOrDefaultAsync<MemberResponse>(
                @"SELECT Id, Email, FirstName, LastName
                  FROM Members
                  WHERE Id = @MemberId",
                new
                {
                    request.MemberId
                });

        if (memberResponse is null)
        {
            return Result.Failure<MemberResponse>(
                DomainErrors.Member.NotFound(request.MemberId));
        }

        return memberResponse;
    }
}
