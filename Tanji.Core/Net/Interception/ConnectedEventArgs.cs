﻿using System.ComponentModel;

namespace Tanji.Core.Net.Interception;

public sealed class ConnectedEventArgs : CancelEventArgs
{
    public HConnectionContext Context { get; set; }

    public ConnectedEventArgs(HConnectionContext context)
    {
        Context = context;
    }
}