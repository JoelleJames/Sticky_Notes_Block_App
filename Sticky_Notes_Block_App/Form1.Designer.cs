using System.Drawing.Text;

namespace Sticky_Notes_Block_App
{
    partial class Sicky_Notes_Block
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            Close_Button = new Button();
            Minimize_Button = new Button();
            History_Button = new Button();
            Settings_Button = new Button();
            Auto_Resize_Button = new Button();
            Insert_Text_Button = new Button();
            SuspendLayout();
            // 
            // Close_Button
            // 
            Close_Button.FlatAppearance.BorderSize = 0;
            Close_Button.FlatStyle = FlatStyle.Flat;
            Close_Button.Location = new Point(749, 12);
            Close_Button.Name = "Close_Button";
            Close_Button.Size = new Size(42, 47);
            Close_Button.TabIndex = 0;
            Close_Button.Text = "X";
            Close_Button.UseVisualStyleBackColor = true;
            Close_Button.Click += Close_Button_Click;
            // 
            // Minimize_Button
            // 
            Minimize_Button.FlatStyle = FlatStyle.Flat;
            Minimize_Button.Location = new Point(691, 12);
            Minimize_Button.Name = "Minimize_Button";
            Minimize_Button.Size = new Size(45, 44);
            Minimize_Button.TabIndex = 1;
            Minimize_Button.Text = "-";
            Minimize_Button.UseVisualStyleBackColor = true;
            // 
            // History_Button
            // 
            History_Button.FlatStyle = FlatStyle.Flat;
            History_Button.Location = new Point(577, 15);
            History_Button.Name = "History_Button";
            History_Button.Size = new Size(108, 43);
            History_Button.TabIndex = 2;
            History_Button.Text = "History?";
            History_Button.UseVisualStyleBackColor = true;
            // 
            // Settings_Button
            // 
            Settings_Button.FlatStyle = FlatStyle.Flat;
            Settings_Button.Location = new Point(12, 13);
            Settings_Button.Name = "Settings_Button";
            Settings_Button.Size = new Size(150, 46);
            Settings_Button.TabIndex = 3;
            Settings_Button.Text = "Settings";
            Settings_Button.UseVisualStyleBackColor = true;
            // 
            // Auto_Resize_Button
            // 
            Auto_Resize_Button.FlatStyle = FlatStyle.Flat;
            Auto_Resize_Button.Location = new Point(726, 399);
            Auto_Resize_Button.Name = "Auto_Resize_Button";
            Auto_Resize_Button.Size = new Size(65, 44);
            Auto_Resize_Button.TabIndex = 4;
            Auto_Resize_Button.Text = "fit";
            Auto_Resize_Button.UseVisualStyleBackColor = true;
            // 
            // Insert_Text_Button
            // 
            Insert_Text_Button.FlatStyle = FlatStyle.Flat;
            Insert_Text_Button.Location = new Point(12, 392);
            Insert_Text_Button.Name = "Insert_Text_Button";
            Insert_Text_Button.Size = new Size(150, 46);
            Insert_Text_Button.TabIndex = 5;
            Insert_Text_Button.Text = "Insert Text";
            Insert_Text_Button.UseVisualStyleBackColor = true;
            // 
            // Sicky_Notes_Block
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 192, 128);
            ClientSize = new Size(800, 450);
            ControlBox = false;
            Controls.Add(Insert_Text_Button);
            Controls.Add(Auto_Resize_Button);
            Controls.Add(Settings_Button);
            Controls.Add(History_Button);
            Controls.Add(Minimize_Button);
            Controls.Add(Close_Button);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Name = "Sicky_Notes_Block";
            Text = "Block of Sticky Notes";
            ResumeLayout(false);
        }

        #endregion

        private Button Close_Button;
        private Button Minimize_Button;
        private Button History_Button;
        private Button Settings_Button;
        private Button Auto_Resize_Button;
        private Button Insert_Text_Button;
    }
}
