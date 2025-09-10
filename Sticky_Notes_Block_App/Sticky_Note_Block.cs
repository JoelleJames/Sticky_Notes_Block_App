using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Windows.Forms;

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
            HideTimer = new System.Windows.Forms.Timer();
            HideTimer.Interval = 1000; // 1 second
            HideTimer.Tick += HideTimer_Tick;

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

        private void Form_MouseEnter(object sender, EventArgs e)
        {
            mouseInsideForm = true;
            Show_All_Components(); // Show all components straight away
            HideTimer.Stop(); // Cancel hiding
        }

        private void Form_MouseLeave(object sender, EventArgs e)
        {
            mouseInsideForm = false;
            HideTimer.Start(); // Start 1-second countdown. Delaying the components hiding
        }

        private const int cGrip = 16;      // Grip size
        private const int cCaption = 48;   // Caption bar height;

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
                Show_All_Components();
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
    }
}
