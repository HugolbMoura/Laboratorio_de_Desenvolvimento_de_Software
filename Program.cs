using System;

partial class Program
{
    static void Main(string[] args)
    {
        // Criar instâncias de Model, View e Controller
        Model model = new Model();
        View view = new View();
        Controller controller = new Controller(model, view);

        // Inicializar a interface
        view.ActivateInterface();

        // Realizar operações
        controller.InsertSalesData();
        controller.InsertSalesComment();

        // Solicitar geração de PDF
        controller.RequestPdfGeneration();

        // Adicionar tratamento de exceção para entrada
        try
        {
            Console.WriteLine("Pressione qualquer tecla para fechar o programa...");
            Console.ReadKey();  // Isso pode causar InvalidOperationException em certos contextos
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Não foi possível ler a entrada do teclado: " + ex.Message);
            Console.WriteLine("Pressione Enter para fechar o programa...");
            Console.ReadLine();  // Uma alternativa que geralmente funciona em mais contextos
        }
    }
}
