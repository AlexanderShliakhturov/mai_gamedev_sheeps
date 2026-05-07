using System;

namespace App.Scripts.Utils.Exceptions
{
    public class InitServiceException : Exception
    {
        public InitServiceException(Type serviceType) : base("Failed to init service with type" + serviceType.Name)
        {
        }
    }
}