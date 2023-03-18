using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Lab3
{
    [Serializable]
    public class WebPage
    {
        [NonSerialized]
        public Page Page;
        [NonSerialized]
        public WebBrowser WebBrowser;
        [NonSerialized]
        public string uri;
        public string Tag { get; set; }
        public string Uri
        {
            get
            {
                return WebBrowser.Source.OriginalString;
            }
            set
            {
                uri = value;
                WebBrowser.Source = new Uri(value);
            }
        }


        public WebPage()
        {
            Page = new Page();
            WebBrowser = new WebBrowser();

            Page.Content = WebBrowser;
        }

        public WebPage(string tag, string uri) : this()
        {
            Tag = tag;
            Uri = uri;
        }
    }

}
