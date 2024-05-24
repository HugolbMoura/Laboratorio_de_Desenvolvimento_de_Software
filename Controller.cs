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
    }

    public void InsertSalesData()
    {
        _model.UpdateSalesData();
    }

    public void InsertSalesComment()
    {
        _model.StoreSalesComment();
    }

    private void ModelOperationCompleted(object sender, OperationCompletedEventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao exibir mensagem: " + ex.Message);
        }
    }

    public void RequestPdfGeneration()
    {
        GeneratePdfRequested?.Invoke(this, EventArgs.Empty);
    }
}
