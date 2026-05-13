using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoolRidesSimulator
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Show splash screen
            ShowSplashAndRun();
        }

        private static void ShowSplashAndRun()
        {
            Form splash = new Form()
            {
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.CenterScreen,
                Size = new System.Drawing.Size(450, 280),
                BackColor = System.Drawing.Color.FromArgb(28, 28, 38),
                TopMost = true
            };

            // Animated dots for loading effect
            Label loadingLabel = new Label()
            {
                Text = "Loading",
                Font = new System.Drawing.Font("Segoe UI", 12),
                ForeColor = System.Drawing.Color.FromArgb(200, 200, 220),
                Location = new System.Drawing.Point(150, 200),
                Size = new System.Drawing.Size(150, 30),
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                BackColor = System.Drawing.Color.Transparent
            };

            Label splashText = new Label()
            {
                Text = "🏭 COOL RIDES PRODUCTION SYSTEM\n\n" +
                       "Initializing Assembly Lines...\n" +
                       "Loading Parts Factories...\n" +
                       "Starting Spraybooth...\n\n" +
                       "Applying Design Patterns:\n" +
                       "• Factory Method\n" +
                       "• Abstract Factory\n" +
                       "• Command\n" +
                       "• Singleton",
                Font = new System.Drawing.Font("Segoe UI", 11),
                ForeColor = System.Drawing.Color.FromArgb(255, 200, 100),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                BackColor = System.Drawing.Color.Transparent
            };

            splash.Controls.Add(splashText);
            splash.Controls.Add(loadingLabel);

            // Animate dots
            int dotCount = 0;
            Timer dotTimer = new Timer();
            dotTimer.Interval = 500;
            dotTimer.Tick += (s, e) =>
            {
                dotCount = (dotCount % 3) + 1;
                loadingLabel.Text = "Loading" + new string('.', dotCount);
            };
            dotTimer.Start();

            // Show splash then switch to MainForm
            splash.Shown += async (s, e) =>
            {
                await Task.Delay(2500); // Show splash for 2.5 seconds
                dotTimer.Stop();
                splash.Close();
                splash.Dispose();
                
            };

            Application.Run(splash);
            Application.Run(new MainForm());
        }
    }
}