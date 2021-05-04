namespace Giphy.Api.Dto
{
    public class GiphyDto
    {
        public string Slug {get;set;}
        public string Url {get;set;}
        public int Height {get;set;}
        public int Width {get;set;}

        public override bool Equals(object obj)
        {
            var info = obj as GiphyDto;
            if(info == null)
                return false;

            if(!string.Equals(info.Slug, this.Slug))
                return false;

            if(!string.Equals(info.Url, this.Url))
                return false;

            if(!string.Equals(info.Height, this.Height))
                return false;

            if(!string.Equals(info.Width, this.Width))
                return false;

            return true;
        }
    }
}