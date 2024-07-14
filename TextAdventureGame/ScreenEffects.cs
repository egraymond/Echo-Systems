using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextAdventureGame
{
    public class ScreenEffects
    {
        private MainForm mainForm;

        public ScreenEffects(MainForm form)
        {
            mainForm = form;
        }
        //Only used when pressing "new game" 
        public async Task FadeScreen(Color color)
        {
            var fadePanel = new Panel
            {
                Size = mainForm.ClientSize,
                Location = new Point(0, 0),
                BackColor = Color.Transparent
            };
            mainForm.Controls.Add(fadePanel);
            for (int i = 0; i <= 255; i += 25)
            {
                fadePanel.BackColor = Color.FromArgb(i, color);
                await Task.Delay(25);
            }
            for (int i = 255; i >= 0; i -= 25)
            {
                fadePanel.BackColor = Color.FromArgb(i, color);
                await Task.Delay(25);
            }
            mainForm.Controls.Remove(fadePanel);
        }
    }
}
