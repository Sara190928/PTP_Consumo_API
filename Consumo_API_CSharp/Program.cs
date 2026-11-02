// =============================================================
// TRABALHO PRÁTICO: CONSUMO DE WEB API EM C#
// Projeto: Consumo_API_CSharp (Windows Forms)
// API escolhida: Bible API (https://bible-api.com)
// =============================================================

namespace Consumo_API_CSharp;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());
    }
}
