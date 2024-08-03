using System.Reflection;
using FluentValidation;
using Gatherly.Domain.Shared;
using MediatR;

namespace Gatherly.Application.Behaviors;

public class ValidationPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators) =>
        _validators = validators;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        Error[] errors = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure is not null)
            .Select(failure => new Error(
                failure.PropertyName,
                failure.ErrorMessage))
            .Distinct()
            .ToArray();

        if (errors.Any())
        {
            return CreateValidationResult<TResponse>(errors);
        }

        return await next();
    }

    private static TResult CreateValidationResult<TResult>(Error[] errors)
        where TResult : Result
    {
        if (typeof(TResult) == typeof(Result))
        {
            return (Result.Failure(errors) as TResult)!;
        }

        object result = typeof(Result)
            .GetMethods()
            .First(m =>
                m is { IsGenericMethod: true, Name: nameof(Result.Failure) } &&
                m.GetParameters().First().ParameterType == typeof(Error[]))!
            .MakeGenericMethod(typeof(TResult).GenericTypeArguments[0])
            .Invoke(null, new object?[] { errors })!;

        return (TResult)result;
    }
}
