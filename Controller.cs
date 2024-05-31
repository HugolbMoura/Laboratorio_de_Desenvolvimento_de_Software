// Controller.cs
using System;
using System.IO;

public class Controller : IController
{
    private readonly IModel _model;
    private readonly IView _view;

    // Evento para solicitação de geração de PDF
    public event EventHandler? GeneratePdfRequested = delegate { };

    public Controller(IModel model, IView view)
    {
        _model = model;
        _view = view;
        _model.OperationCompleted += ModelOperationCompleted;
    }

    public void InsertSalesData()
    {
        // Lógica movida para o método EditReport
    }

    public void SearchSalesData()
    {
        _model.SearchSalesData();
    }

    public void DeleteReport(string reportName)
    {
        _model.DeleteReport(reportName);
    }

    public void InsertSalesComment()
    {
        // Lógica não implementada
    }

    public void RequestPdfGeneration(string reportName, string userName, string product, DateTime date, decimal price, string comments)
    {
        _model.GeneratePdf(reportName, userName, product, date, price, comments);
    }

    public bool ReportExists(string reportName)
    {
        // Verifica se o arquivo de relatório já existe
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
