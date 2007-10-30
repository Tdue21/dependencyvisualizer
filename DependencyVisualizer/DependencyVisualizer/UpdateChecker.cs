using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Xml;
using System.Text.RegularExpressions;

namespace DependencyVisualizer
{
    public class UpdateChecker
    {
        const string RSS_URL = "http://www.codeplex.com/dependencyvisualizer/Project/ProjectRss.aspx?ProjectRSSFeed=codeplex%3a%2f%2frelease%2fdependencyvisualizer";
        public UpdateChecker()
        {

        }

        public void Check()
        {
            HttpWebRequest request = WebRequest.Create(RSS_URL) as HttpWebRequest;
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {

                XmlDocument doc = new XmlDocument();
                doc.Load(response.GetResponseStream());
                Version newestVersion;
                foreach (XmlElement itemElement in doc.SelectNodes("/rss/channel/item/"))
                {
                    // assumption, the title contains a 3 digit version number
                    Regex re = new Regex(@":\s*(?<Major>\d+)\.(?<Minor>\d+)\.(?<Build>\d+)");
                    Match m = re.Match(itemElement.SelectSingleNode("title/text()").Value);
                    if (m.Success)
                    {
                        {

                        }
                    }
                }
            }
        }
    }
}