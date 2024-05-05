using System;

public class Model
{
    // Evento para notificar sobre operações concluídas
    public event EventHandler<string> OperationCompleted;

    // Método para atualizar os dados de venda
    public void UpdateSalesData()
    {
        try
        {
            // Simulação da atualização dos dados de venda
            OnOperationCompleted("Dados de venda atualizados com sucesso.");
        }
        catch (Exception ex)
        {
            OnOperationCompleted("Erro ao atualizar os dados de venda: " + ex.Message);
        }
    }

    // Método para armazenar o comentário sobre a venda
    public void StoreSalesComment()
    {
        try
        {
            // Simulação do armazenamento do comentário sobre a venda
            OnOperationCompleted("Comentário sobre venda armazenado com sucesso.");
        }
        catch (Exception ex)
        {
            OnOperationCompleted("Erro ao armazenar o comentário sobre a venda: " + ex.Message);
        }
    }

    // Método para acionar o evento OperationCompleted
    protected virtual void OnOperationCompleted(string message)
    {
        OperationCompleted?.Invoke(this, message);
    }
}

