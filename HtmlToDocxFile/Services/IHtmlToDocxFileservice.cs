using HtmlToDocxFile.Entities;

namespace HtmlToDocxFile.Services;

public interface IHtmlToDocxFileservice
{
    ValueTask HtmlToDocx(string content);
}