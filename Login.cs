using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3
{
    public partial class Login : Form
    {
        private App app;
        private 
        public Form1()
        {
            InitializeComponent();
            app = new App();
        }


      
       

        private void label1_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }


        private void LoginButton_Click(object sender, EventArgs e)
        {
            app.Show();
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void UserName_TextChanged(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void Password_TextChanged(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }


        private void Login_Load(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.UserName = new System.Windows.Forms.TextBox();
            this.Password = new System.Windows.Forms.TextBox();
            this.LoginButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.RegisterButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UserName
            // 
            this.UserName.Location = new System.Drawing.Point(114, 99);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(175, 22);
            this.UserName.TabIndex = 0;
            this.UserName.TextChanged += new System.EventHandler(this.UserName_TextChanged);
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(114, 159);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(175, 22);
            this.Password.TabIndex = 1;
            this.Password.TextChanged += new System.EventHandler(this.Password_TextChanged);
            // 
            // LoginButton
            // 
            this.LoginButton.BackColor = System.Drawing.Color.LimeGreen;
            this.LoginButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.LoginButton.Font = new System.Drawing.Font("Franklin Gothic Demi", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginButton.ForeColor = System.Drawing.Color.Black;
            this.LoginButton.Location = new System.Drawing.Point(129, 246);
            this.LoginButton.Margin = new System.Windows.Forms.Padding(0);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(151, 39);
            this.LoginButton.TabIndex = 2;
            this.LoginButton.Text = "Login";
            this.LoginButton.UseVisualStyleBackColor = false;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Franklin Gothic Demi", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LimeGreen;
            this.label1.Location = new System.Drawing.Point(114, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "UserName";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Franklin Gothic Demi", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.LimeGreen;
            this.label2.Location = new System.Drawing.Point(114, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 22);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password";
            // 
            // RegisterButton
            // 
            this.RegisterButton.BackColor = System.Drawing.Color.LimeGreen;
            this.RegisterButton.FlatAppearance.BorderColor = System.Drawing.Color.LimeGreen;
            this.RegisterButton.FlatAppearance.BorderSize = 0;
            this.RegisterButton.Font = new System.Drawing.Font("Franklin Gothic Demi", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegisterButton.ForeColor = System.Drawing.Color.Black;
            this.RegisterButton.Location = new System.Drawing.Point(129, 317);
            this.RegisterButton.Name = "RegisterButton";
            this.RegisterButton.Size = new System.Drawing.Size(151, 39);
            this.RegisterButton.TabIndex = 5;
            this.RegisterButton.Text = "Register";
            this.RegisterButton.UseVisualStyleBackColor = false;
            this.RegisterButton.Click += new System.EventHandler(this.RegisterButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(420, 450);
            this.Controls.Add(this.RegisterButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.UserName);
            this.ForeColor = System.Drawing.SystemColors.Highlight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}