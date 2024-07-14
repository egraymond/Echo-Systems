using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextAdventureGame
{
    public class TextEffects
    {
        //Typerwriter effect. Select a label and the text.
        public async Task TypewriterEffect(Label label, string text, int delay = 1500) //Change timer based on how fast you want it to be
        {
            foreach (char c in text)
            {
                label.Text += c;
                await Task.Delay(delay / text.Length);
            }
        }
        //Creates a blinking cursor animation whenever called, used indicating someone is able to type something.
        public async Task BlinkingCursor(Label label, int duration)
        {
            string cursor = "_";
            for (int i = 0; i < duration * 2; i++) // Blinking every 500ms for the specified duration in seconds
            {
                label.Text = label.Text.EndsWith(cursor) ? label.Text.TrimEnd('_') : label.Text + cursor;
                await Task.Delay(500);
            }
            label.Text = label.Text.TrimEnd('_'); // Ensure the cursor is removed after blinking
        }
        //Create a dot animation whenever called
        public async Task DotAnimation(Label label, int repetitions)
        {
            string baseText = label.Text;
            for (int i = 0; i < repetitions; i++)
            {
                for (int j = 1; j <= 3; j++)
                {
                    label.Text = baseText + new string('.', j);
                    await Task.Delay(1000);
                }
            }
            label.Text = baseText + "..."; // Ensure it ends with "..."
        }
        //Does not function as intended (Haven't gotten that far yet)
        public async Task EncryptedText(Label label, string text, int duration)
        {
            Random random = new Random();
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            int length = text.Length;

            for (int i = 0; i < duration * 10; i++) // Update every 100ms
            {
                char[] randomText = new char[length];
                for (int j = 0; j < length; j++)
                {
                    randomText[j] = characters[random.Next(characters.Length)];
                }
                label.Text = new string(randomText);
                await Task.Delay(100);
            }
            label.Text = text; // Set the final text
        }
        //Changes text color for certain character when they speak, and their names too
        public void ChangeTextColor(Label label, string text, Color color)
        {
            label.ForeColor = color;
            label.Text = text;
        }
    }
}
