using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands.Core.Exceptions;


[Serializable]
public class NoSuchCommandException : Exception
{
    public NoSuchCommandException() { }
    public NoSuchCommandException(string message) : base(message) { }
    public NoSuchCommandException(string message, Exception inner) : base(message, inner) { }
    protected NoSuchCommandException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
