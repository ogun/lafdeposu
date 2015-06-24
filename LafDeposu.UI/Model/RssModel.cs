using LafDeposu.Helper.Rss;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Xml;

namespace LafDeposu.UI.Model {
    public class RssModel {
        public List<KeyValuePair<string, string>> RssFeed;

        public void Load() {
            RssFeed = RssHelper.GetGoogleFeed();
        }
    }
}