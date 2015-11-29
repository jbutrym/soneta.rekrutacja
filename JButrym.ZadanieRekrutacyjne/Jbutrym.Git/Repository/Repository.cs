using JButrym.Git.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JButrym.Git.Repository
{
    public class Repository : IRepository<Commit>, IDisposable
    {
        bool _isDisposed;
        Process _process;

        List<Commit> _commits;

        public Repository(string gitPath, string repoPath)
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

            _commits = new List<Commit>();
        }

        public bool IsRepository
        {
            get
            {
                return !string.IsNullOrEmpty(ExecuteCommand("log -1"));
            }
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                _process.Dispose();
            }
        }

        public void Init()
        {
            var commitConverter = new CommitConverter();
            string[] revListOutput = ExecuteCommand(String.Format("rev-list --branches --no-merges --pretty=format:\"<author>{0}</author><subject>{1}</subject><date>{2}</date>\" --date=iso",
                                        "%an", "%s", "%cd")).SplitToNewLine();
            for (var i = 0; i < revListOutput.Length; i++)
            {
                try
                {
                    _commits.Add(commitConverter.FromLog(revListOutput[i]));
                }
                catch (Exceptions.InvalidLogOutputException)
                {
                    continue;
                }
            }
            // TODO - sortowanie po dacie
        }

        private string ExecuteCommand(string command)
        {
            _process.StartInfo.Arguments = command;
            _process.Start();
            string output = _process.StandardOutput.ReadToEnd().Trim();
            _process.WaitForExit();
            return output;
        }

        public int GetCommitsCount(string author, DateTime date)
        {
            int counter = 0;
            foreach (Commit commit in _commits)
            {
                counter += Convert.ToInt32(commit.Author == author && commit.Date.Date == date.Date);
            }
            return counter;
        }


        public int GetAvarangeCommitsCount(string author)
        {
            int counter = 0;
            var dates = new List<string>();
            foreach (Commit commit in _commits)
            {
                if (commit.Author != author)
                    continue;

                string dateString = commit.Date.ToShortDateString();
                if (!dates.Contains(dateString))
                    dates.Add(dateString);
                counter++;
            }
            return counter / (dates.Count == 0 ? 1: dates.Count);
        }
    }
}
