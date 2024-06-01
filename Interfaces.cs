public interface IModel
{
    event OperationCompletedEventHandler OperationCompleted;
    event EventHandler<PdfGenerationEventArgs> PdfGenerationRequested;

    void SearchSalesData();
    void SearchSalesDataByName(string reportName);
    void SearchSalesDataByDateRange(DateTime startDate, DateTime endDate);
    void GeneratePdf(string reportName, string userName, string product, DateTime date, decimal price, string comments);
    void EditReport(string reportName, string userName, string product, DateTime date, decimal price, string comments);
    void DeleteReport(string reportName);
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
    void SearchSalesData();
    void SearchSalesDataByName(string reportName);
    void SearchSalesDataByDateRange(DateTime startDate, DateTime endDate);
    void InsertSalesComment();
    void RequestPdfGeneration(string reportName, string userName, string product, DateTime date, decimal price, string comments);
    bool ReportExists(string reportName);
    void EditReport(string reportName, string userName, string product, DateTime date, decimal price, string comments);
    void DeleteReport(string reportName);
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
