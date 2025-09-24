
using Sticky_Notes_Block_App.Utilities;

namespace Sticky_Notes_Block_App.Factories
{
    /// <summary>
    /// Class for creating instances of controls and formatting them (for text input to a sticky note)
    /// </summary>
    public class TextBox_Controls
    {
        private readonly int _Caption_Bar_Height; //caption bar height used for sizing and locations of controls

        public TextBox_Controls(int Caption_Bar_Height)
        {
            _Caption_Bar_Height = Caption_Bar_Height; 
        }

        public RichTextBox Create_RichTextBox(Form Sticky_Note)
        {
            RichTextBox New_RichTextBox = new()
            {
                Name = "dynamicRichTextBox",

                // Positioning
                Location = new Point(_Caption_Bar_Height, _Caption_Bar_Height + (_Caption_Bar_Height/2)), //top bar * 1.5
                Height = Sticky_Note.Height - (_Caption_Bar_Height * 3), //1.5 from top and 1.5 from bottom
                Width = Sticky_Note.Width - (_Caption_Bar_Height * 2),
                Anchor = AnchorStyles.Left | AnchorStyles.Right |
                AnchorStyles.Bottom | AnchorStyles.Top,

                // Appearance 
                BackColor = Sticky_Note.BackColor,
                BorderStyle = BorderStyle.None,
                ScrollBars = RichTextBoxScrollBars.None,

                // Behaviour
                WordWrap = true, // in case designer overrides defaults
                Multiline = true // in case designer overrides defaults
            };

            InputTextUtilities.AttachDynamicMinimumSizeLimiter(Sticky_Note, New_RichTextBox);

            return New_RichTextBox;
        }

        public ToolStrip Create_Text_Formatting_ToolStrip(Control Sticky_Note)
        {
            ToolStrip New_Text_Formatting_ToolStrip = new()
            {
                Name = "dynamicTextToolStrip",
                Location = new Point(_Caption_Bar_Height * 3, Sticky_Note.Height - 80),
                Height = _Caption_Bar_Height,
                Width = _Caption_Bar_Height * 3,
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom,
                BackColor = Sticky_Note.BackColor,
                GripStyle = ToolStripGripStyle.Hidden
            };

            New_Text_Formatting_ToolStrip.Renderer = new CustomToolStripRenderer(); // for border

            return New_Text_Formatting_ToolStrip;
        }

        private class CustomToolStripRenderer : ToolStripProfessionalRenderer
        {
            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
            {
                // Intentionally blank = no border
            }
        }

        public List<ToolStripMenuItem> Create_Text_Formatting_ToolStrip_Items(RichTextBox targetRichTextBox)
        {
            if (targetRichTextBox == null)
                throw new ArgumentNullException(nameof(targetRichTextBox)); //exception in case textbox null

            return new List<ToolStripMenuItem>
            {
                //CreateItem(label, font, textbox to target)
                CreateItem("B", FontStyle.Bold, targetRichTextBox),
                CreateItem("I", FontStyle.Italic, targetRichTextBox),
                CreateItem("U", FontStyle.Underline, targetRichTextBox),
                CreateItem("S", FontStyle.Strikeout, targetRichTextBox)
            };
        }

        private ToolStripMenuItem CreateItem(string label, FontStyle style, RichTextBox richTextBox)
        {
            var item = new ToolStripMenuItem(label);
            item.Font = new Font("Segoe UI", 10, style); //(font name as string, size, FontStyle)
            item.Click += (sender, e) =>
            {
                ToggleSelectionStyle(richTextBox, style);
            };
            return item;
        }

        private void ToggleSelectionStyle(RichTextBox rtb, FontStyle style)
        {
            if (rtb.SelectionFont == null) return;

            FontStyle currentStyle = rtb.SelectionFont.Style;
            FontStyle newStyle;

            // Toggle newStyle
            if (currentStyle.HasFlag(style))
            {
                newStyle = currentStyle & ~style; // Remove the style
            }
            else
            {
                newStyle = currentStyle | style; // Add the style
            }

            rtb.SelectionFont = new Font(rtb.SelectionFont, newStyle);
        }
        }
}
