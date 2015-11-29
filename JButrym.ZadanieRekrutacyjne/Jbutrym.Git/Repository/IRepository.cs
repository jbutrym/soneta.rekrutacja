using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JButrym.Git.Repository
{
    interface IRepository<T>
        where T: IElement
    {
        bool IsRepository { get; }

        void Init();

        int GetCommitsCount(string author, DateTime date);

        int GetAvarangeCommitsCount(string author);
    }
}
