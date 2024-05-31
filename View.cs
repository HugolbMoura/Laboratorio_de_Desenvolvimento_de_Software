using System;

// Define a classe View que implementa a interface IView
public class View : IView
{
    // Método que ativa a interface, exibindo uma mensagem inicial no console
    public void ActivateInterface()
    {
        Console.WriteLine("Sistema de Relatório de Vendas");
    }

    // Método que exibe uma mensagem no console
    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    // Método que exibe uma mensagem de erro no console
    public void ShowError(string message)
    {
        Console.WriteLine("Erro: " + message);
    }

    // Método que solicita uma entrada de string do usuário
    public string RequestStringInput(string prompt)
    {
        // Exibe o prompt (mensagem de solicitação) no console
        Console.WriteLine(prompt);
        
        // Lê a entrada do usuário do console
        string? input = Console.ReadLine();
        
        // Verifica se a entrada é nula e lança uma exceção se for
        if (input == null)
        {
            throw new InvalidOperationException("A entrada não pode ser nula.");
        }
        
        // Verifica se a entrada é "0" e encerra o programa se for
        if (input == "0")
        {
            Environment.Exit(0);
        }
        
        // Retorna a entrada do usuário
        return input;
    }

    // Método que solicita uma entrada de data do usuário
    public DateTime RequestDateInput(string prompt)
    {
        // Exibe o prompt (mensagem de solicitação) no console
        Console.WriteLine(prompt);
        
        // Declara uma variável para armazenar a data
        DateTime date;
        
        // Tenta converter a entrada do usuário para um objeto DateTime até que seja bem-sucedido
        while (!DateTime.TryParse(Console.ReadLine(), out date))
        {
            // Exibe uma mensagem de erro se a conversão falhar
            Console.WriteLine("Data inválida. Por favor, insira no formato correto.");
        }
        
        // Retorna a data válida inserida pelo usuário
        return date;
    }

    // Método que solicita uma entrada de número decimal do usuário
    public decimal RequestDecimalInput(string prompt)
    {
        // Exibe o prompt (mensagem de solicitação) no console
        Console.WriteLine(prompt);
        
        // Declara uma variável para armazenar o valor decimal
        decimal value;
        
        // Tenta converter a entrada do usuário para um número decimal até que seja bem-sucedido
        while (!decimal.TryParse(Console.ReadLine(), out value))
        {
            // Exibe uma mensagem de erro se a conversão falhar
            Console.WriteLine("Valor inválido. Por favor, insira um número decimal válido.");
        }
        
        // Retorna o valor decimal válido inserido pelo usuário
        return value;
    }
}
