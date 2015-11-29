using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jbutrym.Repositories.Git
{
    public class GitBranch
    {
        List<Commit> _commits = new List<Commit>();

        public string Name { get; set; }

        public void AddCommit(Commit commit)
        {
            _commits.Add(commit);
        }
    }
}
