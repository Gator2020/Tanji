namespace Tanji.Core.Net;

public interface IMiddleman
{
    bool IsInterceptingOutgoing { get; set; }
    bool IsInterceptingIncoming { get; set; }

    ValueTask<bool> PacketInboundAsync(Memory<byte> buffer, HNode source, HNode destination);
    ValueTask<bool> PacketOutboundAsync(Memory<byte> buffer, HNode source, HNode destination);
}