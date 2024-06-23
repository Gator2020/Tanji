using Tanji.Core.Net;
using Tanji.Core.Net.Interception;

namespace Tanji.Infrastructure.Factories.Implementations;

public sealed class ConnectionFactory : IConnectionFactory
{
    private readonly IMiddleman _middleman;
    private readonly IServiceProvider _services;

    public ConnectionFactory(IServiceProvider services, IMiddleman middleman)
    {
        _services = services;
        _middleman = middleman;
    }

    // TODO: Apply Module/Pre-Processing Services to all HConnection instances.
    public HConnection Create(HNode local, HNode remote, HConnectionContext context)
    {
        return new HConnection(local, remote, _middleman, context);
    }
}