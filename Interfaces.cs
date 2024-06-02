using PdfSharp.Pdf;

// Definimos a interface IModel que será usada para implementar o modelo no padrão MVC.
public interface IModel
{
    // Evento que será disparado quando uma operação for concluída.
    event OperationCompletedEventHandler OperationCompleted;

    // Evento que será disparado quando for solicitada a geração de um PDF.
    event EventHandler<PdfGenerationEventArgs> PdfGenerationRequested;

    // Método para procurar dados de vendas.
    void SearchSalesData();

    // Método para gerar um PDF com os detalhes fornecidos.
    void GeneratePdf(string reportName, string userName, string product, decimal price, string comments);

    // Método para editar um relatório existente com novos dados.
    void EditReport(string reportName, string userName, string product, decimal price, string comments);

    // Método para excluir um relatório pelo nome.
    void DeleteReport(string reportName);

    // Método para visualizar um relatório específico
    void ViewReport(string reportName);

    // Método para obter uma lista de relatórios existentes
    string[] GetReportList();

    string[] GetReportsBetweenDates(DateTime startDate, DateTime endDate);
    bool IsFileCreatedBetweenDates(string filePath, DateTime startDate, DateTime endDate);

}

// Definimos a interface IView que será usada para implementar a visualização no padrão MVC.
public interface IView
{
    // Método para ativar a interface de usuário.
    void ActivateInterface();

    // Método para mostrar uma mensagem ao usuário.
    void ShowMessage(string message);

    // Método para mostrar uma mensagem de erro ao usuário.
    void ShowError(string message);

    // Método para solicitar uma entrada de string do usuário.
    string RequestStringInput(string prompt);

    // Método para solicitar uma entrada de valor decimal do usuário.
    decimal RequestDecimalInput(string prompt);

    string RequestReportName();

    // Método para solicitar ao usuário o nome do relatório que deseja visualizar
    string RequestReportSelection(string[] reportList);

    // Método para solicitar duas datas do usuário e retornar como um array de strings
    string[] RequestDateRange();
}

// Definimos a interface IController que será usada para implementar o controlador no padrão MVC.
public interface IController
{
    // Método para inserir dados de vendas.
    void InsertSalesData();

    // Método para procurar dados de vendas.
    void SearchSalesData();

    // Método para inserir um comentário sobre uma venda.
    void InsertSalesComment();

    // Método para solicitar a geração de um PDF com os detalhes fornecidos.
    void RequestPdfGeneration(string reportName, string userName, string product, decimal price, string comments);

    // Método para verificar se um relatório existe pelo nome.
    bool ReportExists(string reportName);

    // Método para editar um relatório existente com novos dados.
    void EditReport(string reportName, string userName, string product, decimal price, string comments);

    // Método para excluir um relatório pelo nome.
    void DeleteReport(string reportName);

    void ViewReports();
}


public interface PDFGenerator
{
    byte[] GeneratePdf(List<Sale> salesData, List<SaleComment> salesComments);
}



// Delegado que define a assinatura do evento de operação concluída.
public delegate void OperationCompletedEventHandler(object sender, OperationCompletedEventArgs e);

// Classe que encapsula os argumentos do evento de operação concluída.
public class OperationCompletedEventArgs : EventArgs
{
    // Propriedade que armazena a mensagem da operação.
    public string Message { get; }

    // Propriedade que indica se houve um erro na operação.
    public bool IsError { get; }

    // Construtor que inicializa as propriedades Message e IsError.
    public OperationCompletedEventArgs(string message, bool isError)
    {
        Message = message;
        IsError = isError;
    }
}