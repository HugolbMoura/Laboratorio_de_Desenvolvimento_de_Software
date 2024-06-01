using System;

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
            Console.WriteLine("3. Consultar Relatórios");
            Console.WriteLine("4. Eliminar Relatório");
            Console.WriteLine("0. Sair");

            string choice = view.RequestStringInput("Escolha uma opção:");

            switch (choice)
            {
                case "1":
                    CreateReport(controller, view);
                    break;
                case "2":
                    EditReport(controller, view);
                    break;
                case "3":
                    ConsultReports(controller, view);
                    break;
                case "4":
                    DeleteReport(controller, view);
                    break;
                case "0":
                    return;
                default:
                    view.ShowError("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    static void CreateReport(IController controller, IView view)
    {
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

        string userName = view.RequestStringInput("Insira o nome do vendedor:");
        string product = view.RequestStringInput("Insira o produto:");
        decimal price = view.RequestDecimalInput("Insira o preço:");
        string comments = view.RequestStringInput("Insira comentário:");

        controller.RequestPdfGeneration(reportName, userName, product, DateTime.Now, price, comments);
    }

    static void EditReport(IController controller, IView view)
    {
        string reportName = view.RequestStringInput("Insira o nome do relatório a editar:");
        if (!controller.ReportExists(reportName))
        {
            view.ShowError("Relatório não encontrado. Tente outro nome.");
            return;
        }

        string userName = view.RequestStringInput("Insira o novo nome do vendedor:");
        string product = view.RequestStringInput("Insira o novo produto:");
        DateTime date = view.RequestDateInput("Insira a nova data:");
        decimal price = view.RequestDecimalInput("Insira o novo preço:");
        string comments = view.RequestStringInput("Insira o novo comentário:");

        controller.EditReport(reportName, userName, product, date, price, comments);
    }

    static void ConsultReports(IController controller, IView view)
    {
        controller.SearchSalesData();
        string option = view.RequestStringInput("Deseja buscar por nome (1) ou por intervalo de datas (2)?");

        if (option == "1")
        {
            string reportName = view.RequestStringInput("Insira o nome do relatório:");
            controller.SearchSalesDataByName(reportName);
        }
        else if (option == "2")
        {
            DateTime startDate = view.RequestDateInput("Insira a data de início:");
            DateTime endDate = view.RequestDateInput("Insira a data de fim:");
            controller.SearchSalesDataByDateRange(startDate, endDate);
        }
        else
        {
            view.ShowError("Opção inválida.");
        }
    }

    static void DeleteReport(IController controller, IView view)
    {
        string reportName = view.RequestStringInput("Insira o nome do relatório a eliminar:");
        controller.DeleteReport(reportName);
    }
}
