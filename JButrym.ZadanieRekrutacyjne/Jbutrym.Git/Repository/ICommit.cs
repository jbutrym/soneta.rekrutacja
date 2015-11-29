using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JButrym.Git.Repository
{
    interface IElement
    {
        string Author { get; set; }

        DateTime Date { get; set; }

        string Subject { get; set; }
    }
}
