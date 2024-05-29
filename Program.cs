partial class Program
{
    static void Main(string[] args)
    {
        IModel model = new Model();
        IView view = new View();
        IController controller = new Controller(model, view);

        view.ActivateInterface();

        controller.InsertSalesData();
        controller.InsertSalesComment();

        // Solicitação dos dados ao usuário
        string reportName = view.RequestStringInput("Insira o nome do relatório:");
        string userName = view.RequestStringInput("Insira o nome do vendedor:");
        string product = view.RequestStringInput("Insira o produto:");
        DateTime date = view.RequestDateInput("Insira a data:");
        decimal price = view.RequestDecimalInput("Insira o preço:");
        string comments = view.RequestStringInput("Insira comentário:");

        // Solicitação de geração do PDF com os dados fornecidos pelo usuário
        controller.RequestPdfGeneration(reportName, userName, product, date, price, comments);

        try
        {
            Console.WriteLine("Pressione qualquer tecla para fechar o programa...");
            Console.ReadKey();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Não foi possível ler a entrada do teclado: " + ex.Message);
            Console.WriteLine("Pressione Enter para fechar o programa...");
            Console.ReadLine();
        }
    }
}
