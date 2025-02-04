using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeVideo.Shareable.Response
{
    public class YoutubeApiResponse
    {
        public List<YoutubeItem> Items { get; set; }
    }

    public class YoutubeItem
    {
        public YoutubeId Id { get; set; }
        public YoutubeSnippet Snippet { get; set; }
    }

    public class YoutubeId
    {
        public string VideoId { get; set; }
    }

    public class YoutubeSnippet
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ChannelTitle { get; set; }
        public string PublishedAt { get; set; }
    }
}
