using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using HtmlToDocxFile.Entities;

namespace HtmlToDocxFile.Services;

public class HtmlToDocxFileServices : IHtmlToDocxFileservice
{
    public async ValueTask HtmlToDocx(string content)
    {
        
        string mainDocPath = @"C:\File\TpiFile.docx";
        
        using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(mainDocPath, true))
        {
            MainDocumentPart mainPart = wordDoc.MainDocumentPart;
            Body body = mainPart.Document.Body;

            Paragraph targetParagraph = body.Elements<Paragraph>()
                .Where(p => p.InnerText.Contains("INSERT_HERE"))
                .FirstOrDefault();

            if (targetParagraph != null)
            {
                
                string altChunkId = "AltChunkId1";
                AlternativeFormatImportPart chunk = mainPart.AddAlternativeFormatImportPart(
                    AlternativeFormatImportPartType.Html, altChunkId);

                
                string tempHtmlPath = Path.GetTempFileName();
                File.WriteAllText(tempHtmlPath, content);

                
                using (FileStream fileStream = File.Open(tempHtmlPath, FileMode.Open))
                {
                    chunk.FeedData(fileStream);
                }

                
                AltChunk altChunk = new AltChunk() { Id = altChunkId };

                
                targetParagraph.InsertBeforeSelf(altChunk);
                targetParagraph.Remove();
            }
        }
    }
}