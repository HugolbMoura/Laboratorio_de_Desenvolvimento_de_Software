// Program.cs
partial class Program
{
    static void Main(string[] args)
    {
        IModel model = new Model();
        IView view = new View();
        IController controller = new Controller(model, view);

        view.ActivateInterface();

        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Criar Relatório");
            Console.WriteLine("2. Editar Relatório");
            Console.WriteLine("3. Eliminar Relatório");
            Console.WriteLine("4. Sair");

            string choice = view.RequestStringInput("Escolha uma opção:");

            switch (choice)
            {
                case "1":
                    CreateReport(controller, view);
                    break;
                case "2":
                    controller.InsertSalesData(); // Chama a função para editar os dados de vendas
                    break;
                case "3":
                    DeleteReport(); // Adicione a lógica de exclusão conforme necessário
                    break;
                case "4":
                    return;
                default:
                    view.ShowError("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    static void CreateReport(IController controller, IView view)
    {
        string reportName = view.RequestStringInput("Insira o nome do relatório:");
        string userName = view.RequestStringInput("Insira o nome do vendedor:");
        string product = view.RequestStringInput("Insira o produto:");
        DateTime date = view.RequestDateInput("Insira a data:");
        decimal price = view.RequestDecimalInput("Insira o preço:");
        string comments = view.RequestStringInput("Insira comentário:");

        controller.RequestPdfGeneration(reportName, userName, product, date, price, comments);
    }

    static void DeleteReport()
    {
        // Adicione a lógica para eliminar o relatório aqui
        Console.WriteLine("Função de exclusão de relatório não implementada.");
    }
}
