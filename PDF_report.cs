

// Classe de evento para comunicação entre Controller e View
public class ViewEventArgs : EventArgs
{
    public string Message { get; }

    public ViewEventArgs(string message)
    {
        Message = message;
    }
}

// Classe que representa o usuário
public class User
{
    public void InicializarSistema(Controller controller)
    {
        controller.InicializarSistema();
    }

    // Outras interações do usuário podem ser implementadas aqui
}

// Classe do Model
public class Model
{
    public event EventHandler<string> OperationCompleted;

    public void AtualizarDadosVenda()
    {
        // Lógica para atualizar os dados de venda
        OnOperationCompleted("Dados de venda atualizados com sucesso.");
    }

    public void ArmazenarComentario()
    {
        // Lógica para armazenar comentário sobre venda
        OnOperationCompleted("Comentário sobre venda armazenado com sucesso.");
    }

    protected virtual void OnOperationCompleted(string message)
    {
        OperationCompleted?.Invoke(this, message);
    }
}

// Classe do View
public class View
{
    public event EventHandler<ViewEventArgs> UpdateInterface;

    public void AtivarInterface()
    {
        // Lógica para ativar a interface
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

// Classe do Controller
public class Controller
{
    private readonly Model _model;
    private readonly View _view;

    public Controller(Model model, View view)
    {
        _model = model;
        _view = view;

        // Subscreve-se aos eventos
        _model.OperationCompleted += ModelOperationCompleted;
        _view.UpdateInterface += ViewUpdateInterface;
    }

    public void InicializarSistema()
    {
        _view.AtivarInterface();
    }

    public void InserirDadosVenda()
    {
        _model.AtualizarDadosVenda();
    }

    public void InserirComentarioVenda()
    {
        _model.ArmazenarComentario();
    }

    private void ModelOperationCompleted(object sender, string message)
    {
        _view.ExibirMensagem(message);
    }

    private void ViewUpdateInterface(object sender, ViewEventArgs e)
    {
        _view.ExibirMensagem(e.Message);
    }
}

partial class Program
{
    static void Main(string[] args)
    {
        // Criar instâncias dos componentes
        Model model = new Model();
        View view = new View();
        Controller controller = new Controller(model, view);
        User user = new User();

        // Inicializar o sistema
        user.InicializarSistema(controller);

        // Interagir com o sistema
        controller.InserirDadosVenda();
        controller.InserirComentarioVenda();

        // Fechar o programa
        Console.WriteLine("Pressione qualquer tecla para fechar o programa...");
        Console.ReadKey();
    }
}
