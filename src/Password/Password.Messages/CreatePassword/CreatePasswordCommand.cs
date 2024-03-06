using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Password.Messages.CreatePassword
{
    public sealed class CreatePasswordCommand : AbstractRequestAcceptedCommand
    {
        public CreatePasswordCommand(string requestId) : base(requestId)
        {
        }
    }
}
