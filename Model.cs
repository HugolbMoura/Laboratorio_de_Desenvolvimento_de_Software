using System;

public class Model
{
    public event EventHandler<string> OperationCompleted;

    public void AtualizarDadosVenda()
    {
        try
        {
            // Código para atualizar os dados de venda
            OnOperationCompleted("Dados de venda atualizados com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao atualizar os dados de venda: " + ex.Message);
        }
    }

    public void ArmazenarComentario()
    {
        try
        {
            // Código para armazenar o comentário sobre a venda
            OnOperationCompleted("Comentário sobre venda armazenado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao armazenar o comentário sobre a venda: " + ex.Message);
        }
    }

    protected virtual void OnOperationCompleted(string message)
    {
        OperationCompleted?.Invoke(this, message);
    }
}
