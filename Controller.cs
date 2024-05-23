using System;

public class Controller
{
    private readonly Model _model;
    private readonly View _view;

    // Evento para sinalizar a necessidade de gerar um PDF
    public event EventHandler GeneratePdfRequested;

    // Construtor que recebe instâncias de Model e View
    public Controller(Model model, View view)
    {
        _model = model;
        _view = view;

        // Inscrever os métodos de tratamento de eventos do model
        _model.OperationCompleted += ModelOperationCompleted;
    }

    // Método para inserir os dados de venda
    public void InsertSalesData()
    {
        _model.UpdateSalesData();
    }

    // Método para inserir o comentário sobre a venda
    public void InsertSalesComment()
    {
        _model.StoreSalesComment();
    }

    // Método para lidar com a conclusão das operações do model
    private void ModelOperationCompleted(object sender, OperationCompletedEventArgs e)
    {
        try
        {
            if (e.IsError)
            {
                // Tratamento especial para erros
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

    // Método para solicitar a geração de um PDF
    public void RequestPdfGeneration()
    {
        // Se houver assinantes para o evento de geração de PDF, acione-o
        GeneratePdfRequested?.Invoke(this, EventArgs.Empty);
    }
}
