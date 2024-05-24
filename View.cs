using System;

public class View : IView
{
    public void ActivateInterface()
    {
        Console.WriteLine("Interface ativada.");
    }

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void ShowError(string message)
    {
        Console.WriteLine("Erro: " + message);
    }
}
