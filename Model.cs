// Model.cs
using PdfSharp.Pdf; // Importa a biblioteca PdfSharp para trabalhar com PDFs
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf.IO;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Classe Model implementa a interface IModel
public class Model : IModel
{
    // Eventos para notificar quando uma operação é concluída ou quando é solicitada a geração de um PDF
    public event OperationCompletedEventHandler OperationCompleted;
    public event EventHandler<PdfGenerationEventArgs>? PdfGenerationRequested;

    // Construtor padrão da classe Model
    public Model()
    {
        // Adiciona um manipulador de evento vazio para OperationCompleted
        OperationCompleted += delegate { };
    }

    // Método para buscar dados de vendas
    public void SearchSalesData()
    {
        // Lista todos os arquivos PDF na pasta "reports"
        var reports = Directory.GetFiles("reports", "*.pdf");
        if (reports.Length == 0)
        {
            // Se nenhum relatório for encontrado, dispara um evento de operação concluída com uma mensagem de erro
            OnOperationCompleted(new OperationCompletedEventArgs("Nenhum relatório encontrado.", false));
        }
        else
        {
            // Se relatórios forem encontrados, monta uma mensagem com seus nomes e dispara um evento de operação concluída
            var message = "Relatórios encontrados:\n";
            foreach (var report in reports)
            {
                message += Path.GetFileName(report) + "\n";
            }
            OnOperationCompleted(new OperationCompletedEventArgs(message, false));
        }
    }

    // Método para editar um relatório
    public void EditReport(string reportName, string userName, string product, DateTime date, decimal price, string comments)
    {
        string filePath = "reports/" + reportName + ".pdf";
        if (File.Exists(filePath))
        {
            // Se o relatório existir, gera um novo PDF com os dados fornecidos e dispara um evento de operação concluída
            GeneratePdf(reportName, userName, product, date, price, comments);
            OnOperationCompleted(new OperationCompletedEventArgs("Relatório editado com sucesso.", false));
        }
        else
        {
            // Se o relatório não existir, dispara um evento de operação concluída com uma mensagem de erro
            OnOperationCompleted(new OperationCompletedEventArgs("Relatório não encontrado.", true));
        }
    }

    // Método para excluir um relatório
    public void DeleteReport(string reportName)
    {
        string filePath = "reports/" + reportName + ".pdf";
        if (File.Exists(filePath))
        {
            // Se o relatório existir, exclui o arquivo e dispara um evento de operação concluída
            File.Delete(filePath);
            OnOperationCompleted(new OperationCompletedEventArgs("Relatório eliminado com sucesso.", false));
        }
        else
        {
            // Se o relatório não existir, dispara um evento de operação concluída com uma mensagem de erro
            OnOperationCompleted(new OperationCompletedEventArgs("Relatório não encontrado.", true));
        }
    }


    // Método para gerar um PDF com os dados fornecidos
    public void GeneratePdf(string reportName, string userName, string product, DateTime date, decimal price, string comments)
    {
        try
        {
            // Utiliza a biblioteca PdfSharp para criar e editar PDFs
            using (var doc = new PdfSharp.Pdf.PdfDocument())
            {
                var page = doc.AddPage();
                var graphics = PdfSharp.Drawing.XGraphics.FromPdfPage(page);
                var textFormatter = new PdfSharp.Drawing.Layout.XTextFormatter(graphics);

                // Adiciona uma imagem de logo ao PDF
                graphics.DrawImage(PdfSharp.Drawing.XImage.FromFile("logo.png"), 7, 1, 77, 77);

                // Adiciona texto ao PDF
                var logoFont = new XFont("Arial", 10);
                textFormatter.Alignment = XParagraphAlignment.Left;
                textFormatter.DrawString($"CodeConquers", logoFont, XBrushes.MediumSlateBlue, new XRect(12, 2, page.Width, page.Height));
                
                // Adiciona cabeçalho ao PDF
                var headFont = new XFont("Arial", 14);
                textFormatter.Alignment = XParagraphAlignment.Center;
                textFormatter.DrawString("Relatório de Vendas", headFont, PdfSharp.Drawing.XBrushes.MediumSlateBlue, new PdfSharp.Drawing.XRect(0, 7, page.Width, page.Height));

                // Adiciona um retângulo com o nome do relatório
                var rect = new XRect(0, 50, page.Width, 30);
                graphics.DrawRectangle(XBrushes.LightBlue, rect);
                var font = new XFont("Arial", 22);
                textFormatter.Alignment = XParagraphAlignment.Center;
                textFormatter.DrawString(reportName, font, XBrushes.Green, rect);

                // Adiciona dados do relatório ao PDF
                var dataFont = new XFont("Arial", 12);
                var yPosition = 90;
                textFormatter.Alignment = XParagraphAlignment.Left;
                textFormatter.DrawString($"Nome do Vendedor: {userName}", dataFont, XBrushes.Black, new XRect(20, yPosition, page.Width - 40, page.Height));
                yPosition += 30;
                textFormatter.DrawString($"Produto: {product}", dataFont, XBrushes.Black, new XRect(20, yPosition, page.Width - 40, page.Height));
                yPosition += 30;
                textFormatter.DrawString($"Data: {date.ToShortDateString()}", dataFont, XBrushes.Black, new XRect(20, yPosition, page.Width - 40, page.Height));
                yPosition += 30;
                textFormatter.DrawString($"Preço: {price:C}", dataFont, XBrushes.Black, new XRect(20, yPosition, page.Width - 40, page.Height));
                yPosition += 30;
                textFormatter.DrawString($"Comentário: {comments}", dataFont, XBrushes.Black, new XRect(20, yPosition, page.Width - 40, page.Height));
                yPosition += 80;

                   // Adiciona um retângulo para o rodapé
                var rectFooter = new XRect(0, 250, page.Width, 30);
                graphics.DrawRectangle(XBrushes.LightBlue, rectFooter);
                yPosition += 4;
                var fontFooter = new XFont("Arial", 12);
                textFormatter.Alignment = XParagraphAlignment.Center;
                textFormatter.DrawString("CodeConquers Sales Report", fontFooter, XBrushes.Green, rectFooter);

                // Dispara um evento de operação concluída indicando que o PDF foi gerado com sucesso
                OnOperationCompleted(new OperationCompletedEventArgs("PDF gerado com sucesso.", false));

                // Salva o PDF no diretório 'reports' com o nome fornecido
                string fileName = "reports/" + reportName + ".pdf";
                doc.Save(fileName);

                // O código abaixo abre o PDF após ser gerado, mas foi comentado
                // para evitar a abertura automática do PDF

                // Process.Start(new ProcessStartInfo(fileName) { UseShellExecute = true });
            }
        }
        catch (Exception ex)
        {
            // Se ocorrer um erro durante a geração do PDF, dispara um evento de operação concluída com a mensagem de erro
            OnOperationCompleted(new OperationCompletedEventArgs("Erro ao gerar PDF: " + ex.Message, true));
        }
    }

    // Método para disparar o evento OperationCompleted
    protected virtual void OnOperationCompleted(OperationCompletedEventArgs e)
    {
        OperationCompleted?.Invoke(this, e);
    }

    // Método para disparar o evento PdfGenerationRequested
    protected virtual void OnPdfGenerationRequested(PdfGenerationEventArgs e)
    {
        PdfGenerationRequested?.Invoke(this, e);
    }
}