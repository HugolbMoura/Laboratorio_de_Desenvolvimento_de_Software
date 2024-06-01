using System;
using System.IO;

public class Controller : IController
{
    private readonly IModel _model;
    private readonly IView _view;

    // Evento para solicitação de geração de PDF
    public event EventHandler? GeneratePdfRequested = delegate { };

    // O controlador é uma parte importante do nosso programa que coordena a interação entre a visualização e o modelo. 
    // Ele recebe entradas do usuário, solicita ações ao modelo e atualiza a visualização com base nos resultados.
    // Neste caso, estamos declarando que o controlador implementa a interface IController.

    public Controller(IModel model, IView view)
    {
        // Aqui estamos criando uma nova instância do controlador. 
        // Ele precisa de uma referência para o modelo e para a visualização para funcionar corretamente.
        // Essas referências são passadas como parâmetros no construtor.
        _model = model;
        _view = view;
        _model.OperationCompleted += ModelOperationCompleted;
        // Além disso, estamos nos inscrevendo no evento OperationCompleted do modelo. 
        // Isso nos permitirá receber notificações quando o modelo concluir uma operação.
    }

    public void InsertSalesData()
    {
        // Este método ainda não foi implementado.
        // Este método será responsável por inserir dados de vendas
        
    }

    public void SearchSalesData()
    {
        // Este método ainda não foi implementado.
        // Este método solicita ao modelo que execute a pesquisa de dados de vendas.
        _model.SearchSalesData();
    }

    public void DeleteReport(string reportName)
    {
        // Este método solicita ao modelo que exclua um relatório com o nome fornecido.
        _model.DeleteReport(reportName);
    }

    public void InsertSalesComment()
    {
        // Este método ainda não foi implementado.
         // Este método será responsável por inserir comentários sobre vendas
    }

    public void RequestPdfGeneration(string reportName, string userName, string product, decimal price, string comments)
    {
        // Este método solicita ao modelo que gere um PDF com os dados fornecidos.
        _model.GeneratePdf(reportName, userName, product, price, comments);
    }

    public bool ReportExists(string reportName)
    {
        // Este método verifica se um relatório com o nome fornecido já existe.
        string filePath = "reports/" + reportName + ".pdf";
        return File.Exists(filePath);
    }

    public void EditReport(string reportName, string userName, string product, decimal price, string comments)
    {
        // Este método solicita ao modelo que edite um relatório com os dados fornecidos.
        _model.EditReport(reportName, userName, product, price, comments);
    }

    private void ModelOperationCompleted(object sender, OperationCompletedEventArgs e)
    {
        // Este método é chamado sempre que uma operação no modelo é concluída.
        // Dependendo se houve um erro ou não, ele atualiza a visualização com uma mensagem apropriada.
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