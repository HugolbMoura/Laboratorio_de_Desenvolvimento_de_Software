using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;

public class PdfSharpGenerator : PDFGenerator
{
    public byte[] GeneratePdf(List<Sale> salesData, List<SaleComment> salesComments)
    {
        using (var document = new PdfDocument())
        {
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            var font = new XFont("Verdana", 12, XFontStyle.Regular);

            int yPoint = 0;

            gfx.DrawString("Relatório de Vendas", font, XBrushes.Black, 
                new XRect(0, yPoint, page.Width, page.Height),
                XStringFormats.TopCenter);

            yPoint += 40;

            foreach (var sale in salesData)
            {
                var comment = salesComments.Find(c => c.SaleId == sale.SaleId)?.Comment ?? "Sem comentário";
                gfx.DrawString($"ID: {sale.SaleId}, Produto: {sale.ProductName}, Preço: {sale.Price:C}, Data: {sale.SaleDate.ToShortDateString()}, Comentário: {comment}", 
                    font, XBrushes.Black, new XRect(20, yPoint, page.Width - 40, page.Height));
                yPoint += 30;
            }

            using (var stream = new MemoryStream())
            {
                document.Save(stream, false);
                return stream.ToArray();
            }
        }
    }
}
