using Tanji.Core.Canvas;
using Tanji.Core.Net.Interception;

namespace Tanji.Core.API;

public interface IInstaller
{
    IGame Game { get; }
    HConnection Connection { get; }
}