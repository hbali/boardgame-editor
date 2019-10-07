using System;
using System.Runtime.Serialization;

namespace UI.Exceptions
{
    [Serializable]
    public class UIComponentNotRegisteredException : Exception
    {
        public UIComponentNotRegisteredException()
        {
        }

        public UIComponentNotRegisteredException(string message) : base(message)
        {
        }

        public UIComponentNotRegisteredException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UIComponentNotRegisteredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}