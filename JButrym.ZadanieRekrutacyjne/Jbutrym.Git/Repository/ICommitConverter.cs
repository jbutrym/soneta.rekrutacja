using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JButrym.Git.Repository
{
    interface ICommitConverter<T>
        where T : IElement
    {
        T FromLog(string log);
    }
}
