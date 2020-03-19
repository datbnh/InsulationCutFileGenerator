using System.Drawing;
using System.Windows.Forms;

namespace InsulationCutFileGeneratorMVC.Helpers
{
    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox richTextBox, string text, FontStyle fontStyle)
        {
            var currentFont = richTextBox.Font;

            var startIdx = richTextBox.TextLength;
            richTextBox.AppendText(text);
            richTextBox.Select(startIdx, richTextBox.TextLength);
            richTextBox.SelectionFont = new Font(richTextBox.Font, fontStyle);
            richTextBox.Select(richTextBox.TextLength, richTextBox.TextLength);
            richTextBox.SelectionFont = currentFont;
        }

        public static void AppendText(this RichTextBox richTextBox, string text,
            Color textColor, FontStyle fontStyle)
        {
            var currentFont = richTextBox.Font;

            var startIdx = richTextBox.TextLength;
            richTextBox.AppendText(text);
            richTextBox.Select(startIdx, richTextBox.TextLength);
            richTextBox.SelectionFont = new Font(richTextBox.Font, fontStyle);
            richTextBox.SelectionColor = textColor;
            richTextBox.Select(richTextBox.TextLength, richTextBox.TextLength);
            richTextBox.SelectionFont = currentFont;
            richTextBox.SelectionColor = richTextBox.ForeColor;
        }
    }
}