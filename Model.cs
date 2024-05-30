// Model.cs
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
        // Simulação de atualização de dados de vendas
        OnOperationCompleted(new OperationCompletedEventArgs("Dados de vendas atualizados com sucesso.", false));
    }

     public void SearchSalesData()
    {
        // Simulação de consulta de relatórios
        //OnOperationCompleted(new OperationCompletedEventArgs("Dados de vendas atualizados com sucesso.", false));
    }


    public void StoreSalesComment()
    {
        // Simulação de armazenamento de comentários de vendas
        OnOperationCompleted(new OperationCompletedEventArgs("Comentário de vendas armazenado com sucesso.", false));
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
                graphics.DrawImage(PdfSharp.Drawing.XImage.FromFile("logo.png"), 7, 1, 77, 77);

                 // Adicionar texto logo
                var logoFont = new XFont("Arial", 10);
                textFormatter.Alignment = XParagraphAlignment.Left;
                textFormatter.DrawString("CodeConquers", logoFont, PdfSharp.Drawing.XBrushes.MediumSlateBlue, new PdfSharp.Drawing.XRect(12,2,page.Width,page.Height));

                // Adicionar cabeçalho
                var headFont = new XFont("Arial", 14);
                textFormatter.Alignment = XParagraphAlignment.Center;
                textFormatter.DrawString("Relatório de Vendas", headFont, PdfSharp.Drawing.XBrushes.MediumSlateBlue, new PdfSharp.Drawing.XRect(0,7,page.Width,page.Height));

                 //centralizar texto
                textFormatter.Alignment = PdfSharp.Drawing.Layout.XParagraphAlignment.Center;


                // Adicionar retângulo com o nome do relatório
                var rect = new XRect(0, 50, page.Width, 30);
                graphics.DrawRectangle(XBrushes.LightBlue, rect);

                var font = new XFont("Arial", 22);
                textFormatter.Alignment = XParagraphAlignment.Center;
                textFormatter.DrawString(reportName, font, XBrushes.Green, rect);

                // Adicionar dados do relatório
                var dataFont = new XFont("Arial", 12);
                var yPosition = 90;
                textFormatter.Alignment = XParagraphAlignment.Left;

                textFormatter.DrawString($"Nome do Vendedor: {userName}", dataFont, XBrushes.Black, new XRect(20, yPosition, page.Width - 40, page.Height));
                yPosition += 30;
                textFormatter.DrawString($"Produto: {product}", dataFont, XBrushes.Black, new XRect(20, yPosition, page.Width - 40, page.Height));
                yPosition += 30;
                textFormatter.DrawString($"Data: {date.ToShortDateString()}", dataFont, XBrushes.Black, new XRect(20, yPosition, page.Width - 40, page.Height));
                yPosition += 30;
                textFormatter.DrawString($"Preço: {price:C}", dataFont, XBrushes.Black, new XRect(20, yPosition, page.Width - 40, page.Height));
                yPosition += 30;
                textFormatter.DrawString($"Comentário: {comments}", dataFont, XBrushes.Black, new XRect(20, yPosition, page.Width - 40, page.Height));
                 yPosition += 80;
                
                // Adicionar retângulo para footer
                var rectFooter = new XRect(0, 250, page.Width, 30);
                graphics.DrawRectangle(XBrushes.LightBlue, rectFooter);
                 yPosition += 4;
                var fontFooter = new XFont("Arial", 12);
                textFormatter.Alignment = XParagraphAlignment.Center;
                textFormatter.DrawString("CodeConquers Sales Report", fontFooter, XBrushes.Green, rectFooter);
                
                // Salvar PDF
                string fileName = "reports/" + reportName + ".pdf";

                doc.Save(fileName);

                // Abrir PDF
                Process.Start(new ProcessStartInfo(fileName) { UseShellExecute = true });
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
