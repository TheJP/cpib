using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixFoxi2CSharp
{
    public interface Creator
    {
        void Create(StreamWriter writer, IDictionary<string, IDictionary<string, string[]>> parseTable);
    }
}
