using System;

public class Model : IModel
{
    public event OperationCompletedEventHandler OperationCompleted;
    public event EventHandler<PdfGenerationEventArgs> PdfGenerationRequested;

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
            // Lógica para gerar o PDF utilizando PdfSharp
            // Esta parte do código foi omitida para brevidade

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

public class PdfGenerationEventArgs : EventArgs
{
    public string ReportName { get; set; }
    public string UserName { get; set; }
    public string Product { get; set; }
    public DateTime Date { get; set; }
    public decimal Price { get; set; }
    public string comments { get; set; }
}
