// Program.cs
partial class Program
{
    static void Main(string[] args)
    {
        // Cria uma instância do modelo, que lida com os dados
        IModel model = new Model();
        
        // Cria uma instância da visualização, que lida com a interface do usuário
        IView view = new View();
        
        // Cria uma instância do controlador, que gerencia a lógica entre o modelo e a visualização
        IController controller = new Controller(model, view);

        // Ativa a interface do usuário (exibe uma mensagem inicial)
        view.ActivateInterface();

        // Inicia um loop infinito para manter o programa em execução
        while (true)
        {
            // Exibe o menu de opções para o usuário
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Criar Relatório");
            Console.WriteLine("2. Editar Relatório");
            Console.WriteLine("3. Consultar Relatórios");
            Console.WriteLine("4. Eliminar Relatório");
            Console.WriteLine("0. Sair");

            // Solicita que o usuário escolha uma opção digitando um número
            string choice = view.RequestStringInput("Escolha uma opção:");

            // Executa uma ação com base na escolha do usuário
            switch (choice)
            {
                case "1":
                    // Chama a função para criar um novo relatório
                    CreateReport(controller, view);
                    break;
                case "2":
                    // Chama a função para editar um relatório existente
                    EditReport(controller, view);
                    break;
                case "3":
                    // Chama a função do controlador para consultar relatórios
                    controller.SearchSalesData();
                    break;
                case "4":
                    // Chama a função para excluir um relatório
                    DeleteReport(controller, view);
                    break;
                case "0":
                    // Encerra o programa
                    return;
                default:
                    // Exibe uma mensagem de erro se a opção for inválida
                    view.ShowError("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    // Função para criar um relatório
    static void CreateReport(IController controller, IView view)
    {
        // Loop para solicitar um nome de relatório que não exista
        string reportName;
        while (true)
        {
            // Solicita o nome do relatório
            reportName = view.RequestStringInput("Insira o nome do relatório:");
            // Verifica se o relatório já existe
            if (!controller.ReportExists(reportName))
            {
                // Sai do loop se o relatório não existir
                break;
            }
            // Exibe uma mensagem de erro se o relatório já existir
            view.ShowError("Já existe um relatório com este nome. Tente outro nome.");
        }

        // Solicita os demais dados necessários para o relatório
        string userName = view.RequestStringInput("Insira o nome do vendedor:");
        string product = view.RequestStringInput("Insira o produto:");
        DateTime date = view.RequestDateInput("Insira a data:");
        decimal price = view.RequestDecimalInput("Insira o preço:");
        string comments = view.RequestStringInput("Insira comentário:");

        // Pede ao controlador para gerar um PDF com os dados fornecidos
        controller.RequestPdfGeneration(reportName, userName, product, date, price, comments);
    }

    // Função para editar um relatório existente
    static void EditReport(IController controller, IView view)
    {
        // Solicita o nome do relatório a ser editado
        string reportName = view.RequestStringInput("Insira o nome do relatório a editar:");
        // Verifica se o relatório existe
        if (!controller.ReportExists(reportName))
        {
            // Exibe uma mensagem de erro se o relatório não for encontrado
            view.ShowError("Relatório não encontrado. Tente outro nome.");
            return;
        }

        // Solicita os novos dados para o relatório
        string userName = view.RequestStringInput("Insira o novo nome do vendedor:");
        string product = view.RequestStringInput("Insira o novo produto:");
        DateTime date = view.RequestDateInput("Insira a nova data:");
        decimal price = view.RequestDecimalInput("Insira o novo preço:");
        string comments = view.RequestStringInput("Insira o novo comentário:");

        // Pede ao controlador para editar o relatório com os novos dados
        controller.EditReport(reportName, userName, product, date, price, comments);
    }

    // Função para excluir um relatório
    static void DeleteReport(IController controller, IView view)
    {
        // Solicita o nome do relatório a ser excluído
        string reportName = view.RequestStringInput("Insira o nome do relatório a eliminar:");
        // Pede ao controlador para excluir o relatório com o nome fornecido
        controller.DeleteReport(reportName);
    }
}
