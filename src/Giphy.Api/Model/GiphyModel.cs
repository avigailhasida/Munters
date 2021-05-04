namespace Giphy.Api.Model
{
    public class GiphyModel
    {
        public DataModel[] data {get;set;}
    }

    public class DataModel
    {
        public string slug {get;set;}
        public ImagesModel images {get;set;}
    }

    public class ImagesModel
    {
        public GifModel original {get;set;}
        public GifModel preview_gif {get;set;}
    }

    public class GifModel
    {
        public string height {get;set;}
        public string width {get;set;}
        public string url {get;set;}
    }
}