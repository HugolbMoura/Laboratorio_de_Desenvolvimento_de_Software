using System;

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
        controller.RequestPdfGeneration();

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
