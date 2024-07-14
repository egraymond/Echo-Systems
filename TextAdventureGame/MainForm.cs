using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextAdventureGame
{
    public partial class MainForm : Form
    {
        public Label? lblMessage;
        private Label? terminalLabel;
        private Label? asciiArtLabel;
        private Label? lucasLabel;
        private Button? btnOption1;
        private Button? btnOption2;

        private ButtonManager buttonManager;
        private ScreenEffects screenEffects;
        private TextEffects textEffects;

        public MainForm()
        {
            InitializeComponent();
            buttonManager = new ButtonManager(this);
            screenEffects = new ScreenEffects(this);
            textEffects = new TextEffects();
            InitializeGameMenu();
        }
        //Loads and Centers buttons
        private void MainForm_Load(object? sender, EventArgs e)
        {
            buttonManager.CenterButtons();
        }
        //Creates Window
        private void InitializeComponent()
        {
            this.Text = "Text Adventure Game";
            this.ClientSize = new Size(800, 600);
            this.BackColor = Color.Black;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Load += new EventHandler(this.MainForm_Load);
        }
        //Creates labels
        private void InitializeGameMenu()
        {
            lblMessage = new Label
            {
                ForeColor = Color.White,
                BackColor = Color.Black,
                Font = new Font("Consolas", 14),
                Size = new Size(800, 50),
                Location = new Point(0, 250),
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false
            };
            this.Controls.Add(lblMessage);

            terminalLabel = new Label
            {
                ForeColor = Color.White,
                BackColor = Color.Black,
                Font = new Font("Consolas", 12),
                Location = new Point(10, 10),
                Size = new Size(780, 580),
                Visible = false,
                Text = ""
            };
            this.Controls.Add(terminalLabel);

            asciiArtLabel = new Label
            {
                ForeColor = Color.White,
                BackColor = Color.Black,
                Font = new Font("Consolas", 12),
                Size = new Size(780, 580),
                Location = new Point(10, 10),
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false,
                Text = @"
    ______     __             _____            __                     
   / _________/ /_  ____     / ___/__  _______/ /____  ____ ___  _____
  / __/ / ___/ __ \/ __ \    \__ \/ / / / ___/ __/ _ \/ __ `__ \/ ___/
 / /___/ /__/ / / / /_/ /   ___/ / /_/ (__  / /_/  __/ / / / / (__  ) 
/_____/\___/_/ /_/\____/   /____/\__, /____/\__/\___/_/ /_/ /_/____/  
                                /____/                                
"
            };
            this.Controls.Add(asciiArtLabel);

            lucasLabel = new Label
            {
                ForeColor = Color.White,
                BackColor = Color.Black,
                Font = new Font("Consolas", 12),
                Location = new Point(10, 10),
                Size = new Size(780, 580),
                Visible = false
            };
            this.Controls.Add(lucasLabel);

            // WIP - Button not functional
            btnOption1 = new Button
            {
                Text = "Who are you?",
                ForeColor = Color.White,
                BackColor = Color.Black,
                Font = new Font("Consolas", 12),
                Size = new Size(200, 50),
                Location = new Point((this.ClientSize.Width - 200) / 2 - 110, this.ClientSize.Height - 100),
                Visible = false,
                FlatStyle = FlatStyle.Flat
            };
            btnOption1.FlatAppearance.BorderSize = 1;
            btnOption1.FlatAppearance.MouseOverBackColor = Color.White;
            btnOption1.FlatAppearance.MouseDownBackColor = Color.DarkGray;
            btnOption1.FlatAppearance.BorderColor = Color.White;
            btnOption1.Click += (s, e) => RespondToOption("Who are you?");
            this.Controls.Add(btnOption1);

            // WIP - Button not functional
            btnOption2 = new Button
            {
                Text = "What is going on?",
                ForeColor = Color.White,
                BackColor = Color.Black,
                Font = new Font("Consolas", 12),
                Size = new Size(200, 50),
                Location = new Point((this.ClientSize.Width - 200) / 2 + 110, this.ClientSize.Height - 100),
                Visible = false,
                FlatStyle = FlatStyle.Flat
            };
            btnOption2.FlatAppearance.BorderSize = 1;
            btnOption2.FlatAppearance.MouseOverBackColor = Color.White;
            btnOption2.FlatAppearance.MouseDownBackColor = Color.DarkGray;
            btnOption2.FlatAppearance.BorderColor = Color.White;
            btnOption2.Click += (s, e) => RespondToOption("What is going on?");
            this.Controls.Add(btnOption2);

            buttonManager.CreateMainMenuButtons(StartNewGame, ShowMessage, ConfirmQuit);
        }
        //Begins intro 
        private async Task StartNewGame()
        {
            await buttonManager.FadeOutButtons();
            await screenEffects.FadeScreen(Color.White);
            await screenEffects.FadeScreen(Color.Black);
            await Task.Delay(2000); // Wait for 2 seconds

            terminalLabel!.Visible = true;
            await textEffects.TypewriterEffect(terminalLabel, "C:\\Intro\\textadventuregame\\users\\max>");
            await textEffects.BlinkingCursor(terminalLabel, 3);
            await textEffects.TypewriterEffect(terminalLabel, "Echo --force --break --open --communicate");

            await Task.Delay(1000); // Wait for 1 second
            terminalLabel.Text += "\n\nC:\\Intro\\textadventuregame\\users\\max>";
            await Task.Delay(1000); // Wait for 1 second
            terminalLabel.Text += "\n    system break, forcing open communication";
            await textEffects.DotAnimation(terminalLabel, 3);

            await Task.Delay(1000); // Wait for 1 second
            terminalLabel.Text += "\n\nSuccess";

            await Task.Delay(2000); // Wait for 2 seconds

            terminalLabel.Visible = false; // Hide terminalLabel
            await screenEffects.FadeScreen(Color.Black);
            asciiArtLabel!.Visible = true; // Display ASCII art
            asciiArtLabel.Text = ""; // Clear text before typing
            await textEffects.TypewriterEffect(asciiArtLabel, @"
    ______     __             _____            __                     
   / _________/ /_  ____     / ___/__  _______/ /____  ____ ___  _____
  / __/ / ___/ __ \/ __ \    \__ \/ / / / ___/ __/ _ \/ __ `__ \/ ___/
 / /___/ /__/ / / / /_/ /   ___/ / /_/ (__  / /_/  __/ / / / / (__  ) 
/_____/\___/_/ /_/\____/   /____/\__, /____/\__/\___/_/ /_/ /_/____/  
                                /____/                                
", 10);

            await Task.Delay(3000); // Wait for 3 seconds
            asciiArtLabel.Visible = false; // Hide ASCII art

            await Task.Delay(1000); // Wait for 1 second
            lucasLabel!.Visible = true;
            await textEffects.BlinkingCursor(lucasLabel, 3);
            textEffects.ChangeTextColor(lucasLabel, "[Lucas]:", Color.Blue);
            await textEffects.TypewriterEffect(lucasLabel, " Hello? Is anyone there?", 1500);

            await Task.Delay(1000); // Wait for 1 second
            btnOption1!.Visible = true;
            btnOption1.Enabled = true;
            btnOption2!.Visible = true;
            btnOption2.Enabled = true;
        }
        // WIP - Does not function
        private async void RespondToOption(string option)
        {
            btnOption1!.Visible = false;
            btnOption2!.Visible = false;

            lucasLabel!.Text += $"\n\n[asdfaf]: {option}";
            await textEffects.EncryptedText(lucasLabel, "[asdfaf]: " + option, 2);
        }

        public async Task ShowMessage(string message, bool showBackButton)
        {
            await buttonManager.FadeOutButtons();
            if (lblMessage != null)
            {
                lblMessage.Visible = true;
                await textEffects.TypewriterEffect(lblMessage, message);
                await Task.Delay(1000);
                if (showBackButton)
                {
                    buttonManager.ShowBackButton(lblMessage.Bottom + 10);
                }
            }
        }
        // Quit Button Function
        private async Task ConfirmQuit()
        {
            await buttonManager.FadeOutButtons();
            this.Text = "Don't Go";
            if (lblMessage != null)
            {
                lblMessage.Visible = true;
                await textEffects.TypewriterEffect(lblMessage, "Are you sure you want to quit?");
                await Task.Delay(1000);
                buttonManager.ShowYesNoButtons(lblMessage.Bottom + 10);
            }
        }
        // Displays the main menu whenever "back" or "no" is pressed, also when the game loads naturally.
        public void ShowMainMenu()
        {
            this.Text = "Text Adventure Game";
            if (lblMessage != null)
            {
                lblMessage.Visible = false;
                lblMessage.Text = ""; // Clear text
            }
            if (terminalLabel != null) terminalLabel.Visible = false;
            if (asciiArtLabel != null) asciiArtLabel.Visible = false;
            if (lucasLabel != null) lucasLabel.Visible = false;

            var buttons = new[] { buttonManager.btnNewGame, buttonManager.btnLoadGame, buttonManager.btnOptions, buttonManager.btnQuitGame };
            foreach (var btn in buttons)
            {
                if (btn != null)
                {
                    btn.Visible = true;
                    btn.ForeColor = Color.White;
                    btn.BackColor = Color.Black;
                    btn.Enabled = true;
                }
            }
            buttonManager.CenterButtons();
        }
    }
}
