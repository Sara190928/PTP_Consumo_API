# PTP_Consumo_API

Trabalho prático: **Consumo de Web API em C#** — projeto `Consumo_API_CSharp`.

## Descrição

Aplicação de consola em C# que consome a **Bible API** ([bible-api.com](https://bible-api.com)) usando `HttpClient`, converte a resposta JSON para objetos estruturados com `System.Text.Json` e apresenta os dados ao utilizador, com tratamento de exceções para falhas de rede.

## Funcionalidades

- Requisição HTTP **GET** com `HttpClient`
- Deserialização JSON para objetos C# (`System.Text.Json`)
- Blocos `try/catch` para erros de conexão, timeout, JSON inválido e erros inesperados
- O utilizador pode escolher a referência bíblica (por omissão: `john 3:16`)

## Como executar

Requer [.NET SDK 8.0+](https://dotnet.microsoft.com/download).

```bash
cd Consumo_API_CSharp
dotnet run
```

Ou executar com uma referência específica:

```bash
dotnet run -- "genesis 1:1"
```

## Estrutura do projeto

```
PTP_Consumo_API/
├── README.md
└── Consumo_API_CSharp/
    ├── Consumo_API_CSharp.csproj
    └── Program.cs
```

## API utilizada

`GET https://bible-api.com/john%203:16`

Exemplo de resposta JSON:

```json
{
  "reference": "John 3:16",
  "verses": [
    { "book_id": "JHN", "book_name": "John", "chapter": 3, "verse": 16, "text": "..." }
  ],
  "translation_id": "web",
  "translation_name": "World English Bible"
}
```
