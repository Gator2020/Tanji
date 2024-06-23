using Tanji.Core.Net;

namespace Tanji.Infrastructure.Factories;

public interface IConnectionFactory
{
    HConnection Create(HNode local, HNode remote, HConnectionContext context);
}