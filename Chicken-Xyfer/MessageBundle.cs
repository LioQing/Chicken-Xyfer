using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace Chicken_Xyfer
{
    class MessageBundle
    {
        public SocketUserMessage msg { get; set; }
        public string[] lowArgs { get; set; }
        public string[] args { get; set; }

        public void UnBundle(out SocketUserMessage aMsg, out string[] aLowArgs, out string[] aArgs)
        {
            aMsg = msg;
            aLowArgs = lowArgs;
            aArgs = args;
        }

        public MessageBundle(SocketUserMessage aMsg, string[] aLowArgs, string[] aArgs)
        {
            msg = aMsg;
            lowArgs = aLowArgs;
            args = aArgs;
        }
    }
}
