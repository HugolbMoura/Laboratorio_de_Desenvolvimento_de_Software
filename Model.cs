using System;

public class Model : IModel
{
    public event OperationCompletedEventHandler OperationCompleted;

    public void UpdateSalesData()
    {
        try
        {
            // Simulação da atualização dos dados de venda
            OnOperationCompleted(new OperationCompletedEventArgs("Dados de venda atualizados com sucesso.", false));
        }
        catch (Exception ex)
        {
            OnOperationCompleted(new OperationCompletedEventArgs("Erro ao atualizar os dados de venda: " + ex.Message, true));
        }
    }

    public void StoreSalesComment()
    {
        try
        {
            // Simulação do armazenamento do comentário sobre a venda
            OnOperationCompleted(new OperationCompletedEventArgs("Comentário sobre venda armazenado com sucesso.", false));
        }
        catch (Exception ex)
        {
            OnOperationCompleted(new OperationCompletedEventArgs("Erro ao armazenar o comentário sobre a venda: " + ex.Message, true));
        }
    }

    protected virtual void OnOperationCompleted(OperationCompletedEventArgs e)
    {
        OperationCompleted?.Invoke(this, e);
    }
}
