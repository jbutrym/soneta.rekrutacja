using System;
using Soneta.Business.UI;
using JButrym.ZadanieRekrutacyjne;
using JButrym.Git.Repository;
using Soneta.Business.Licence;

[assembly: FolderView("JButrym.ZadanieRekrutacyjne",
    Priority = 10,
    Description = "Jarosław Butrym zadanie rekrutacyjne",
    BrickColor = FolderViewAttribute.YellowBrick,
    Icon = "JButrym.ZadanieRekrutacyjne.Utils.examples.ico;JButrym.ZadanieRekrutacyjne",
    Contexts = new object[] { LicencjeModułu.All },
    ObjectType = typeof(CommitCounter),
    ObjectPage = "CommitCounter.pageform.xml",
    ReadOnlySession = false,
    ConfigSession = false
)]

namespace JButrym.ZadanieRekrutacyjne
{
    public class CommitCounter
    {
        private string _repoPathField = @"C:\Work\testRepo";

        public string RepoPathField 
        {
            get
            {
                return _repoPathField;
            }
            set
            {
                _repoPathField = value;
            }
        }

        private string _gitPathField = @"C:\Program Files (x86)\Git\cmd\";

        public string GitPathField
        {
            get
            {
                return _gitPathField;
            }
            set
            {
                _gitPathField = value;
            }
        }

        public string AuthorField { get; set; }
        public string DateField { get; set; }

        public MessageBoxInformation ShowCountValue()
        {
            int count = 0;
            using (Repository repo = new Repository(GitPathField, RepoPathField))
            {
                count = repo.GetCommitsCount(AuthorField, DateTime.Parse(DateField));
            }

            return new MessageBoxInformation()
            {
                Text = String.Format("Ilość commitów, autora {0} z dnia {1}: {2}", AuthorField, DateField, count)
            };
        }

        public MessageBoxInformation ShowAverangeValue()
        {
            int count = 0;
            using (Repository repo = new Repository(GitPathField, RepoPathField))
            {
                count = repo.GetAvarangeCommitsCount(AuthorField);
            }

            return new MessageBoxInformation()
            {
                Text = String.Format("Średnia ilość commitów autora {0}: {1}", AuthorField, count)
            };
        }
    }
}
