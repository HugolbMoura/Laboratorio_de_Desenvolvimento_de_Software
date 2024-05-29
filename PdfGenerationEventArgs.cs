using System;

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
