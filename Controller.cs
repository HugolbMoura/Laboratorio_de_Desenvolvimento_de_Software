// Controller.cs
using System;

public class Controller : IController
{
    private readonly IModel _model;
    private readonly IView _view;

   public event EventHandler? GeneratePdfRequested  = delegate { };

    public Controller(IModel model, IView view)
    {
        _model = model;
        _view = view;
        _model.OperationCompleted += ModelOperationCompleted;
    }

     public void InsertSalesData()
    {
        // Para edição de relatório, coletamos informações como no CreateReport
        string reportName = _view.RequestStringInput("Insira o nome do relatório a editar:");
        string userName = _view.RequestStringInput("Insira o novo nome do vendedor:");
        string product = _view.RequestStringInput("Insira o novo produto:");
        DateTime date = _view.RequestDateInput("Insira a nova data:");
        decimal price = _view.RequestDecimalInput("Insira o novo preço:");
        string comments = _view.RequestStringInput("Insira o novo comentário:");

        _model.EditReport(reportName, userName, product, date, price, comments);
    }

    public void SearchSalesData()
    {
        _model.SearchSalesData();
    }

    public void DeleteReport()
    {
        string reportName = _view.RequestStringInput("Insira o nome do relatório a eliminar:");
        _model.DeleteReport(reportName);
    }

    public void InsertSalesComment()
    {
        //_model.StoreSalesComment();
    }

    public void RequestPdfGeneration(string reportName, string userName, string product, DateTime date, decimal price, string comments)
    {
        _model.GeneratePdf(reportName, userName, product, date, price, comments);
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