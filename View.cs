using System;

    public class View
    {
        public event EventHandler<ViewEventArgs> UpdateInterface;

        public void AtivarInterface()
        {
            OnUpdateInterface("Interface ativada.");
        }

        public void ExibirMensagem(string message)
        {
            Console.WriteLine(message);
        }

        protected virtual void OnUpdateInterface(string message)
        {
            UpdateInterface?.Invoke(this, new ViewEventArgs(message));
        }
    }

