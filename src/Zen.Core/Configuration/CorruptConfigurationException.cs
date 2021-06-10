using System;

namespace Zen.Core.Configuration
{
    public class CorruptConfigurationException : Exception
    {
        public CorruptConfigurationException() : base("Configuration file is corrupted")
        {

        }
    }
}