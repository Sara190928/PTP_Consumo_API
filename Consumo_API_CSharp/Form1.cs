// =============================================================
// Lógica do formulário: consumo da Bible API via HttpClient
// =============================================================

using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Consumo_API_CSharp;

public partial class Form1 : Form
{
    // Endpoint escolhido: Bible API - referência por omissão (João 3:16)
    const string UrlBase = "https://bible-api.com/";

    // HttpClient reutilizado em toda a aplicação (boas práticas)
    static readonly HttpClient client = new HttpClient
    {
        Timeout = TimeSpan.FromSeconds(30)
    };

    public Form1()
    {
        InitializeComponent();
    }

    // ---- Botão "Pesquisar": requisição GET + tratamento de erros ----
    private async void btnPesquisar_Click(object sender, EventArgs e)
    {
        string referencia = txtReferencia.Text.Trim();

        if (string.IsNullOrEmpty(referencia))
        {
            MessageBox.Show(
                "Escreva uma referência bíblica (ex: john 3:16, genesis 1:1-5, psalm 23).",
                "Aviso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        btnPesquisar.Enabled = false;

        try
        {
            // ---- 1. Requisição HTTP do tipo GET ----
            string url = UrlBase + Uri.EscapeDataString(referencia);
            lblEstado.Text = $"A consultar: {url}";

            HttpResponseMessage resposta = await client.GetAsync(url);

            // Lança HttpRequestException se a resposta não for de sucesso (4xx/5xx)
            resposta.EnsureSuccessStatusCode();

            string json = await resposta.Content.ReadAsStringAsync();

            // ---- 2. Converter JSON para objetos estruturados em C# ----
            var opcoes = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var biblia = JsonSerializer.Deserialize<BibliaResposta>(json, opcoes);

            if (biblia?.Verses == null || biblia.Verses.Count == 0)
            {
                txtResultado.Text = "A API não devolveu nenhum versículo. Verifique a referência.";
                lblEstado.Text = "Sem resultados.";
                return;
            }

            // ---- 3. Apresentar os dados de forma clara ----
            var sb = new StringBuilder();
            sb.AppendLine($"Referência : {biblia.Reference}");
            sb.AppendLine($"Tradução   : {biblia.TranslationName} ({biblia.TranslationId})");
            sb.AppendLine("-------------------------------------------");

            foreach (var v in biblia.Verses)
            {
                sb.AppendLine();
                sb.AppendLine($">> {v.BookName} {v.Chapter}:{v.Verse}");
                sb.AppendLine(LimparTexto(v.Text));
            }

            txtResultado.Text = sb.ToString();
            lblEstado.Text = "Dados recebidos e processados com sucesso!";
        }
        catch (HttpRequestException ex)
        {
            // Falhas de rede, DNS, 404, 500...
            lblEstado.Text = "Erro de conexão.";
            MessageBox.Show(
                $"Não foi possível obter resposta da API.\n\n{ex.Message}",
                "Erro de conexão",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        catch (TaskCanceledException)
        {
            // Timeout da ligação
            lblEstado.Text = "Tempo esgotado.";
            MessageBox.Show(
                "A API demorou demasiado a responder.",
                "Timeout",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        catch (JsonException ex)
        {
            // JSON malformado ou estrutura inesperada
            lblEstado.Text = "Erro de JSON.";
            MessageBox.Show(
                $"A resposta da API não pôde ser interpretada.\n\n{ex.Message}",
                "Erro de JSON",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            // Qualquer outra exceção inesperada
            lblEstado.Text = "Erro inesperado.";
            MessageBox.Show(
                $"{ex.GetType().Name}: {ex.Message}",
                "Erro",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        finally
        {
            btnPesquisar.Enabled = true;
        }
    }

    // Remove quebras de linha redundantes devolvidas pela API
    static string LimparTexto(string? texto) =>
        string.Join("\n", (texto ?? string.Empty)
            .Split('\n')
            .Select(l => l.Trim())
            .Where(l => l.Length > 0));
}

// ---- Classes que espelham a estrutura JSON da API ----

class BibliaResposta
{
    [JsonPropertyName("reference")]
    public string? Reference { get; set; }

    [JsonPropertyName("verses")]
    public List<Versiculo>? Verses { get; set; }

    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("translation_id")]
    public string? TranslationId { get; set; }

    [JsonPropertyName("translation_name")]
    public string? TranslationName { get; set; }
}

class Versiculo
{
    [JsonPropertyName("book_id")]
    public string? BookId { get; set; }

    [JsonPropertyName("book_name")]
    public string? BookName { get; set; }

    [JsonPropertyName("chapter")]
    public int Chapter { get; set; }

    [JsonPropertyName("verse")]
    public int Verse { get; set; }

    [JsonPropertyName("text")]
    public string? Text { get; set; }
}
