using System.ComponentModel;

namespace Client;

partial class ReservationController
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
        this.ReserveButton = new System.Windows.Forms.Button();
        this.ClientNameText = new System.Windows.Forms.TextBox();
        this.PhoneText = new System.Windows.Forms.TextBox();
        this.TicketsText = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // ReserveButton
        // 
        this.ReserveButton.Location = new System.Drawing.Point(101, 236);
        this.ReserveButton.Name = "ReserveButton";
        this.ReserveButton.Size = new System.Drawing.Size(75, 43);
        this.ReserveButton.TabIndex = 0;
        this.ReserveButton.Text = "Reserve";
        this.ReserveButton.UseVisualStyleBackColor = true;
        this.ReserveButton.Click += new System.EventHandler(this.ReserveButton_Click);
        // 
        // ClientNameText
        // 
        this.ClientNameText.Location = new System.Drawing.Point(79, 39);
        this.ClientNameText.Name = "ClientNameText";
        this.ClientNameText.Size = new System.Drawing.Size(132, 22);
        this.ClientNameText.TabIndex = 1;
        this.ClientNameText.TextChanged += new System.EventHandler(this.ClientNameText_TextChanged_1);
        // 
        // PhoneText
        // 
        this.PhoneText.Location = new System.Drawing.Point(79, 97);
        this.PhoneText.Name = "PhoneText";
        this.PhoneText.Size = new System.Drawing.Size(132, 22);
        this.PhoneText.TabIndex = 2;
        this.PhoneText.TextChanged += new System.EventHandler(this.PhoneText_TextChanged);
        // 
        // TicketsText
        // 
        this.TicketsText.Location = new System.Drawing.Point(79, 153);
        this.TicketsText.Name = "TicketsText";
        this.TicketsText.Size = new System.Drawing.Size(132, 22);
        this.TicketsText.TabIndex = 3;
        this.TicketsText.TextChanged += new System.EventHandler(this.TicketsText_TextChanged);
        // 
        // label1
        // 
        this.label1.Location = new System.Drawing.Point(79, 13);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(132, 23);
        this.label1.TabIndex = 4;
        this.label1.Text = "ClientName";
        this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        // 
        // label2
        // 
        this.label2.Location = new System.Drawing.Point(79, 71);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(132, 23);
        this.label2.TabIndex = 5;
        this.label2.Text = "Phone";
        this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        // 
        // label3
        // 
        this.label3.Location = new System.Drawing.Point(79, 127);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(132, 23);
        this.label3.TabIndex = 6;
        this.label3.Text = "No Tickets";
        this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        // 
        // ReservationController
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.DimGray;
        this.ClientSize = new System.Drawing.Size(289, 328);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.TicketsText);
        this.Controls.Add(this.PhoneText);
        this.Controls.Add(this.ClientNameText);
        this.Controls.Add(this.ReserveButton);
        this.Name = "ReservationController";
        this.Text = "Reservation";
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.TextBox ClientNameText;
    private System.Windows.Forms.TextBox PhoneText;
    private System.Windows.Forms.TextBox TicketsText;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;

    private System.Windows.Forms.Button ReserveButton;

    #endregion
}