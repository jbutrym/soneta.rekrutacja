using System;
using System.Text.RegularExpressions;

namespace JButrym.Git.Repository
{
    public class CommitConverter : ICommitConverter<Commit>
    {
        Commit _commit;

        public Commit FromLog(string log)
        {
            Regex regex = new Regex("<author>(.*)</author><subject>(.*)</subject><date>(.*)</date>");
            if (regex.IsMatch(log))
            {
                Match regMatch = regex.Match(log);
                _commit = new Commit()
                {
                    Author = regMatch.Groups[1].ToString(),
                    Subject = regMatch.Groups[2].ToString(),
                    Date = DateTime.Parse(regMatch.Groups[3].ToString()),
                };
            }
            else
            {
                throw new Exceptions.InvalidLogOutputException();
            }
            return _commit;
        }
    }
}
