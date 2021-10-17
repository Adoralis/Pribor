using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _292_verification
{
    class PPB : Device
    {
        public void SendMsg (byte data)
        {
            byte[] msg;
            msg = TransmitMessage(data);
            Port.Write(msg, 0, msg.Length);
        }

        public void SendMsg(byte data1, byte data2)
        {
            byte[] msg;
            msg = TransmitMessage(data1, data2);
            Port.Write(msg, 0, msg.Length);
        }
    }
}
