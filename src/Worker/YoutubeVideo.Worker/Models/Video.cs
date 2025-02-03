using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeVideo.Worker.Models
{
    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ChannelTitle { get; set; }
        public string Author { get; set; }
        public string Duration { get; set; }
        public DateTime PublishedAt { get; set; }
        public string LinkVideo { get; set; }
        public bool IsDeleted { get; set; }
    }
}
