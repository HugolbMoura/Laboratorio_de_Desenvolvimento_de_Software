using System;
using System.IO;

public class Controller : IController
{
    private readonly IModel _model;
    private readonly IView _view;

    public event EventHandler? GeneratePdfRequested = delegate { };

    public Controller(IModel model, IView view)
    {
        _model = model;
        _view = view;
        _model.OperationCompleted += ModelOperationCompleted;
    }

    public void InsertSalesData()
    {
    }

    public void SearchSalesData()
    {
        _model.SearchSalesData();
    }

    public void SearchSalesDataByName(string reportName)
    {
        _model.SearchSalesDataByName(reportName);
    }

    public void SearchSalesDataByDateRange(DateTime startDate, DateTime endDate)
    {
        _model.SearchSalesDataByDateRange(startDate, endDate);
    }

    public void DeleteReport(string reportName)
    {
        _model.DeleteReport(reportName);
    }

    public void InsertSalesComment()
    {
    }

    public void RequestPdfGeneration(string reportName, string userName, string product, DateTime date, decimal price, string comments)
    {
        _model.GeneratePdf(reportName, userName, product, date, price, comments);
    }

    public bool ReportExists(string reportName)
    {
        string filePath = "reports/" + reportName + ".pdf";
        return File.Exists(filePath);
    }

    public void EditReport(string reportName, string userName, string product, DateTime date, decimal price, string comments)
    {
        _model.EditReport(reportName, userName, product, date, price, comments);
    }

    private void ModelOperationCompleted(object sender, OperationCompletedEventArgs e)
    {
        if (e.IsError)
        {
            _view.ShowError(e.Message);
        }
        else
        {
            _view.ShowMessage(e.Message);
        }
    }
}
