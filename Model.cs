// Model.cs
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf.IO;
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
    
      public void SearchSalesData()
    {
        // Lista todos os relatórios na pasta "reports"
        var reports = Directory.GetFiles("reports", "*.pdf");
        if (reports.Length == 0)
        {
            OnOperationCompleted(new OperationCompletedEventArgs("Nenhum relatório encontrado.", false));
        }
        else
        {
            var message = "Relatórios encontrados:\n";
            foreach (var report in reports)
            {
                message += Path.GetFileName(report) + "\n";
            }
            OnOperationCompleted(new OperationCompletedEventArgs(message, false));
        }
    }

    public void EditReport(string reportName, string userName, string product, DateTime date, decimal price, string comments)
    {
        string filePath = "reports/" + reportName + ".pdf";
        if (File.Exists(filePath))
        {
            GeneratePdf(reportName, userName, product, date, price, comments);
            OnOperationCompleted(new OperationCompletedEventArgs("Relatório editado com sucesso.", false));
        }
        else
        {
            OnOperationCompleted(new OperationCompletedEventArgs("Relatório não encontrado.", true));
        }
    }

    public void DeleteReport(string reportName)
    {
        string filePath = "reports/" + reportName + ".pdf";
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            OnOperationCompleted(new OperationCompletedEventArgs("Relatório eliminado com sucesso.", false));
        }
        else
        {
            OnOperationCompleted(new OperationCompletedEventArgs("Relatório não encontrado.", true));
        }
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
             string filePath = "reports/" + reportName + ".pdf";
             if (File.Exists(filePath))
             {
               OnOperationCompleted(new OperationCompletedEventArgs("Já existe um relatório com este nome.", true));
               return; // Retorna imediatamente se o relatório já existir
             }     
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
                    textFormatter.DrawString("CodeConquers", logoFont, PdfSharp.Drawing.XBrushes.MediumSlateBlue, new PdfSharp.Drawing.XRect(12, 2, page.Width, page.Height));

                    // Adicionar cabeçalho
                    var headFont = new XFont("Arial", 14);
                    textFormatter.Alignment = XParagraphAlignment.Center;
                    textFormatter.DrawString("Relatório de Vendas", headFont, PdfSharp.Drawing.XBrushes.MediumSlateBlue, new PdfSharp.Drawing.XRect(0, 7, page.Width, page.Height));

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

                    OnOperationCompleted(new OperationCompletedEventArgs("PDF gerado com sucesso.", false));
                }
            
            
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
