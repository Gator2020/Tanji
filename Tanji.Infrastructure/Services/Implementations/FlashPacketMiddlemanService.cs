using System.Text;

using Microsoft.Extensions.Logging;

using Tanji.Core.Net;
using Tanji.Core.Net.Buffers;
using Tanji.Core.Cryptography.Ciphers;

namespace Tanji.Infrastructure.Services.Implementations;

public sealed class FlashPacketMiddlemanService : IPacketMiddlemanService
{
    private readonly ILogger<FlashPacketMiddlemanService> _logger;

    public Guid Id { get; }
    public bool IsInterceptingOutgoing { get; set; } = true;
    public bool IsInterceptingIncoming { get; set; } = true;

    public FlashPacketMiddlemanService(ILogger<FlashPacketMiddlemanService> logger)
    {
        _logger = logger;

        Id = Guid.NewGuid();
    }

    public ValueTask<bool> PacketInboundAsync(Memory<byte> buffer, HNode source, HNode destination)
    {
        var pktReader = new HPacketReader(source.ReceivePacketFormat, buffer.Span);

        _logger.LogTrace($"[ID: {Id}]\r\n[Inbound({pktReader.Id}, {pktReader.Length})] < {ToString(buffer.Span)}\r\n----------------");
        return ValueTask.FromResult(false);
    }
    public ValueTask<bool> PacketOutboundAsync(Memory<byte> buffer, HNode source, HNode destination)
    {
        var pktReader = new HPacketReader(destination.ReceivePacketFormat, buffer.Span);
        if (pktReader.Id == 4002)
        {
            string sharedKeyHex = pktReader.ReadUTF8();
            if (sharedKeyHex.Length % 2 != 0)
            {
                sharedKeyHex = ("0" + sharedKeyHex);
            }

            byte[] sharedKeyBytes = Convert.FromHexString(sharedKeyHex);
            destination.EncryptCipher = new RC4(sharedKeyBytes);

            return ValueTask.FromResult(true);
        }

        _logger.LogTrace($"[ID: {Id}]\r\n[Outbound({pktReader.Id}, {pktReader.Length})] > {ToString(buffer.Span)}\r\n----------------");
        return ValueTask.FromResult(false);
    }

    private static string ToString(ReadOnlySpan<byte> bufferSpan)
    {
        string result = Encoding.UTF8.GetString(bufferSpan);
        for (int i = 0; i <= 13; i++)
        {
            result = result.Replace(((char)i).ToString(), "[" + i + "]");
        }
        return result;
    }
}