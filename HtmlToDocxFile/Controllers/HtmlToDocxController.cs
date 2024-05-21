using HtmlToDocxFile.Entities;
using HtmlToDocxFile.Services;
using Microsoft.AspNetCore.Mvc;

namespace HtmlToDocxFile.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HtmlToDocxController : ControllerBase
{
    private readonly IHtmlToDocxFileservice _docxFileservice;

    public HtmlToDocxController(IHtmlToDocxFileservice docxFileservice)
    {
        _docxFileservice = docxFileservice;
    }
    

    [HttpPost("htmlCodeToDocx")]
    public async ValueTask<IActionResult> HtmlToDocxConvertor(string bodyContent)
    {
        await this._docxFileservice.HtmlToDocx(bodyContent);
        return Ok();
    }
}