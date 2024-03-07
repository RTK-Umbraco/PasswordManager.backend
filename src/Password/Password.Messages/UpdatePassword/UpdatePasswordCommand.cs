using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Password.Messages.UpdatePassword
{
    public sealed class UpdatePasswordCommand : AbstractRequestAcceptedCommand
    {
        public UpdatePasswordCommand(string requestId) : base(requestId)
        { 
        }
    }
}
