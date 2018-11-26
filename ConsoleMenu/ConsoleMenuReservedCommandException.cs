namespace ConsoleMenu
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class ConsoleMenuReservedCommandException : Exception
    {
        public ConsoleMenuReservedCommandException()
        {
        }

        public ConsoleMenuReservedCommandException(string message)
            : base(message)
        {
        }

        public ConsoleMenuReservedCommandException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ConsoleMenuReservedCommandException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}