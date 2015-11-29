using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jbutrym.Repositories.Git
{
    public class Commit
    {
        public string Author { get; set; }

        public string Subject { get; set; }

        public DateTime Date { get; set; }
    }
}
