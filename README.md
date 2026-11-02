# PTP_Consumo_API

Trabalho prático: **Consumo de Web API em C#** — projeto `Consumo_API_CSharp` (Windows Forms).

## Descrição

Aplicação Windows Forms em C# que consome a **Bible API** ([bible-api.com](https://bible-api.com)) usando `HttpClient`, converte a resposta JSON para objetos estruturados com `System.Text.Json` e apresenta os dados ao utilizador, com tratamento de exceções para falhas de rede.

## Funcionalidades

- Requisição HTTP **GET** com `HttpClient`
- Deserialização JSON para objetos C# (`System.Text.Json`)
- Blocos `try/catch` para erros de conexão, timeout, JSON inválido e erros inesperados
- Interface gráfica com campo de pesquisa, botão **Pesquisar** e caixa de resultado

## Como executar

Requer [.NET SDK 8.0+](https://dotnet.microsoft.com/download) com suporte a Windows Forms (Windows).

```bash
cd Consumo_API_CSharp
dotnet run
```

Ou executar diretamente o executável gerado em `bin\Debug\net10.0-windows\Consumo_API_CSharp.exe`.

## Estrutura do projeto

```
PTP_Consumo_API/
├── README.md
└── Consumo_API_CSharp/
    ├── Consumo_API_CSharp.csproj
    ├── Program.cs          // ponto de entrada
    ├── Form1.cs            // lógica (HttpClient + JSON + try/catch)
    └── Form1.Designer.cs   // layout do formulário
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

Referências aceites (nomes bíblicos em inglês): versículo (`john 3:16`), intervalo (`genesis 1:1-5`) ou capítulo inteiro (`psalm 23`).
