using System;

public class View : IView
{
    public void ActivateInterface()
    {
        Console.WriteLine("Sistema de Relatório de Vendas");
    }

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void ShowError(string message)
    {
        Console.WriteLine("Erro: " + message);
    }

    public string RequestStringInput(string prompt)
    {
        Console.WriteLine(prompt);
        return Console.ReadLine();
    }

    public DateTime RequestDateInput(string prompt)
    {
        Console.WriteLine(prompt);
        DateTime date;
        while (!DateTime.TryParse(Console.ReadLine(), out date))
        {
            Console.WriteLine("Data inválida. Por favor, insira no formato correto.");
        }
        return date;
    }

    public decimal RequestDecimalInput(string prompt)
    {
        Console.WriteLine(prompt);
        decimal value;
        while (!decimal.TryParse(Console.ReadLine(), out value))
        {
            Console.WriteLine("Valor inválido. Por favor, insira um número decimal válido.");
        }
        return value;
    }
}
