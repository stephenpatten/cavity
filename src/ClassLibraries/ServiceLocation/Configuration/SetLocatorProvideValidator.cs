﻿namespace Cavity.Configuration
{
    using System;
    using System.Configuration;

    public sealed class SetLocatorProvideValidator : ConfigurationValidatorBase
    {
        public override bool CanValidate(Type type)
        {
            return true;
        }

        public override void Validate(object value)
        {
        }
    }
}