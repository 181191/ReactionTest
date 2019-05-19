using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReactionTest
{
    public interface IShare
    {
        Task Show(string filePath);
    }
}
