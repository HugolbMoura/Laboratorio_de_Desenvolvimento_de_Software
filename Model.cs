using System;

public class Model
{
    public event EventHandler<string> OperationCompleted;

    public void AtualizarDadosVenda()
    {
        OnOperationCompleted("Dados de venda atualizados com sucesso.");
    }

    public void ArmazenarComentario()
    {
        OnOperationCompleted("Comentário sobre venda armazenado com sucesso.");
    }

    protected virtual void OnOperationCompleted(string message)
    {
        OperationCompleted?.Invoke(this, message);
    }
}

