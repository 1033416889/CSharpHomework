using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Collections;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Concurrent;

namespace CSharpHomeworkProject1
{
    class Crawler
    {

        private Hashtable urls = Hashtable.Synchronized(new Hashtable());
        private int count = 0, count2 = 0;

        static void Main(string[] args)
        {
            Crawler myCrawler = new Crawler();

            string startUrl = "http://www.4399.com";
            if (args.Length >= 1) startUrl = args[0];

            myCrawler.urls.Add(startUrl, false);

            new Thread(myCrawler.Crawl).Start();
        }

        private void Crawl()
        {
            Console.WriteLine("开始爬行了....");
            List<Task> taskList = new List<Task>();
            while (true)
            {
                string current = null;
                lock (this)
                {
                    foreach (string url in urls.Keys)
                    {
                        if ((bool)urls[url]) continue;
                        current = url;
                    }
                }
                
                if (current == null) continue;
                if(count > 100) break;
                urls[current] = true;
                Console.WriteLine("爬行" + current + "页面！");
                count++;
                taskList.Add(
                Task.Run(() =>
                {
                    Parse(DownLoad(current));
                }));
            }
            foreach(Task task in taskList)
            {
                Task.WaitAll(task);
            }
            Console.WriteLine("爬行结束！,共爬取"+count2+"个页面");
        }

        public string DownLoad(string url)
        {
            try
            {
                Console.WriteLine("开始下载:" + url);
                WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                string html = webClient.DownloadString(url);
                count2++;
                string fileName = count2.ToString();
                File.WriteAllText(fileName, html, Encoding.UTF8);
                Console.WriteLine("下载完成:" + url +"编号:"+count2);
                return html;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        public void Parse(string html)
        {
            Console.WriteLine("开始解析");
            string strRef = @"(href|HREF)[]*=[]*[""'][^""'#>]+[""']";
            MatchCollection matches = new Regex(strRef).Matches(html);
            lock (this)
            {
                foreach (Match match in matches)
                {
                    strRef = match.Value.Substring(match.Value.IndexOf('=') + 1).Trim('"', '\'', '#', ' ', '>');
                    if (strRef.Length == 0) continue;
                    if (urls[strRef] == null) urls[strRef] = false;
                }
            }
        }
    }
}
