﻿using Tanji.Core.Net.Interception;

namespace Tanji.Core.API;

public interface IExtension : IDisposable
{
    bool IsStandalone { get; }
    IInstaller Installer { get; set; }

    void OnConnected();
    void HandleOutgoing(DataInterceptedEventArgs e);
    void HandleIncoming(DataInterceptedEventArgs e);
}