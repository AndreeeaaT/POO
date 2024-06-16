using System;
using System.Windows.Forms;

namespace Proiect1
{
    public class RegisterForm : Form
    {
        private static int clientCounter = 1;
        public Client RegisteredClient { get; private set; }

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            RegisteredClient = new Client
            {
                Id = clientCounter++,
                Name = nameTextBox.Text,
                Email = emailTextBox.Text,
                Address = addressTextBox.Text
            };
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void InitializeComponent()
        {
            this.nameTextBox = new TextBox();
            this.emailTextBox = new TextBox();
            this.addressTextBox = new TextBox();
            this.registerButton = new Button();
            this.SuspendLayout();

            this.nameTextBox.Location = new System.Drawing.Point(12, 12);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(260, 20);
            this.nameTextBox.TabIndex = 0;
            this.nameTextBox.PlaceholderText = "Nume";

            this.emailTextBox.Location = new System.Drawing.Point(12, 38);
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(260, 20);
            this.emailTextBox.TabIndex = 1;
            this.emailTextBox.PlaceholderText = "Email";

            this.addressTextBox.Location = new System.Drawing.Point(12, 64);
            this.addressTextBox.Name = "addressTextBox";
            this.addressTextBox.Size = new System.Drawing.Size(260, 20);
            this.addressTextBox.TabIndex = 2;
            this.addressTextBox.PlaceholderText = "Adresă";

            this.registerButton.Location = new System.Drawing.Point(12, 90);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(260, 23);
            this.registerButton.TabIndex = 3;
            this.registerButton.Text = "Înregistrează";
            this.registerButton.UseVisualStyleBackColor = true;
            this.registerButton.Click += new System.EventHandler(this.RegisterButton_Click);

            this.ClientSize = new System.Drawing.Size(284, 121);
            this.Controls.Add(this.registerButton);
            this.Controls.Add(this.addressTextBox);
            this.Controls.Add(this.emailTextBox);
            this.Controls.Add(this.nameTextBox);
            this.Name = "RegisterForm";
            this.Text = "Înregistrare Client";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private TextBox nameTextBox;
        private TextBox emailTextBox;
        private TextBox addressTextBox;
        private Button registerButton;
    }
}
