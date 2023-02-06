using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ServiceModel.Syndication;
using System.Xml;
namespace WebApplication1.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }

    public IEnumerable <FeedItem> GetLatestFivePosts() {
        var reader = XmlReader.Create("https://svtv.org/feed/rss/");
        var feed = SyndicationFeed.Load(reader);
        reader.Close();
        return (from itm in feed.Items select new FeedItem {
            Title = itm.Title.Text, Link = itm.Id, description = itm.Summary.Text, date = itm.PublishDate.ToString()
        }).ToList();
    }

    public class FeedItem {
        public string Title {
            get;
            set;
        }
        public string Link {
            get;
            set;
        }

        public string description
        {
            get;
            set;
        }

        public string date
        {
            get;
            set;
        }
    }

    
}

