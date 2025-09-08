using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Windows.Forms;

namespace Sticky_Notes_Block_App
{
    public partial class Sicky_Notes_Block : Form
    {
        public Sicky_Notes_Block()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            Hide_All_Components();


        }
        private void Hide_All_Components()
        {
            foreach (Control ctrl in Controls)
            {
                if (ctrl is Button && ctrl.TabIndex > 0)
                {
                        ctrl.Hide();
                }
            }
        }
        private const int cGrip = 16;      // Grip size
        private const int cCaption = 32;   // Caption bar height;

        protected override void OnPaint(PaintEventArgs e)
        {
            // Paint the grip in lower right corner
            Rectangle rc = new Rectangle(this.ClientSize.Width - cGrip, this.ClientSize.Height - cGrip, cGrip, cGrip);
            ControlPaint.DrawSizeGrip(e.Graphics, this.BackColor, rc);
            // Paint the simulated caption bar
            rc = new Rectangle(0, 0, this.ClientSize.Width, cCaption);
            e.Graphics.FillRectangle(Brushes.DarkBlue, rc);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {  // Trap WM_NCHITTEST
                Point pos = new Point(m.LParam.ToInt32());
                pos = this.PointToClient(pos);
                if (pos.Y < cCaption)
                {
                    m.Result = (IntPtr)2;  // HTCAPTION
                    return;
                }
                if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)17; // HTBOTTOMRIGHT
                    return;
                }
            }
            base.WndProc(ref m);
        }

        private void Close_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
