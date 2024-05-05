using System;

public class ViewEventArgs : EventArgs
{
    public string Message { get; }

    public ViewEventArgs(string message)
    {
        Message = message;
    }
}
