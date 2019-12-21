using ExamBuilder.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ExamBuilder.Business
{
    public class WiredBusiness : IWiredBusiness
    {
        private const string Url = "https://www.wired.com/feed/rss";
        public List<Wired> GetContents()
        {
            XDocument doc = XDocument.Load(Url);
            var x = doc.Root.Element("channel").Elements("item");
            int i = 0;
            List<Wired> wireds = doc.Root
                 .Element("channel").Elements("item")
                 .Select(x => new Wired
                 {
                     Id = ++i,
                     Title = (string)x.Element("title"),
                     Description = (string)x.Element("description")
                 })
                 .Take(5)
                 .ToList();

            return wireds;
        }
    }

    public interface IWiredBusiness
    {
        List<Wired> GetContents();
    }
}
