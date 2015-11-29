using Jbutrym.Git.Extensions;
using Jbutrym.Repositories;
using Jbutrym.Repositories.Git;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Jbutrym.Git.GitRepository
{
    public class GitRepository : IDisposable
    {
        bool _isDisposed;
        Process _process;

        public GitRepository(string gitPath, string repoPath)
        {
            string gitFullPath = gitPath + "git.exe";
            if (!File.Exists(gitFullPath))
            {
                throw new ArgumentException("Invalid gitPath");
            }

            var startInfo = new ProcessStartInfo(gitFullPath)
            {
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                WorkingDirectory = repoPath,
            };

            _process = new Process()
            {
                StartInfo = startInfo
            };

            if (!IsRepository)
            {
                throw new ArgumentException("Invalid repoPath");
            }
        }

        public bool IsRepository
        {
            get
            {
                return !string.IsNullOrEmpty(ExecuteCommand("log -1"));
            }
        }

        public List<GitBranch> GetBranches()
        {
            var result = new List<GitBranch>();
            string[] branches = ExecuteCommand("branch").SplitToNewLine();
            for (var i = 0; i < branches.Length; i++)
            {

                var branch = new GitBranch()
                {
                    Name = branches[i]
                };
                ExecuteCommand(String.Format("checkout {0}", branches[i]));
                var commitsOutput = ExecuteCommand(String.Format("log --date=iso --pretty=format:\"<author>{0}</author><subject>{1}</subject><date>{2}</date>\"",
                    "%an", "%s", "%cd"));
                var commits = commitsOutput.SplitToNewLine();
                for (var j = 0; j < commits.Length; j++)
                {
                    Regex regex = new Regex("<author>(.*)</author><subject>(.*)</subject><date>(.*)</date>");
                    if (regex.IsMatch(commits[j]))
                    {
                        var match = regex.Match(commits[j]);
                        branch.AddCommit(new Commit
                        {
                            Author = match.Groups[1].ToString(),
                            Subject = match.Groups[2].ToString(),
                            Date = DateTime.Parse(match.Groups[3].ToString())
                        });
                    }
                    else
                    {
                        System.Diagnostics.Debugger.Break();
                    }
                }

                result.Add(branch);
            }
            return result;
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                _process.Dispose();
            }
        }

        private string ExecuteCommand(string command)
        {
            _process.StartInfo.Arguments = command;
            _process.Start();
            string output = _process.StandardOutput.ReadToEnd().Trim();
            _process.WaitForExit();
            return output;
        }
    }
}
