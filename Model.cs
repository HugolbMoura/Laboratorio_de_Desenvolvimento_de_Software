using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Model : IModel
{
    public event OperationCompletedEventHandler OperationCompleted;
    public event EventHandler<PdfGenerationEventArgs>? PdfGenerationRequested;

    public void UpdateSalesData()
    {
        // Implementação omitida para brevidade
    }

    public void StoreSalesComment()
    {
        // Implementação omitida para brevidade
    }

    public void GeneratePdf(string reportName, string userName, string product, DateTime date, decimal price, string comments)
    {
        try
        {
            using (var doc = new PdfSharp.Pdf.PdfDocument())
            {
                var page = doc.AddPage();
                var graphics = PdfSharp.Drawing.XGraphics.FromPdfPage(page);
                var textFormatter = new PdfSharp.Drawing.Layout.XTextFormatter(graphics);

                // Adicionar logo
                graphics.DrawImage(PdfSharp.Drawing.XImage.FromFile("logo.png"), 7, 7);

                // Adicionar retângulo com o nome do relatório
                var rect = new XRect(0, 50, page.Width, 30);
                graphics.DrawRectangle(XBrushes.LightBlue, rect);

                var font = new XFont("Arial", 12);
                textFormatter.Alignment = XParagraphAlignment.Center;
                textFormatter.DrawString(reportName, font, XBrushes.Black, rect);

                // Adicionar dados do relatório
                var dataFont = new XFont("Arial", 10);
                var yPosition = 90;
                textFormatter.Alignment = XParagraphAlignment.Left;

                textFormatter.DrawString($"Nome do Vendedor: {userName}", dataFont, XBrushes.Black, new XRect(20, yPosition, page.Width - 40, page.Height));
                yPosition += 20;
                textFormatter.DrawString($"Produto: {product}", dataFont, XBrushes.Black, new XRect(20, yPosition, page.Width - 40, page.Height));
                yPosition += 20;
                textFormatter.DrawString($"Data: {date.ToShortDateString()}", dataFont, XBrushes.Black, new XRect(20, yPosition, page.Width - 40, page.Height));
                yPosition += 20;
                textFormatter.DrawString($"Preço: {price:C}", dataFont, XBrushes.Black, new XRect(20, yPosition, page.Width - 40, page.Height));
                yPosition += 20;
                textFormatter.DrawString($"Observações: {comments}", dataFont, XBrushes.Black, new XRect(20, yPosition, page.Width - 40, page.Height));

                // Salvar PDF
                string fileName = "reports/report.pdf";
                doc.Save(fileName);

                // Abrir PDF
                System.Diagnostics.Process.Start(fileName);
                //Process.Start(new ProcessStartInfo(fileName) { UseShellExecute = true });
            }

            OnOperationCompleted(new OperationCompletedEventArgs("PDF gerado com sucesso.", false));
        }
        catch (Exception ex)
        {
            OnOperationCompleted(new OperationCompletedEventArgs("Erro ao gerar PDF: " + ex.Message, true));
        }
    }

    protected virtual void OnOperationCompleted(OperationCompletedEventArgs e)
    {
        OperationCompleted?.Invoke(this, e);
    }

    protected virtual void OnPdfGenerationRequested(PdfGenerationEventArgs e)
    {
        PdfGenerationRequested?.Invoke(this, e);
    }
}
