using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

public class Model : IModel
{
    public event OperationCompletedEventHandler OperationCompleted;
    public event EventHandler<PdfGenerationEventArgs>? PdfGenerationRequested;

    public Model()
    {
        OperationCompleted += delegate { };
    }

    public void SearchSalesData()
    {
        var reports = Directory.GetFiles("reports", "*.pdf");
        foreach (var report in reports)
        {
            Console.WriteLine(Path.GetFileNameWithoutExtension(report));
        }
    }

    public void SearchSalesDataByName(string reportName)
    {
        string filePath = "reports/" + reportName + ".pdf";
        if (File.Exists(filePath))
        {
            Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
        }
        else
        {
            OperationCompleted(this, new OperationCompletedEventArgs("Relatório não encontrado.", true));
        }
    }

    public void SearchSalesDataByDateRange(DateTime startDate, DateTime endDate)
    {
        var reports = Directory.GetFiles("reports", "*.pdf");
        foreach (var report in reports)
        {
            FileInfo fileInfo = new FileInfo(report);
            if (fileInfo.CreationTime >= startDate && fileInfo.CreationTime <= endDate)
            {
                Console.WriteLine(Path.GetFileNameWithoutExtension(report));
            }
        }
    }

    public void GeneratePdf(string reportName, string userName, string product, DateTime date, decimal price, string comments)
    {
        string directoryPath = "reports";
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        string filePath = Path.Combine(directoryPath, reportName + ".pdf");

        using (PdfDocument document = new PdfDocument())
        {
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Arial", 12);
            XTextFormatter tf = new XTextFormatter(gfx);

            tf.DrawString("Nome do Relatório: " + reportName, font, XBrushes.Black, new XRect(40, 50, page.Width - 80, 20));
            tf.DrawString("Nome do Vendedor: " + userName, font, XBrushes.Black, new XRect(40, 80, page.Width - 80, 20));
            tf.DrawString("Produto: " + product, font, XBrushes.Black, new XRect(40, 110, page.Width - 80, 20));
            tf.DrawString("Data: " + DateTime.Now.ToString("dd/MM/yyyy"), font, XBrushes.Black, new XRect(40, 140, page.Width - 80, 20));
            tf.DrawString("Preço: " + price.ToString("C"), font, XBrushes.Black, new XRect(40, 170, page.Width - 80, 20));
            tf.DrawString("Comentário: " + comments, font, XBrushes.Black, new XRect(40, 200, page.Width - 80, 20));

            document.Save(filePath);
        }

        OperationCompleted(this, new OperationCompletedEventArgs("PDF gerado com sucesso.", false));
    }

    public void EditReport(string reportName, string userName, string product, DateTime date, decimal price, string comments)
    {
        string filePath = "reports/" + reportName + ".pdf";
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            GeneratePdf(reportName, userName, product, date, price, comments);
        }
        else
        {
            OperationCompleted(this, new OperationCompletedEventArgs("Relatório não encontrado.", true));
        }
    }

    public void DeleteReport(string reportName)
    {
        string filePath = "reports/" + reportName + ".pdf";
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            OperationCompleted(this, new OperationCompletedEventArgs("Relatório deletado com sucesso.", false));
        }
        else
        {
            OperationCompleted(this, new OperationCompletedEventArgs("Relatório não encontrado.", true));
        }
    }
}

public class PdfGenerationEventArgs : EventArgs
{
    public string ReportName { get; }
    public string UserName { get; }
    public string Product { get; }
    public DateTime Date { get; }
    public decimal Price { get; }
    public string Comments { get; }

    public PdfGenerationEventArgs(string reportName, string userName, string product, DateTime date, decimal price, string comments)
    {
        ReportName = reportName;
        UserName = userName;
        Product = product;
        Date = date;
        Price = price;
        Comments = comments;
    }
}
