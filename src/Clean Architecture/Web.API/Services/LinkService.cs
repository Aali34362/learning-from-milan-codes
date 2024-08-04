using Application.Abstractions.Links;

namespace Web.API.Services;

internal sealed  class LinkService : ILinkService
{
    private readonly LinkGenerator _linkGenerator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LinkService(LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
    {
        _linkGenerator = linkGenerator;
        _httpContextAccessor = httpContextAccessor;
    }

    public Link Generate(string endpointName, object? routeValues, string rel, string method)
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            return new Link("", "", "");
        }

        return new Link(
            _linkGenerator.GetUriByName(
                _httpContextAccessor.HttpContext,
                endpointName,
                routeValues),
            rel,
            method);
    }
}
