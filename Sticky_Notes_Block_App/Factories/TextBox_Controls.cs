
namespace Sticky_Notes_Block_App.Factories
{
    public class TextBox_Controls
    {
        private readonly int _Caption_Bar_Height; //caption bar height used for sizing and locations of controls

        public TextBox_Controls(int Caption_Bar_Height)
        {
            _Caption_Bar_Height = Caption_Bar_Height; 
        }

        public RichTextBox Create_RichTextBox(Size Form_Size)
        {
            RichTextBox New_RichTextBox = new()
            {
                Name = "dynamicRichTextBox",
                Location = new Point(_Caption_Bar_Height, _Caption_Bar_Height),
                Height = Form_Size.Height - (_Caption_Bar_Height * 2),
                Width = Form_Size.Width - (_Caption_Bar_Height * 2),
                Anchor = AnchorStyles.Left | AnchorStyles.Right |
                AnchorStyles.Bottom | AnchorStyles.Top
            };

            return New_RichTextBox;
        }

        public ToolStrip Create_Text_Formatting_ToolStrip(Size Form_Size)
        {
            ToolStrip New_Text_Formatting_ToolStrip = new()
            {
                Name = "dynamicTextToolStrip",
                Location = new Point(_Caption_Bar_Height * 3, Form_Size.Height - 84),
                Height = _Caption_Bar_Height,
                Width = _Caption_Bar_Height * 3,
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom
            };


            return New_Text_Formatting_ToolStrip;
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
            item.Font = new Font(item.Font, style); //style the items
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
