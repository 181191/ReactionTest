using System;
using System.Collections.Generic;
using System.Text;

namespace ReactionTest
{
    public interface ISendMail
    {
        void sendMail(byte[]bytes, string mimeType, string content);
    }
}
