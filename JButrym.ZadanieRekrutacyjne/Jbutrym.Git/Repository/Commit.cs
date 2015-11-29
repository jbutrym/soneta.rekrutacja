using System;


namespace JButrym.Git.Repository
{
    public class Commit : IElement
    {
        public string Author { get; set; }

        public DateTime Date { get; set; }

        public string Subject { get; set; }
    }
}
