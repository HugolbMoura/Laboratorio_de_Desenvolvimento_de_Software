using System;

public class View
{
    // Método para ativar a interface
    public void ActivateInterface()
    {
        Console.WriteLine("Interface ativada.");
    }

    // Método para exibir uma mensagem na interface
    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    // Método para exibir uma mensagem de erro na interface
    public void ShowError(string errorMessage)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(errorMessage);
        Console.ResetColor();
    }
}
