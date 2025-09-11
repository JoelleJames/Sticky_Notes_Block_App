using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Windows.Forms;
using Font = System.Drawing.Font;

namespace Sticky_Notes_Block_App
{
    public partial class Sicky_Notes_Block : Form
    {
        private System.Windows.Forms.Timer HideTimer;
        private bool mouseInsideForm = false;

        public Sicky_Notes_Block()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            Hide_All_Components(); // Hide comonents when opened
            
            //timer for 1 second delay to hide components when mouse not over form
            HideTimer = new System.Windows.Forms.Timer();
            HideTimer.Interval = 1000; // 1 second
            HideTimer.Tick += HideTimer_Tick;

            //refresh to link controls to Form_MouseEnter and Form_MouseLeave events
            Refresh_Form();
        }
        private void Refresh_Form()
        {
            foreach (Control ctrl in Controls)
            {
                ctrl.MouseLeave += Form_MouseLeave;
            }
            foreach (Control ctrl in Controls)
            {
                ctrl.MouseEnter += Form_MouseEnter;
            }
        }
        private void Hide_All_Components() //temporary implementation, does not hide close button. Need to edit wording in future
        {
            foreach (Control ctrl in Controls)
            {
                if (ctrl is Button && ctrl.TabIndex > 0)
                {
                    ctrl.Hide();
                }
            }
        }
        private void Show_All_Components()
        {
            foreach (Control ctrl in Controls)
            {
                if (ctrl is Button && ctrl.TabIndex > 0)
                {
                    ctrl.Show();
                }
            }
        }

        private void HideTimer_Tick(object? sender, EventArgs e) //?object for nullable sender
        {
            if (!mouseInsideForm)
            {
                HideTimer.Stop(); // Stop further ticks
                Hide_All_Components();
            }
        }

        private void Form_MouseEnter(object? sender, EventArgs e)
        {
            mouseInsideForm = true;
            Show_All_Components(); // Show all components straight away
            HideTimer.Stop(); // Cancel hiding
        }

        private void Form_MouseLeave(object? sender, EventArgs e)
        {
            //checking if the mouse is within the client area (not just on the form directly)
            if (this.ClientRectangle.Contains(this.PointToClient(Control.MousePosition))) 
                return;
            else
                mouseInsideForm = false;
                HideTimer.Start(); // Start 1-second countdown. Delaying the components hiding
        }

        private const int cGrip = 16;      // Grip size
        private const int cCaption = 64;   // Caption bar height. Also used for textbox size and location

        protected override void OnPaint(PaintEventArgs e)
        {
            // Paint the grip in lower right corner
            Rectangle rc = new Rectangle(this.ClientSize.Width - cGrip, this.ClientSize.Height - cGrip, cGrip, cGrip);
            ControlPaint.DrawSizeGrip(e.Graphics, this.BackColor, rc);
            // Create SolidBrush with Background colour
            SolidBrush Brush_Using_BackGround_Colour = new SolidBrush(this.BackColor);
            // Paint the simulated caption bar
            rc = new Rectangle(0, 0, this.ClientSize.Width, cCaption);
            e.Graphics.FillRectangle(Brush_Using_BackGround_Colour, rc);
        }

        protected override void WndProc(ref Message m) //used to process Windows messages
        {
            if (m.Msg == 0x84) // Trap WM_NCHITTEST message from Windows OS to find out where mouse is
            {
                Point pos = new Point(m.LParam.ToInt32()); //get screen coordinates
                pos = this.PointToClient(pos); //convert screen coordinates to client coordinates

                if (pos.Y < cCaption) //if client coordinates is in top bar (caption)
                {
                    m.Result = (IntPtr)2;  // Tell Windows this is top bar (HTCAPTION)
                    return;
                }
                if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                //if client coordinates is in bottom right (cGrip area)
                {
                    m.Result = (IntPtr)17; // Tell Windows this is bottom right and can resize (HTBOTTOMRIGHT)
                    return;
                }
            }
            base.WndProc(ref m); //any other message should behave as normal
        }

        private void Close_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Minimize_Button_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Insert_Text_Button_Click(object sender, EventArgs e)
        {
            RichTextBox Created_RichTextBox = new RichTextBox();
            Resize_RichTextBox(Created_RichTextBox);
            this.Controls.Add(Created_RichTextBox); //ensure all new ctrls lead to a call to Refresh_Form();
            Create_Text_Formatting_Tool_Strip(); //Refresh_Form(); in here
        }
        private void Create_Text_Formatting_Tool_Strip()
        {
            //create instance of toolstrip
            ToolStrip Text_Formatting_ToolStrip = new ToolStrip();

            //create instances of toolstrip items
            ToolStripMenuItem Bold_Button = new ToolStripMenuItem("B");
            ToolStripMenuItem Italics_Button = new ToolStripMenuItem("I");
            ToolStripMenuItem Underline_Button = new ToolStripMenuItem("U");
            ToolStripMenuItem Strikethrough_Button = new ToolStripMenuItem("S");

            //Format toolstrip items
            Bold_Button.Font = new Font(Bold_Button.Font, FontStyle.Bold);
            Italics_Button.Font = new Font(Italics_Button.Font, FontStyle.Italic);
            Underline_Button.Font = new Font(Underline_Button.Font, FontStyle.Underline);
            Strikethrough_Button.Font = new Font(Strikethrough_Button.Font, FontStyle.Strikeout);

            //format toolstrip
            Text_Formatting_ToolStrip.ImageScalingSize = new Size(32, 32);
            Resize_Text_Formatting_ToolStrip(Text_Formatting_ToolStrip);

            //add toolstrip items to toolstrip
            Text_Formatting_ToolStrip.Items.AddRange(new ToolStripMenuItem[] { Bold_Button, Italics_Button, Underline_Button , Strikethrough_Button });

            //events for clicking on toolstrip items
            Bold_Button.Click += Bold_Button_Click;
            Italics_Button.Click += Italics_Button_Click;
            Underline_Button.Click += Underline_Button_Click;
            Strikethrough_Button.Click += Strikethrough_Button_Click;

            //add toolstrip to form
            this.Controls.Add(Text_Formatting_ToolStrip);

            //refreshing form to link new controls to Form_MouseEnter and Form_MouseLeave events
            Refresh_Form(); 
        }

        private void Bold_Button_Click(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void Italics_Button_Click(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void Underline_Button_Click(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void Strikethrough_Button_Click(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Resize_RichTextBox(RichTextBox richTextBox)
        {
            //using top bar (cCaption) size to size and position the richtextbox
            richTextBox.Location = new Point(cCaption, cCaption);
            richTextBox.Height = this.ClientSize.Height - (cCaption * 2);
            richTextBox.Width = this.ClientSize.Width - (cCaption * 2);
            richTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right |
                AnchorStyles.Bottom | AnchorStyles.Top;

        }
        private void Resize_Text_Formatting_ToolStrip(ToolStrip toolStrip)
        {
            //temporary values, may need changing at later date
            toolStrip.Location = new Point(cCaption * 3, this.ClientSize.Height - 84);
            toolStrip.Height = cCaption;
            toolStrip.Width = cCaption * 3;
            toolStrip.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;

        }

    }
}
