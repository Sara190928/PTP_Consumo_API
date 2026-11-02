namespace Consumo_API_CSharp;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    // ---- Controles do formulário ----
    private Label lblTitulo;
    private Label lblReferencia;
    private TextBox txtReferencia;
    private Button btnPesquisar;
    private Label lblResultado;
    private TextBox txtResultado;
    private Label lblEstado;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        lblTitulo = new Label();
        lblReferencia = new Label();
        txtReferencia = new TextBox();
        btnPesquisar = new Button();
        lblResultado = new Label();
        txtResultado = new TextBox();
        lblEstado = new Label();
        SuspendLayout();
        //
        // lblTitulo
        //
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblTitulo.Location = new Point(12, 9);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(450, 25);
        lblTitulo.Text = "Consumo de Web API em C# — Bible API (bible-api.com)";
        //
        // lblReferencia
        //
        lblReferencia.AutoSize = true;
        lblReferencia.Location = new Point(14, 45);
        lblReferencia.Name = "lblReferencia";
        lblReferencia.Size = new Size(130, 20);
        lblReferencia.Text = "Referência bíblica:";
        //
        // txtReferencia
        //
        txtReferencia.Location = new Point(150, 42);
        txtReferencia.Name = "txtReferencia";
        txtReferencia.Size = new Size(300, 27);
        txtReferencia.Text = "john 3:16";
        //
        // btnPesquisar
        //
        btnPesquisar.Location = new Point(458, 41);
        btnPesquisar.Name = "btnPesquisar";
        btnPesquisar.Size = new Size(120, 28);
        btnPesquisar.Text = "Pesquisar";
        btnPesquisar.UseVisualStyleBackColor = true;
        btnPesquisar.Click += btnPesquisar_Click;
        //
        // lblResultado
        //
        lblResultado.AutoSize = true;
        lblResultado.Location = new Point(14, 78);
        lblResultado.Name = "lblResultado";
        lblResultado.Size = new Size(75, 20);
        lblResultado.Text = "Resultado:";
        //
        // txtResultado
        //
        txtResultado.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        txtResultado.Font = new Font("Consolas", 10F);
        txtResultado.Location = new Point(12, 101);
        txtResultado.Multiline = true;
        txtResultado.Name = "txtResultado";
        txtResultado.ReadOnly = true;
        txtResultado.ScrollBars = ScrollBars.Vertical;
        txtResultado.Size = new Size(696, 330);
        txtResultado.Text = "Escreva uma referência e clique em Pesquisar.\r\nExemplos: john 3:16 | genesis 1:1-5 | psalm 23";
        //
        // lblEstado
        //
        lblEstado.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lblEstado.Location = new Point(12, 440);
        lblEstado.Name = "lblEstado";
        lblEstado.Size = new Size(696, 20);
        lblEstado.Text = "Pronto.";
        //
        // Form1
        //
        AcceptButton = btnPesquisar;
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(720, 470);
        MinimumSize = new Size(560, 380);
        Name = "Form1";
        Text = "Consumo_API_CSharp — Bible API";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
}
