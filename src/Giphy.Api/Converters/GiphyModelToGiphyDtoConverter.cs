using System.Collections.Generic;
using System.Text.Json;
using Giphy.Api.Dto;
using Giphy.Api.Model;

namespace Giphy.Api.Converters
{
    public static class GiphyModelToGiphyDtoConverter
    {
        public static GiphyDto[] ToDtosArray(this GiphyModel model)
        {
            var list = new List<GiphyDto>();

            foreach(var item in model.data)
            {
                list.Add( new GiphyDto() {
                    Slug = item.slug,
                    Url = item.images.preview_gif.url ?? item.images.original.url,
                    Height = int.Parse(item.images.preview_gif.height ?? item.images.original.height),
                    Width = int.Parse(item.images.preview_gif.width ?? item.images.original.width)
                });
            }

            return list.ToArray();
        }
    }
}