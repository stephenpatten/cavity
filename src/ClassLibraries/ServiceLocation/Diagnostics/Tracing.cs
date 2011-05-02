﻿namespace Cavity.Diagnostics
{
    using System.Diagnostics;

    internal static class Tracing
    {
        internal static bool Enabled
        {
            get
            {
                return new BooleanSwitch("Cavity.ServiceLocation", string.Empty).Enabled;
            }
        }
    }
}