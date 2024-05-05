using System;

public class Controller
{
    private readonly Model _model;
    private readonly View _view;

    public Controller(Model model, View view)
    {
        _model = model;
        _view = view;

        _model.OperationCompleted += ModelOperationCompleted;
        _view.UpdateInterface += ViewUpdateInterface;
    }

    public void InicializarSistema()
    {
        try
        {
            _view.AtivarInterface();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao inicializar o sistema: " + ex.Message);
        }
    }

    public void InserirDadosVenda()
    {
        try
        {
            _model.AtualizarDadosVenda();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao inserir dados de venda: " + ex.Message);
        }
    }

    public void InserirComentarioVenda()
    {
        try
        {
            _model.ArmazenarComentario();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao inserir comentário de venda: " + ex.Message);
        }
    }

    private void ModelOperationCompleted(object sender, string message)
    {
        try
        {
            _view.ExibirMensagem(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao exibir mensagem: " + ex.Message);
        }
    }

    private void ViewUpdateInterface(object sender, ViewEventArgs e)
    {
        try
        {
            _view.ExibirMensagem(e.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao atualizar interface: " + ex.Message);
        }
    }
}
