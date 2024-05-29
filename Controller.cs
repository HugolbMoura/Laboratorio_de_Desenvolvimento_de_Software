using System;
using System.Collections.Generic;

public class Controller : IController
{
    private readonly IModel _model;
    private readonly IView _view;
    private readonly PDFGenerator _pdfGenerator;

    public event EventHandler GeneratePdfRequested;

    public Controller(IModel model, IView view, PDFGenerator pdfGenerator)
    {
        _model = model;
        _view = view;
        _pdfGenerator = pdfGenerator;
        _model.OperationCompleted += ModelOperationCompleted;
        GeneratePdfRequested += OnGeneratePdfRequested;
    }

    public void InsertSalesData()
    {
        _model.UpdateSalesData();
    }

    public void InsertSalesComment()
    {
        _model.StoreSalesComment();
    }

    private void ModelOperationCompleted(object sender, OperationCompletedEventArgs e)
    {
        try
        {
            if (e.IsError)
            {
                _view.ShowError(e.Message);
            }
            else
            {
                _view.ShowMessage(e.Message);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao exibir mensagem: " + ex.Message);
        }
    }

    public void RequestPdfGeneration()
    {
        GeneratePdfRequested?.Invoke(this, EventArgs.Empty);
    }

    private void OnGeneratePdfRequested(object sender, EventArgs e)
    {
        // Exemplo de dados. Em um cenário real, esses dados viriam do modelo.
        var salesData = new List<Sale>
        {
            new Sale { SaleId = 1, ProductName = "Produto A", Price = 10.0m, SaleDate = DateTime.Now },
            new Sale { SaleId = 2, ProductName = "Produto B", Price = 20.0m, SaleDate = DateTime.Now }
        };
        var salesComments = new List<SaleComment>
        {
            new SaleComment { SaleId = 1, Comment = "Comentário A" },
            new SaleComment { SaleId = 2, Comment = "Comentário B" }
        };

        var pdfBytes = _pdfGenerator.GeneratePdf(salesData, salesComments);
        // Lógica para salvar ou enviar o PDF gerado
        // Por exemplo: File.WriteAllBytes("relatorio.pdf", pdfBytes);

        _view.ShowMessage("PDF gerado com sucesso.");
    }
}
