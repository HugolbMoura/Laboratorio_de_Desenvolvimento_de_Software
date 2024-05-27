using PdfSharp.Pdf;

public interface IModel
{
    event OperationCompletedEventHandler OperationCompleted;
    void UpdateSalesData();
    void StoreSalesComment();
}

public interface IView
{
    void ShowMessage(string message);
    void ShowError(string message);
    void ActivateInterface();
}

public interface IController
{
    void InsertSalesData();
    void InsertSalesComment();
    void RequestPdfGeneration();
}


public interface PDFGenerator
{
    byte[] GeneratePdf(List<Sale> salesData, List<SaleComment> salesComments);
}



public delegate void OperationCompletedEventHandler(object sender, OperationCompletedEventArgs e);

public class OperationCompletedEventArgs : EventArgs
{
    public string Message { get; }
    public bool IsError { get; }

    public OperationCompletedEventArgs(string message, bool isError)
    {
        Message = message;
        IsError = isError;
    }
}
