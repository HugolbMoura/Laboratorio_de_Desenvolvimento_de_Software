// Interfaces.cs
public interface IModel
{
    event OperationCompletedEventHandler OperationCompleted;
    event EventHandler<PdfGenerationEventArgs> PdfGenerationRequested;

    void UpdateSalesData();
    void StoreSalesComment();
    void GeneratePdf(string reportName, string userName, string product, DateTime date, decimal price, string comments);
}

public interface IView
{
    void ActivateInterface();
    void ShowMessage(string message);
    void ShowError(string message);
    string RequestStringInput(string prompt);
    DateTime RequestDateInput(string prompt);
    decimal RequestDecimalInput(string prompt);
}

public interface IController
{
    void InsertSalesData();
    void InsertSalesComment();
    void RequestPdfGeneration(string reportName, string userName, string product, DateTime date, decimal price, string comments);
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