using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TaoyuanActivity.Models
{
    public class Activity
    {
        [DisplayName("目標族群")]
        public string Age { get; set; }

        public string ActType { get; set; }

        public string Id { get; set; }
        public string Seq { get; set; }

        public string Text { get; set; }
        public string Value { get; set; }
        public string SitePubPri { get; set; }
        public string SiteName { get; set; }
        public string SiteAddressName { get; set; }
        public string SitePhone { get; set; }
        public string SiteDType { get; set; }
        public string SitePing { get; set; }




    }
}