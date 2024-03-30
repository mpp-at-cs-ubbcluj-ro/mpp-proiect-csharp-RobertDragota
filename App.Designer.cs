using System.ComponentModel;

namespace Lab3;

partial class App
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.dataGridView = new System.Windows.Forms.DataGridView();
        this.SearchButton = new System.Windows.Forms.Button();
        this.FromText = new System.Windows.Forms.TextBox();
        this.ToText = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.DestinationText = new System.Windows.Forms.TextBox();
        this.label3 = new System.Windows.Forms.Label();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
        this.SuspendLayout();
        // 
        // dataGridView
        // 
        this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridView.Dock = System.Windows.Forms.DockStyle.Right;
        this.dataGridView.Location = new System.Drawing.Point(243, 0);
        this.dataGridView.Name = "dataGridView";
        this.dataGridView.RowTemplate.Height = 24;
        this.dataGridView.Size = new System.Drawing.Size(662, 644);
        this.dataGridView.TabIndex = 0;
        this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick);
        // 
        // SearchButton
        // 
        this.SearchButton.BackColor = System.Drawing.Color.LimeGreen;
        this.SearchButton.Font = new System.Drawing.Font("Franklin Gothic Demi", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.SearchButton.ForeColor = System.Drawing.SystemColors.ControlText;
        this.SearchButton.Location = new System.Drawing.Point(21, 207);
        this.SearchButton.Name = "SearchButton";
        this.SearchButton.Size = new System.Drawing.Size(109, 35);
        this.SearchButton.TabIndex = 1;
        this.SearchButton.Text = "Search";
        this.SearchButton.UseVisualStyleBackColor = false;
        this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
        // 
        // FromText
        // 
        this.FromText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.FromText.ForeColor = System.Drawing.SystemColors.ControlText;
        this.FromText.Location = new System.Drawing.Point(21, 48);
        this.FromText.Name = "FromText";
        this.FromText.Size = new System.Drawing.Size(133, 22);
        this.FromText.TabIndex = 2;
        this.FromText.TextChanged += new System.EventHandler(this.FromText_TextChanged);
        // 
        // ToText
        // 
        this.ToText.Location = new System.Drawing.Point(21, 109);
        this.ToText.Name = "ToText";
        this.ToText.Size = new System.Drawing.Size(133, 22);
        this.ToText.TabIndex = 3;
        this.ToText.TextChanged += new System.EventHandler(this.ToText_TextChanged);
        // 
        // label1
        // 
        this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(21, 22);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(100, 23);
        this.label1.TabIndex = 4;
        this.label1.Text = "From";
        // 
        // label2
        // 
        this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label2.Location = new System.Drawing.Point(21, 83);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(100, 23);
        this.label2.TabIndex = 5;
        this.label2.Text = "To";
        // 
        // DestinationText
        // 
        this.DestinationText.Location = new System.Drawing.Point(21, 169);
        this.DestinationText.Name = "DestinationText";
        this.DestinationText.Size = new System.Drawing.Size(133, 22);
        this.DestinationText.TabIndex = 6;
        this.DestinationText.TextChanged += new System.EventHandler(this.DestinationText_TextChanged);
        // 
        // label3
        // 
        this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label3.Location = new System.Drawing.Point(21, 143);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(100, 23);
        this.label3.TabIndex = 7;
        this.label3.Text = "Destination";
        // 
        // App
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.DimGray;
        this.ClientSize = new System.Drawing.Size(905, 644);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.DestinationText);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.ToText);
        this.Controls.Add(this.FromText);
        this.Controls.Add(this.SearchButton);
        this.Controls.Add(this.dataGridView);
        this.Name = "App";
        this.Text = "App";
        this.Load += new System.EventHandler(this.App_Load);
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.TextBox DestinationText;
    private System.Windows.Forms.Label label3;

    private System.Windows.Forms.Button SearchButton;
    private System.Windows.Forms.TextBox FromText;
    private System.Windows.Forms.TextBox ToText;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;

    private System.Windows.Forms.DataGridView dataGridView;

    #endregion
}