using System;

partial class Program
{
    static void Main(string[] args)
    {
        Model model = new Model();
        View view = new View();
        Controller controller = new Controller(model, view);
        User user = new User();

        user.InicializarSistema(controller);
        controller.InserirDadosVenda();
        controller.InserirComentarioVenda();

        // Modifique aqui para adicionar o tratamento de exceção ou alterar a entrada
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
 