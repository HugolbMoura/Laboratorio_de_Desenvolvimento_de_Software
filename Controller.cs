using System;

    public class Controller
    {
        private readonly Model _model;
        private readonly View _view;

        public Controller(Model model, View view)
        {
            _model = model;
            _view = view;

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
