using System;

public class Controller : IController
{
    private readonly IModel _model;
    private readonly IView _view;

    public event EventHandler GeneratePdfRequested;

    public Controller(IModel model, IView view)
    {
        _model = model;
        _view = view;
        _model.OperationCompleted += ModelOperationCompleted;
        _model.PdfGenerationRequested += ModelPdfGenerationRequested;
    }

    public void InsertSalesData()
    {
        _model.UpdateSalesData();
    }

    public void InsertSalesComment()
    {
        _model.StoreSalesComment();
    }

    public void RequestPdfGeneration(string reportName, string userName, string product, DateTime date, decimal price, string comments)
    {
        _model.GeneratePdf(reportName, userName, product, date, price, comments);
    }

    private void ModelOperationCompleted(object sender, OperationCompletedEventArgs e)
    {
        // Implementação omitida para brevidade
    }

    private void ModelPdfGenerationRequested(object sender, PdfGenerationEventArgs e)
    {
        // Implementação omitida para brevidade
    }
}
