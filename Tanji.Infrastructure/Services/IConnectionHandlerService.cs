using System.Collections.ObjectModel;

using Tanji.Core.Net;

namespace Tanji.Infrastructure.Services;

public interface IConnectionHandlerService
{
    ObservableCollection<HConnection> Connections { get; }

    Task<HConnection> LaunchAndInterceptConnectionAsync(string ticket, HConnectionContext context, CancellationToken cancellationToken = default);
}