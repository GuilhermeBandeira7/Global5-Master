namespace Global5.Api.Common;

public interface IEndpoint
{
    static abstract void Map(IEndpointRouteBuilder app);
}