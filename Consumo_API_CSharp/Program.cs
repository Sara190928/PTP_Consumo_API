// =============================================================
// TRABALHO PRÁTICO: CONSUMO DE WEB API EM C#
// Projeto: Consumo_API_CSharp
// API escolhida: Bible API (https://bible-api.com)
// Método: GET via HttpClient + System.Text.Json
// =============================================================

using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Consumo_API_CSharp;

class Program
{
    // Endpoint escolhido: Bible API - versículo por omissão (João 3:16)
    const string UrlBase = "https://bible-api.com/";

    // HttpClient reutilizado em toda a aplicação (boas práticas)
    static readonly HttpClient client = new HttpClient
    {
        Timeout = TimeSpan.FromSeconds(30)
    };

    static async Task Main(string[] args)
    {
        Console.WriteLine("==================================================");
        Console.WriteLine("   CONSUMO DE WEB API EM C# - Bible API");
        Console.WriteLine("   Projeto: Consumo_API_CSharp");
        Console.WriteLine("==================================================");

        // Referência bíblica: por omissão João 3:16, ou via argumentos
        string referencia = "john 3:16";

        if (args.Length > 0)
        {
            referencia = string.Join(" ", args);
        }
        else
        {
            Console.Write("\nPressione Enter para usar João 3:16, ou escreva outra");
            Console.Write("\nreferência (ex: genesis 1:1 / john 3:16-18): ");
            string? entrada = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(entrada))
                referencia = entrada.Trim();
        }

        try
        {
            // ---- 1. Requisição HTTP do tipo GET ----
            string url = UrlBase + Uri.EscapeDataString(referencia);
            Console.WriteLine($"\nA consultar: {url}");

            HttpResponseMessage resposta = await client.GetAsync(url);

            // Lança HttpRequestException se a resposta não for de sucesso (4xx/5xx)
            resposta.EnsureSuccessStatusCode();

            string json = await resposta.Content.ReadAsStringAsync();
            Console.WriteLine($"Resposta recebida ({json.Length} caracteres de JSON)");

            // ---- 2. Converter JSON para objetos estruturados em C# ----
            var opcoes = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var biblia = JsonSerializer.Deserialize<BibliaResposta>(json, opcoes);

            if (biblia?.Verses == null || biblia.Verses.Count == 0)
            {
                Console.WriteLine("\nA API não devolveu nenhum versículo. Verifique a referência.");
                return;
            }

            // ---- 3. Apresentar os dados de forma clara ----
            Console.WriteLine("\n---------------- RESULTADO ----------------");
            Console.WriteLine($"Referência : {biblia.Reference}");
            Console.WriteLine($"Tradução   : {biblia.TranslationName} ({biblia.TranslationId})");
            Console.WriteLine("-------------------------------------------");

            foreach (var v in biblia.Verses)
            {
                Console.WriteLine($"\n>> {v.BookName} {v.Chapter}:{v.Verse}");
                Console.WriteLine(Teformatar(v.Text));
            }

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Dados recebidos e processados com sucesso!");
        }
        catch (HttpRequestException ex)
        {
            // Falhas de rede, DNS, 404, 500...
            Console.WriteLine("\n[ERRO DE CONEXÃO] Não foi possível obter resposta da API.");
            Console.WriteLine($"Detalhes: {ex.Message}");
        }
        catch (TaskCanceledException)
        {
            // Timeout da ligação
            Console.WriteLine("\n[ERRO DE TIMEOUT] A API demorou demasiado a responder.");
        }
        catch (JsonException ex)
        {
            // JSON malformado ou estrutura inesperada
            Console.WriteLine("\n[ERRO DE JSON] A resposta da API não pôde ser interpretada.");
            Console.WriteLine($"Detalhes: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Qualquer outra exceção inesperada
            Console.WriteLine($"\n[ERRO INESPERADO] {ex.GetType().Name}: {ex.Message}");
        }
    }

    // Remove quebras de linha redundantes devolvidas pela API
    static string Teformatar(string? texto) =>
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
