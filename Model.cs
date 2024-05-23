using System;

// Delegado customizado para o evento OperationCompleted
public delegate void OperationCompletedEventHandler(object sender, OperationCompletedEventArgs e);

public class Model
{
    // Evento para notificar sobre operações concluídas
    public event OperationCompletedEventHandler OperationCompleted;

    // Método para atualizar os dados de venda
    public void UpdateSalesData()
    {
        try
        {
            // Simulação da atualização dos dados de venda
            OnOperationCompleted("Dados de venda atualizados com sucesso.", false);
        }
        catch (Exception ex)
        {
            OnOperationCompleted("Erro ao atualizar os dados de venda: " + ex.Message, true);
        }
    }

    // Método para armazenar o comentário sobre a venda
    public void StoreSalesComment()
    {
        try
        {
            // Simulação do armazenamento do comentário sobre a venda
            OnOperationCompleted("Comentário sobre venda armazenado com sucesso.", false);
        }
        catch (Exception ex)
        {
            OnOperationCompleted("Erro ao armazenar o comentário sobre a venda: " + ex.Message, true);
        }
    }

    // Método para acionar o evento OperationCompleted
    protected virtual void OnOperationCompleted(string message, bool isError)
    {
        OperationCompleted?.Invoke(this, new OperationCompletedEventArgs(message, isError));
    }
}

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
