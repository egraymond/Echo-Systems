using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextAdventureGame
{
    public class ButtonManager
    {
        private MainForm mainForm;

        public Button? btnNewGame;
        public Button? btnLoadGame;
        public Button? btnOptions;
        public Button? btnQuitGame;
        private Button? btnBack;
        private Button? btnYes;
        private Button? btnNo;

        public ButtonManager(MainForm form)
        {
            mainForm = form;
        }
        //All Main Menu Buttons
        public void CreateMainMenuButtons(Func<Task> startNewGame, Func<string, bool, Task> showMessage, Func<Task> confirmQuit)
        {
            btnNewGame = CreateButton("New Game", async (s, e) => await startNewGame());
            btnLoadGame = CreateButton("Load Game", async (s, e) => await showMessage("This feature has not been implemented", true));
            btnOptions = CreateButton("Options", async (s, e) => await showMessage("This feature has not been implemented", true));
            btnQuitGame = CreateButton("Quit Game", async (s, e) => await confirmQuit());

            btnBack = CreateButton("Back", (s, e) => { ClearMessage(); HideBackButton(); mainForm.ShowMainMenu(); });
            btnBack.Visible = false;

            btnYes = CreateButton("Yes", (s, e) => { HideYesNoButtons(); mainForm.Close(); });
            btnNo = CreateButton("No", (s, e) => { ClearMessage(); HideYesNoButtons(); mainForm.ShowMainMenu(); });

            btnYes.Visible = false;
            btnNo.Visible = false;

            mainForm.Controls.AddRange(new Control[] { btnNewGame, btnLoadGame, btnOptions, btnQuitGame, btnBack, btnYes, btnNo });
        }
        //All buttons follow the same format when created
        private Button CreateButton(string text, EventHandler onClick)
        {
            var button = new Button
            {
                Text = text,
                ForeColor = Color.White,
                BackColor = Color.Black,
                Font = new Font("Consolas", 12),
                Size = new Size(100, 30),
                FlatStyle = FlatStyle.Flat
            };
            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.MouseOverBackColor = Color.White;
            button.FlatAppearance.MouseDownBackColor = Color.DarkGray;
            button.FlatAppearance.BorderColor = Color.White;
            button.Click += onClick;
            return button;
        }
        // Centers Buttons when needed
        public void CenterButtons()
        {
            if (btnNewGame == null || btnLoadGame == null || btnOptions == null || btnQuitGame == null) return;

            int buttonWidth = btnNewGame.Width;
            int buttonHeight = btnNewGame.Height;
            int spacing = 10;
            int totalHeight = (buttonHeight + spacing) * 4 - spacing;
            int startY = (mainForm.ClientSize.Height - totalHeight) / 2;
            int centerX = (mainForm.ClientSize.Width - buttonWidth) / 2;

            btnNewGame.Location = new Point(centerX, startY);
            btnLoadGame.Location = new Point(centerX, startY + (buttonHeight + spacing));
            btnOptions.Location = new Point(centerX, startY + 2 * (buttonHeight + spacing));
            btnQuitGame.Location = new Point(centerX, startY + 3 * (buttonHeight + spacing));
        }
        // Have buttons fade when selected
        public async Task FadeOutButtons()
        {
            var buttons = new[] { btnNewGame, btnLoadGame, btnOptions, btnQuitGame, btnBack, btnYes, btnNo };
            foreach (var btn in buttons)
            {
                if (btn != null) btn.Enabled = false;
            }
            for (int i = 0; i < 10; i++)
            {
                foreach (var btn in buttons)
                {
                    if (btn != null)
                    {
                        btn.ForeColor = DecreaseColorAlpha(btn.ForeColor, 25);
                        btn.BackColor = DecreaseColorAlpha(btn.BackColor, 25);
                    }
                }
                await Task.Delay(50);
            }
            foreach (var btn in buttons)
            {
                if (btn != null) btn.Visible = false;
            }
        }
        // Defaults colors of button. 
        private Color DecreaseColorAlpha(Color color, int amount)
        {
            int newAlpha = Math.Max(color.A - amount, 0);
            return Color.FromArgb(newAlpha, color.R, color.G, color.B);
        }
        // Displays a back button when "Load Game" or "Options" is pressed
        public void ShowBackButton(int yPosition)
        {
            if (btnBack != null)
            {
                btnBack.Location = new Point((mainForm.ClientSize.Width - btnBack.Width) / 2, yPosition);
                btnBack.Visible = true;
                btnBack.Enabled = true;
            }
        }
        // Displays a "yes" or "no" button whenever "Quit Game" is pressed
        public void ShowYesNoButtons(int yPosition)
        {
            if (btnYes != null && btnNo != null)
            {
                btnYes.Location = new Point((mainForm.ClientSize.Width - btnYes.Width - btnNo.Width - 10) / 2, yPosition);
                btnNo.Location = new Point(btnYes.Right + 10, btnYes.Top);
                btnYes.Visible = true;
                btnNo.Visible = true;
                btnYes.Enabled = true;
                btnNo.Enabled = true;
            }
        }
        // Hides back button when they are pressed
        private void HideBackButton()
        {
            if (btnBack != null)
            {
                btnBack.Visible = false;
                btnBack.Enabled = false;
            }
        }
        // Hides yes and no button when either are pressed
        private void HideYesNoButtons()
        {
            if (btnYes != null && btnNo != null)
            {
                btnYes.Visible = false;
                btnYes.Enabled = false;
                btnNo.Visible = false;
                btnNo.Enabled = false;
            }
        }
        //Clears the message that comes with the button. 
        private void ClearMessage()
        {
            if (mainForm.lblMessage != null)
            {
                mainForm.lblMessage.Text = "";
            }
        }
    }
}
