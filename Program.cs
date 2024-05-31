// Program.cs
partial class Program
{
    static void Main(string[] args)
    {
        // Inicialização do modelo, visualização e controlador
        IModel model = new Model();
        IView view = new View();
        IController controller = new Controller(model, view);

        // Ativação da interface de usuário
        view.ActivateInterface();

        // Loop principal do programa
        while (true)
        {
            // Apresentação do menu
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Criar Relatório");
            Console.WriteLine("2. Editar Relatório");
            Console.WriteLine("3. Consultar Relatórios");
            Console.WriteLine("4. Eliminar Relatório");
            Console.WriteLine("0. Sair");

            // Solicitação de entrada do usuário para escolha da opção
            string choice = view.RequestStringInput("Escolha uma opção:");

            // Execução da ação com base na escolha do usuário
            switch (choice)
            {
                case "1":
                    CreateReport(controller, view); // Criação de um relatório
                    break;
                case "2":
                    EditReport(controller, view); // Edição de um relatório
                    break;
                case "3":
                    controller.SearchSalesData(); // Consulta de relatórios
                    break;
                case "4":
                    DeleteReport(controller, view); // Exclusão de um relatório
                    break;
                case "0":
                    return; // Encerra o programa
                default:
                    view.ShowError("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    // Função para criar um relatório
    static void CreateReport(IController controller, IView view)
    {
        // Solicitação de nome de relatório e verificação de existência
        string reportName;
        while (true)
        {
            reportName = view.RequestStringInput("Insira o nome do relatório:");
            if (!controller.ReportExists(reportName))
            {
                break;
            }
            view.ShowError("Já existe um relatório com este nome. Tente outro nome.");
        }

        // Solicitação dos demais dados do relatório
        string userName = view.RequestStringInput("Insira o nome do vendedor:");
        string product = view.RequestStringInput("Insira o produto:");
        DateTime date = view.RequestDateInput("Insira a data:");
        decimal price = view.RequestDecimalInput("Insira o preço:");
        string comments = view.RequestStringInput("Insira comentário:");

        // Solicita ao controlador que gere um PDF com os dados fornecidos
        controller.RequestPdfGeneration(reportName, userName, product, date, price, comments);
    }

    // Função para editar um relatório
    static void EditReport(IController controller, IView view)
    {
        // Solicitação de nome de relatório e verificação de existência
        string reportName = view.RequestStringInput("Insira o nome do relatório a editar:");
        if (!controller.ReportExists(reportName))
        {
            view.ShowError("Relatório não encontrado. Tente outro nome.");
            return;
        }

        // Solicitação dos novos dados do relatório
        string userName = view.RequestStringInput("Insira o novo nome do vendedor:");
        string product = view.RequestStringInput("Insira o novo produto:");
        DateTime date = view.RequestDateInput("Insira a nova data:");
        decimal price = view.RequestDecimalInput("Insira o novo preço:");
        string comments = view.RequestStringInput("Insira o novo comentário:");

        // Solicita ao controlador que edite o relatório
        controller.EditReport(reportName, userName, product, date, price, comments);
    }

    // Função para excluir um relatório
    static void DeleteReport(IController controller, IView view)
    {
        // Solicitação do nome do relatório a ser excluído
        string reportName = view.RequestStringInput("Insira o nome do relatório a eliminar:");
        controller.DeleteReport(reportName);
    }
}
