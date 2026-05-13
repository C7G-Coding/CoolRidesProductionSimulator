using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoolRidesSimulator
{
    public partial class MainForm : Form
    {
        private HQ _hq;
        private CarAssemblyLine _carLine;
        private MinibusAssemblyLine _minibusLine;
        private Timer _statusTimer;
        private Timer _animationTimer;

        // Animation variables
        private float _carProgress = 0;
        private float _busProgress = 0;
        private float _sprayProgress = 0;
        private int _pulseValue = 0;
        private bool _pulseDirection = true;

        // UI Controls
        private Button btnLUX1000Black, btnLUX1000White;
        private Button btnMV500Black, btnMV500White;
        private Label lblCarStatus, lblCarCurrentTask, lblCarQueueCount, lblCarTime;
        private Label lblBusStatus, lblBusCurrentTask, lblBusQueueCount, lblBusTime;
        private Label lblSprayboothStatus, lblSprayboothCurrent, lblSprayTime;
        private ListBox lstOrderHistory;
        private ProgressBar progressCar, progressBus, progressSpray;
        private Panel panelCarAnimation, panelBusAnimation, panelSprayAnimation;
        private Label lblCarAnimIcon, lblBusAnimIcon, lblSprayAnimIcon;
        private Timer carAnimTimer, busAnimTimer, sprayAnimTimer;
        private int carAnimFrame = 0, busAnimFrame = 0, sprayAnimFrame = 0;

        public MainForm()
        {
            // Enable double buffering for smooth rendering
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();

            InitializeProfessionalComponents();

            _carLine = new CarAssemblyLine();
            _minibusLine = new MinibusAssemblyLine();
            _hq = new HQ(_carLine, _minibusLine);

            Task.Run(() => _carLine.ProcessQueueAsync());
            Task.Run(() => _minibusLine.ProcessQueueAsync());

            _statusTimer = new Timer();
            _statusTimer.Interval = 100;
            _statusTimer.Tick += UpdateStatus;
            _statusTimer.Start();

            _animationTimer = new Timer();
            _animationTimer.Interval = 50;
            _animationTimer.Tick += AnimateUI;
            _animationTimer.Start();
        }

        private void InitializeProfessionalComponents()
        {
            this.Text = "Cool Rides Production Simulator v2.0";
            this.Size = new Size(1100, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(18, 18, 28);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Header Panel with Gradient
            Panel headerPanel = new Panel()
            {
                Location = new Point(0, 0),
                Size = new Size(1100, 80),
                BackColor = Color.FromArgb(28, 28, 38)
            };

            Label lblTitle = new Label()
            {
                Text = "🏭 COOL RIDES PRODUCTION SYSTEM 🚗",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                Location = new Point(20, 20),
                Size = new Size(1060, 45),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(255, 200, 100),
                BackColor = Color.Transparent
            };

            Label lblSubtitle = new Label()
            {
                Text = "Factory Method • Abstract Factory • Command • Singleton Patterns",
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                Location = new Point(20, 55),
                Size = new Size(1060, 25),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(150, 150, 170),
                BackColor = Color.Transparent
            };

            headerPanel.Controls.AddRange(new Control[] { lblTitle, lblSubtitle });

            // ========== CAR ASSEMBLY CARD ==========
            Panel carCard = CreateCardPanel("🚗 CAR ASSEMBLY LINE", "LUX1000", new Point(20, 100), 500, 260);

            // Buttons
            btnLUX1000Black = CreateGradientButton("ORDER BLACK", Color.FromArgb(30, 30, 40), Color.Black, new Point(25, 60), 210, 45);
            btnLUX1000Black.Click += (s, e) => { _hq.OrderCar("Black"); AddOrderToHistory("CAR", "Black LUX1000"); };

            btnLUX1000White = CreateGradientButton("ORDER WHITE", Color.FromArgb(220, 220, 230), Color.White, new Point(255, 60), 210, 45);
            btnLUX1000White.Click += (s, e) => { _hq.OrderCar("White"); AddOrderToHistory("CAR", "White LUX1000"); };

            // Status indicators
            lblCarStatus = CreateStatusLabel("● IDLE", new Point(25, 120), Color.FromArgb(80, 200, 80));
            lblCarCurrentTask = CreateInfoLabel("Current: None", new Point(25, 148), 250);
            lblCarQueueCount = CreateInfoLabel("Queue: 0", new Point(25, 172), 150);
            lblCarTime = CreateInfoLabel("⏱️ Est. remaining: --", new Point(200, 172), 200);

            // Progress Bar
            progressCar = new ProgressBar()
            {
                Location = new Point(25, 200),
                Size = new Size(440, 12),
                Style = ProgressBarStyle.Continuous,
                ForeColor = Color.FromArgb(80, 200, 80),
                BackColor = Color.FromArgb(60, 60, 70)
            };

            // Animation panel for car
            panelCarAnimation = new Panel()
            {
                Location = new Point(25, 220),
                Size = new Size(440, 30),
                BackColor = Color.FromArgb(28, 28, 38)
            };
            lblCarAnimIcon = new Label()
            {
                Text = "🚗",
                Font = new Font("Segoe UI", 18),
                Location = new Point(0, 0),
                Size = new Size(40, 30),
                BackColor = Color.Transparent
            };
            panelCarAnimation.Controls.Add(lblCarAnimIcon);

            carCard.Controls.AddRange(new Control[] { btnLUX1000Black, btnLUX1000White, lblCarStatus, lblCarCurrentTask, lblCarQueueCount, lblCarTime, progressCar, panelCarAnimation });

            // ========== MINIBUS ASSEMBLY CARD ==========
            Panel busCard = CreateCardPanel("🚐 MINIBUS ASSEMBLY LINE", "MV500", new Point(540, 100), 520, 260);

            btnMV500Black = CreateGradientButton("ORDER BLACK", Color.FromArgb(30, 30, 40), Color.Black, new Point(25, 60), 220, 45);
            btnMV500Black.Click += (s, e) => { _hq.OrderMinibus("Black"); AddOrderToHistory("MINIBUS", "Black MV500"); };

            btnMV500White = CreateGradientButton("ORDER WHITE", Color.FromArgb(220, 220, 230), Color.White, new Point(265, 60), 220, 45);
            btnMV500White.Click += (s, e) => { _hq.OrderMinibus("White"); AddOrderToHistory("MINIBUS", "White MV500"); };

            lblBusStatus = CreateStatusLabel("● IDLE", new Point(25, 120), Color.FromArgb(80, 200, 80));
            lblBusCurrentTask = CreateInfoLabel("Current: None", new Point(25, 148), 260);
            lblBusQueueCount = CreateInfoLabel("Queue: 0", new Point(25, 172), 150);
            lblBusTime = CreateInfoLabel("⏱️ Est. remaining: --", new Point(200, 172), 200);

            progressBus = new ProgressBar()
            {
                Location = new Point(25, 200),
                Size = new Size(460, 12),
                Style = ProgressBarStyle.Continuous,
                ForeColor = Color.FromArgb(80, 180, 255),
                BackColor = Color.FromArgb(60, 60, 70)
            };

            panelBusAnimation = new Panel()
            {
                Location = new Point(25, 220),
                Size = new Size(460, 30),
                BackColor = Color.FromArgb(28, 28, 38)
            };
            lblBusAnimIcon = new Label()
            {
                Text = "🚐",
                Font = new Font("Segoe UI", 18),
                Location = new Point(0, 0),
                Size = new Size(40, 30),
                BackColor = Color.Transparent
            };
            panelBusAnimation.Controls.Add(lblBusAnimIcon);

            busCard.Controls.AddRange(new Control[] { btnMV500Black, btnMV500White, lblBusStatus, lblBusCurrentTask, lblBusQueueCount, lblBusTime, progressBus, panelBusAnimation });

            // ========== SPRAYBOOTH CARD ==========
            Panel sprayCard = CreateCardPanel("🎨 SPRAYBOOTH", "Singleton Pattern", new Point(20, 380), 1040, 140);
            sprayCard.BackColor = Color.FromArgb(35, 30, 45);

            lblSprayboothStatus = CreateStatusLabel("● READY", new Point(25, 70), Color.FromArgb(80, 200, 80), 16);
            lblSprayboothCurrent = new Label()
            {
                Text = "📦 Waiting for vehicles...",
                Location = new Point(200, 72),
                Size = new Size(300, 30),
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.FromArgb(200, 200, 220),
                BackColor = Color.Transparent
            };
            lblSprayTime = new Label()
            {
                Text = "⏱️ Spray time: --",
                Location = new Point(530, 72),
                Size = new Size(180, 30),
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(150, 150, 170),
                BackColor = Color.Transparent
            };

            progressSpray = new ProgressBar()
            {
                Location = new Point(25, 105),
                Size = new Size(990, 10),
                Style = ProgressBarStyle.Continuous,
                ForeColor = Color.FromArgb(255, 160, 80),
                BackColor = Color.FromArgb(60, 60, 70)
            };

            sprayCard.Controls.AddRange(new Control[] { lblSprayboothStatus, lblSprayboothCurrent, lblSprayTime, progressSpray });

            // ========== ORDER HISTORY PANEL ==========
            Panel historyPanel = new Panel()
            {
                Location = new Point(20, 540),
                Size = new Size(1040, 160),
                BackColor = Color.FromArgb(25, 25, 35),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblHistoryTitle = new Label()
            {
                Text = "📋 ORDER HISTORY",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(15, 10),
                Size = new Size(200, 30),
                ForeColor = Color.FromArgb(255, 200, 100),
                BackColor = Color.Transparent
            };

            lstOrderHistory = new ListBox()
            {
                Location = new Point(15, 45),
                Size = new Size(1010, 100),
                Font = new Font("Consolas", 9),
                BackColor = Color.FromArgb(20, 20, 30),
                ForeColor = Color.FromArgb(180, 255, 180),
                BorderStyle = BorderStyle.None
            };

            historyPanel.Controls.AddRange(new Control[] { lblHistoryTitle, lstOrderHistory });

            // Footer
            Panel footerPanel = new Panel()
            {
                Location = new Point(0, 715),
                Size = new Size(1100, 30),
                BackColor = Color.FromArgb(15, 15, 22)
            };

            Label lblFooter = new Label()
            {
                Text = "Design Patterns: Abstract Factory | Factory Method | Command | Singleton | Threading Simulation",
                Font = new Font("Segoe UI", 8),
                Location = new Point(20, 8),
                Size = new Size(1060, 20),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(100, 100, 120),
                BackColor = Color.Transparent
            };
            footerPanel.Controls.Add(lblFooter);

            this.Controls.AddRange(new Control[] { headerPanel, carCard, busCard, sprayCard, historyPanel, footerPanel });
        }

        private Panel CreateCardPanel(string title, string subtitle, Point location, int width, int height)
        {
            Panel panel = new Panel()
            {
                Location = location,
                Size = new Size(width, height),
                BackColor = Color.FromArgb(28, 28, 38),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label titleLabel = new Label()
            {
                Text = title,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(15, 12),
                Size = new Size(250, 25),
                ForeColor = Color.FromArgb(255, 200, 100),
                BackColor = Color.Transparent
            };

            Label subtitleLabel = new Label()
            {
                Text = subtitle,
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                Location = new Point(15, 35),
                Size = new Size(200, 20),
                ForeColor = Color.FromArgb(130, 130, 150),
                BackColor = Color.Transparent
            };

            panel.Controls.AddRange(new Control[] { titleLabel, subtitleLabel });
            return panel;
        }

        private Button CreateGradientButton(string text, Color backColor1, Color backColor2, Point location, int width, int height)
        {
            Button btn = new Button()
            {
                Text = text,
                Location = location,
                Size = new Size(width, height),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                BackColor = backColor1,
                ForeColor = text == "ORDER BLACK" ? Color.White : Color.Black
            };
            btn.FlatAppearance.BorderSize = 0;

            // Hover effects
            btn.MouseEnter += (s, e) => { btn.BackColor = ControlPaint.Light(backColor1, 0.2f); btn.Cursor = Cursors.Hand; };
            btn.MouseLeave += (s, e) => { btn.BackColor = backColor1; };

            return btn;
        }

        private Label CreateStatusLabel(string text, Point location, Color color, int fontSize = 11)
        {
            return new Label()
            {
                Text = text,
                Location = location,
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", fontSize, FontStyle.Bold),
                ForeColor = color,
                BackColor = Color.Transparent
            };
        }

        private Label CreateInfoLabel(string text, Point location, int width)
        {
            return new Label()
            {
                Text = text,
                Location = location,
                Size = new Size(width, 22),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(180, 180, 200),
                BackColor = Color.Transparent
            };
        }

        private void AddOrderToHistory(string type, string order)
        {
            if (lstOrderHistory.InvokeRequired)
            {
                lstOrderHistory.Invoke(new Action(() => AddOrderToHistory(type, order)));
                return;
            }
            lstOrderHistory.Items.Insert(0, $"[{DateTime.Now:HH:mm:ss}] {type}: {order}");
            if (lstOrderHistory.Items.Count > 30)
                lstOrderHistory.Items.RemoveAt(lstOrderHistory.Items.Count - 1);
        }

        private void AnimateUI(object sender, EventArgs e)
        {
            // Pulse animation for idle/busy indicators
            _pulseValue += _pulseDirection ? 5 : -5;
            if (_pulseValue >= 255) { _pulseValue = 255; _pulseDirection = false; }
            if (_pulseValue <= 100) { _pulseValue = 100; _pulseDirection = true; }

            // Update progress bars based on actual status
            if (_carLine.IsBusy)
            {
                _carProgress = Math.Min(100, _carProgress + 2);
                progressCar.Value = (int)_carProgress;

                // Animate car moving
                int x = (int)((progressCar.Value / 100f) * 400);
                lblCarAnimIcon.Location = new Point(x, 0);
            }
            else
            {
                if (_carProgress > 0) _carProgress = Math.Max(0, _carProgress - 1);
                progressCar.Value = (int)_carProgress;
                lblCarAnimIcon.Location = new Point(0, 0);
            }

            if (_minibusLine.IsBusy)
            {
                _busProgress = Math.Min(100, _busProgress + 2);
                progressBus.Value = (int)_busProgress;

                int x = (int)((progressBus.Value / 100f) * 420);
                lblBusAnimIcon.Location = new Point(x, 0);
            }
            else
            {
                if (_busProgress > 0) _busProgress = Math.Max(0, _busProgress - 1);
                progressBus.Value = (int)_busProgress;
                lblBusAnimIcon.Location = new Point(0, 0);
            }

            if (Spraybooth.Instance.IsSpraying)
            {
                _sprayProgress = Math.Min(100, _sprayProgress + 2);
                progressSpray.Value = (int)_sprayProgress;
            }
            else
            {
                if (_sprayProgress > 0) _sprayProgress = Math.Max(0, _sprayProgress - 1);
                progressSpray.Value = (int)_sprayProgress;
            }
        }

        private void UpdateStatus(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateStatus(sender, e)));
                return;
            }

            // Car status
            if (_carLine.IsBusy)
            {
                lblCarStatus.Text = "● BUSY";
                lblCarStatus.ForeColor = Color.FromArgb(255, 100, 100);
            }
            else
            {
                lblCarStatus.Text = "● IDLE";
                lblCarStatus.ForeColor = Color.FromArgb(80, 200, 80);
            }
            lblCarCurrentTask.Text = $"🔧 Current: {_carLine.GetCurrentTaskDescription()}";
            lblCarQueueCount.Text = $"📋 Queue: {_carLine.GetQueueCount()} orders";

            // Minibus status
            if (_minibusLine.IsBusy)
            {
                lblBusStatus.Text = "● BUSY";
                lblBusStatus.ForeColor = Color.FromArgb(255, 100, 100);
            }
            else
            {
                lblBusStatus.Text = "● IDLE";
                lblBusStatus.ForeColor = Color.FromArgb(80, 200, 80);
            }
            lblBusCurrentTask.Text = $"🔧 Current: {_minibusLine.GetCurrentTaskDescription()}";
            lblBusQueueCount.Text = $"📋 Queue: {_minibusLine.GetQueueCount()} orders";

            // Spraybooth status
            if (Spraybooth.Instance.IsSpraying)
            {
                lblSprayboothStatus.Text = "● SPRAYING";
                lblSprayboothStatus.ForeColor = Color.FromArgb(255, 160, 80);
                string current = Spraybooth.Instance.GetCurrentVehicle();
                lblSprayboothCurrent.Text = $"🎨 Painting: {current}";
                lblSprayTime.Text = $"⏱️ Spray time: 5-7 seconds";
            }
            else
            {
                lblSprayboothStatus.Text = "● READY";
                lblSprayboothStatus.ForeColor = Color.FromArgb(80, 200, 80);
                lblSprayboothCurrent.Text = $"📦 Waiting for vehicles...";
                lblSprayTime.Text = $"⏱️ Spray time: --";
            }

            // Estimated time remaining (simple calculation)
            if (_carLine.IsBusy)
            {
                int remaining = (int)((100 - progressCar.Value) / 100f * 12);
                lblCarTime.Text = $"⏱️ Est. remaining: ~{remaining}s";
            }
            else
            {
                lblCarTime.Text = $"⏱️ Est. remaining: --";
            }

            if (_minibusLine.IsBusy)
            {
                int remaining = (int)((100 - progressBus.Value) / 100f * 15);
                lblBusTime.Text = $"⏱️ Est. remaining: ~{remaining}s";
            }
            else
            {
                lblBusTime.Text = $"⏱️ Est. remaining: --";
            }
        }
    }
}