using System;
using System.Collections.Generic;
using System.Text;

namespace ReactionTest
{
    public interface ISaveFile
    {
        void saveFile(string fileName, string text);
    }
}
