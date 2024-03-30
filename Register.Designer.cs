using System.ComponentModel;

namespace Lab3;

partial class Register
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
        this.RegisterButton = new System.Windows.Forms.Button();
        this.UserNameText = new System.Windows.Forms.TextBox();
        this.PasswordText = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // RegisterButton
        // 
        this.RegisterButton.BackColor = System.Drawing.Color.LimeGreen;
        this.RegisterButton.Font = new System.Drawing.Font("Franklin Gothic Demi", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.RegisterButton.Location = new System.Drawing.Point(181, 280);
        this.RegisterButton.Name = "RegisterButton";
        this.RegisterButton.Size = new System.Drawing.Size(83, 40);
        this.RegisterButton.TabIndex = 0;
        this.RegisterButton.Text = "Register";
        this.RegisterButton.UseVisualStyleBackColor = false;
        this.RegisterButton.Click += new System.EventHandler(this.RegisterButton_Click);
        // 
        // UserNameText
        // 
        this.UserNameText.Location = new System.Drawing.Point(126, 98);
        this.UserNameText.Name = "UserNameText";
        this.UserNameText.Size = new System.Drawing.Size(195, 22);
        this.UserNameText.TabIndex = 1;
        this.UserNameText.TextChanged += new System.EventHandler(this.UserNameText_TextChanged);
        // 
        // PasswordText
        // 
        this.PasswordText.Location = new System.Drawing.Point(126, 187);
        this.PasswordText.Name = "PasswordText";
        this.PasswordText.Size = new System.Drawing.Size(195, 22);
        this.PasswordText.TabIndex = 2;
        this.PasswordText.TextChanged += new System.EventHandler(this.PasswordText_TextChanged);
        // 
        // label1
        // 
        this.label1.Font = new System.Drawing.Font("Franklin Gothic Demi", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(126, 60);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(195, 23);
        this.label1.TabIndex = 3;
        this.label1.Text = "New UserName";
        this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        this.label1.Click += new System.EventHandler(this.label1_Click);
        // 
        // label2
        // 
        this.label2.Font = new System.Drawing.Font("Franklin Gothic Demi", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label2.Location = new System.Drawing.Point(126, 150);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(195, 23);
        this.label2.TabIndex = 4;
        this.label2.Text = "New Password";
        this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        this.label2.Click += new System.EventHandler(this.label2_Click);
        // 
        // Register
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.DimGray;
        this.ClientSize = new System.Drawing.Size(454, 398);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.PasswordText);
        this.Controls.Add(this.UserNameText);
        this.Controls.Add(this.RegisterButton);
        this.Name = "Register";
        this.Text = "Register";
        this.Load += new System.EventHandler(this.Register_Load);
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.Button RegisterButton;
    private System.Windows.Forms.TextBox UserNameText;
    private System.Windows.Forms.TextBox PasswordText;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;

    #endregion
}