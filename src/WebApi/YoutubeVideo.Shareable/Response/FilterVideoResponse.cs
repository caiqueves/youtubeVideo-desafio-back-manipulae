using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeVideo.Shareable.DTOs;

namespace YoutubeVideo.Shareable.Response
{
    public class FilterVideoResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;

        public List<VideoDto>? Videos { get; set; }
    }
}
