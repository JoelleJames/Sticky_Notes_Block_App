
namespace Sticky_Notes_Block_App.Utilities
{
    /// <summary>
    /// Utilities for handling user text input in a sticky note.
    /// </summary>
    public static class InputTextUtilities
    {
        /// <summary>
        /// Dynamically limits the minimum size of the sticky note and prevents input that would 
        /// cause the RichTextBox to exceed its' bounds.
        /// </summary>
        /// <param name="stickyNote">The form representing the current sticky note.</param>
        /// <param name="rtb">The RichTextBox to monitor for input and dynamic sizing.</param>
        /// <remarks>
        /// Expected behavior:
        /// - Prevent text input if it would cause the sticky note to exceed its maximum size 
        ///   (both size overflow and scroll overflow).
        /// - Prevent the sticky note from being resized smaller than what is required to display 
        ///   the current text.
        /// </remarks>
        public static void AttachDynamicMinimumSizeLimiter(Form stickyNote, RichTextBox rtb)
        {
            var maxSize = GetHalfScreenSize();

            rtb.TextChanged += (s, e) => UpdateMinimumSize(stickyNote, rtb);

            rtb.KeyPress += (s, e) =>
            {
                // Skip handling Enter here. Enter handled in KeyDown
                if (char.IsControl(e.KeyChar) && e.KeyChar != (char)Keys.Enter)
                    return;

                if (WouldExceedMaxSize(rtb, stickyNote, e.KeyChar.ToString(), maxSize))
                {
                    e.Handled = true; // Bypass default handling
                }
            };

            rtb.KeyDown += (s, e) =>
            {
                if (IsPasteCommand(e))
                {
                    HandlePasteLimit(rtb, stickyNote, maxSize, e);
                    return;
                }

                if (e.KeyCode == Keys.Enter)
                {
                    var newLine = Environment.NewLine; // Used for newline simulation

                    if (WouldExceedMaxSize(rtb, stickyNote, newLine, maxSize))
                    { 
                        e.Handled = true; // Bypass default handling
                    }
                }
            };
        }

        // --- Helper Methods ---

        private static Size GetHalfScreenSize()
        { // Consider moving this to a constants class if reused elsewhere
            var screenBounds = Screen.PrimaryScreen.Bounds;
            return new Size(screenBounds.Width / 2, screenBounds.Height / 2);
        }

        // Update MinimumSize of sticky note when text changed in text box
        private static void UpdateMinimumSize(Form stickyNote, RichTextBox rtb) 
        {
            var preferredSize = rtb.GetPreferredSize(rtb.Size);

            int widthOffset = stickyNote.Width - rtb.Width;
            int heightOffset = stickyNote.Height - rtb.Height;

            int minWidth = Math.Max(preferredSize.Width + widthOffset, 400);
            int minHeight = Math.Max(preferredSize.Height + heightOffset, 400);

            stickyNote.MinimumSize = new Size(minWidth, minHeight);
        }

        private static bool IsPasteCommand(KeyEventArgs e)
        {
            return e.Control && e.KeyCode == Keys.V;
        }

        private static void HandlePasteLimit(RichTextBox rtb, Form stickyNote, Size maxSize, KeyEventArgs e)
        {
            if (!Clipboard.ContainsText()) return;

            string pasteText = Clipboard.GetText(); // Used for pasting simulation

            if (WouldExceedMaxSize(rtb, stickyNote, pasteText, maxSize)) 
            {
                e.Handled = true; // Bypass default handling
            }
        }

        private static bool WouldExceedMaxSize(RichTextBox rtb, Form stickyNote, string incomingText, Size maxSize)
        {
            int selStart = rtb.SelectionStart;   // Starting position of text selected
            int selLength = rtb.SelectionLength; // Number of characters selected

            string currentText = rtb.Text;

            // Replace the selected text with the incoming input, simulating user typing/pasting
            string simulatedText = currentText.Remove(selStart, selLength) 
                                              .Insert(selStart, incomingText);

            // Simulate text input with a temp text box
            using (var tempRTB = new RichTextBox())
            {
                tempRTB.Font = rtb.Font;
                tempRTB.Text = simulatedText;
                tempRTB.WordWrap = rtb.WordWrap;

                var preferredSize = tempRTB.GetPreferredSize(rtb.Size);

                int widthOffset = stickyNote.Width - rtb.Width;
                int heightOffset = stickyNote.Height - rtb.Height;

                Size projectedSize = new(preferredSize.Width + widthOffset, preferredSize.Height + heightOffset);

                return projectedSize.Width > maxSize.Width || projectedSize.Height > maxSize.Height;
            }
        }
    }
}
