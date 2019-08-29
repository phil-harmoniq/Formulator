using System;

namespace Formulator.Exceptions
{
    [Serializable]
    public class FormulatorException : Exception
    {
        public FormulatorException() { }
        public FormulatorException(string message) : base(message) { }
        public FormulatorException(string message, Exception inner) : base(message, inner) { }
        //protected FormulatorException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
