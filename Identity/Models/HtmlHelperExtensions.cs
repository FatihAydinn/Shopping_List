using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shopping_List.Models
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent Image(this IHtmlHelper helper, string src, int width, int height)
        {
            TagBuilder imgBuilder = new TagBuilder("img");
            //imgBuilder.Attributes.Add("class", "btn");
            //imgBuilder.AddCssClass("btn");

            //imgBuilder.Attributes["src"] = src;
            //imgBuilder.Attributes["width"] = width.ToString();
            //imgBuilder.Attributes["height"] = height.ToString();

            imgBuilder.Attributes.Add("src", src);
            imgBuilder.Attributes.Add("width", width.ToString());
            imgBuilder.Attributes.Add("height", height.ToString());
            imgBuilder.TagRenderMode = TagRenderMode.SelfClosing;

            return imgBuilder;
        }
    }
}
