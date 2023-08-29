using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Manager.Serialization
{
    public interface Serializable
    {
        string[] ToCSV();

        void FromCSV(string[] values);
    }
}
