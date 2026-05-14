using System;
using System.Drawing;
using System.Linq;
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

        // UI Controls
        private Button btnCarBlack, btnCarWhite;
        private Button btnBusBlack, btnBusWhite;
        private Label lblCarStatus, lblCarTask, lblCarQueue;
        private Label lblBusStatus, lblBusTask, lblBusQueue;
        private Label lblSprayStatus, lblSprayVehicle;
        private ListBox lstOrders;
        private GroupBox grpCar, grpBus, grpSpray, grpHistory;

        public MainForm()
        {
            InitializeComponent();

            _carLine = new CarAssemblyLine();
            _minibusLine = new MinibusAssemblyLine();
            _hq = new HQ(_carLine, _minibusLine);

            // Start background processing
            Task.Run(() => _carLine.ProcessQueueAsync());
            Task.Run(() => _minibusLine.ProcessQueueAsync());

            // Timer for status updates
            _statusTimer = new Timer();
            _statusTimer.Interval = 500;
            _statusTimer.Tick += UpdateStatus;
            _statusTimer.Start();
        }

        private void InitializeComponent()
        {
            this.Text = "Cool Rides Production System";
            this.Size = new Size(850, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.LightGray;

            // Car Group
            grpCar = new GroupBox()
            {
                Text = "Car Assembly Line - LUX1000",
                Location = new Point(15, 15),
                Size = new Size(390, 170),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            btnCarBlack = new Button()
            {
                Text = "Order Black",
                Location = new Point(15, 35),
                Size = new Size(100, 40),
                BackColor = Color.Black,
                ForeColor = Color.White
            };
            btnCarBlack.Click += (s, e) => _hq.OrderCar("Black");

            btnCarWhite = new Button()
            {
                Text = "Order White",
                Location = new Point(125, 35),
                Size = new Size(100, 40),
                BackColor = Color.White,
                ForeColor = Color.Black
            };
            btnCarWhite.Click += (s, e) => _hq.OrderCar("White");

            lblCarStatus = new Label()
            {
                Text = "Status: Idle",
                Location = new Point(15, 90),
                Size = new Size(150, 25)
            };

            lblCarTask = new Label()
            {
                Text = "Current: None",
                Location = new Point(15, 115),
                Size = new Size(350, 25)
            };

            lblCarQueue = new Label()
            {
                Text = "Queue: 0",
                Location = new Point(15, 140),
                Size = new Size(150, 25)
            };

            grpCar.Controls.AddRange(new Control[] { btnCarBlack, btnCarWhite, lblCarStatus, lblCarTask, lblCarQueue });

            // Bus Group
            grpBus = new GroupBox()
            {
                Text = "Minibus Assembly Line - MV500",
                Location = new Point(420, 15),
                Size = new Size(390, 170),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            btnBusBlack = new Button()
            {
                Text = "Order Black",
                Location = new Point(15, 35),
                Size = new Size(100, 40),
                BackColor = Color.Black,
                ForeColor = Color.White
            };
            btnBusBlack.Click += (s, e) => _hq.OrderMinibus("Black");

            btnBusWhite = new Button()
            {
                Text = "Order White",
                Location = new Point(125, 35),
                Size = new Size(100, 40),
                BackColor = Color.White,
                ForeColor = Color.Black
            };
            btnBusWhite.Click += (s, e) => _hq.OrderMinibus("White");

            lblBusStatus = new Label()
            {
                Text = "Status: Idle",
                Location = new Point(15, 90),
                Size = new Size(150, 25)
            };

            lblBusTask = new Label()
            {
                Text = "Current: None",
                Location = new Point(15, 115),
                Size = new Size(350, 25)
            };

            lblBusQueue = new Label()
            {
                Text = "Queue: 0",
                Location = new Point(15, 140),
                Size = new Size(150, 25)
            };

            grpBus.Controls.AddRange(new Control[] { btnBusBlack, btnBusWhite, lblBusStatus, lblBusTask, lblBusQueue });

            // Spraybooth Group
            grpSpray = new GroupBox()
            {
                Text = "Spraybooth",
                Location = new Point(15, 200),
                Size = new Size(795, 80),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            lblSprayStatus = new Label()
            {
                Text = "Status: Ready",
                Location = new Point(15, 35),
                Size = new Size(150, 30),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            lblSprayVehicle = new Label()
            {
                Text = "Currently Spraying: None",
                Location = new Point(200, 35),
                Size = new Size(350, 30)
            };

            grpSpray.Controls.AddRange(new Control[] { lblSprayStatus, lblSprayVehicle });

            // Order History
            grpHistory = new GroupBox()
            {
                Text = "Order History & Activity Log",
                Location = new Point(15, 295),
                Size = new Size(795, 300),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            lstOrders = new ListBox()
            {
                Location = new Point(10, 25),
                Size = new Size(770, 260),
                Font = new Font("Consolas", 9)
            };

            grpHistory.Controls.Add(lstOrders);

            // Add all to form
            this.Controls.AddRange(new Control[] { grpCar, grpBus, grpSpray, grpHistory });
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
                lblCarStatus.Text = "Status: Busy";
                lblCarStatus.ForeColor = Color.Red;
            }
            else
            {
                lblCarStatus.Text = "Status: Idle";
                lblCarStatus.ForeColor = Color.Green;
            }

            // Get current task - this will show things like "Build Car (Black)" from your command
            string currentTask = _carLine.GetCurrentTaskDescription();
            lblCarTask.Text = $"Current: {currentTask}";
            lblCarQueue.Text = $"Queue: {_carLine.GetQueueCount()}";

            // Bus status
            if (_minibusLine.IsBusy)
            {
                lblBusStatus.Text = "Status: Busy";
                lblBusStatus.ForeColor = Color.Red;
            }
            else
            {
                lblBusStatus.Text = "Status: Idle";
                lblBusStatus.ForeColor = Color.Green;
            }

            string currentBusTask = _minibusLine.GetCurrentTaskDescription();
            lblBusTask.Text = $"Current: {currentBusTask}";
            lblBusQueue.Text = $"Queue: {_minibusLine.GetQueueCount()}";

            // Spraybooth status
            if (Spraybooth.Instance.IsSpraying)
            {
                lblSprayStatus.Text = "Status: Spraying";
                lblSprayStatus.ForeColor = Color.Orange;
            }
            else
            {
                lblSprayStatus.Text = "Status: Ready";
                lblSprayStatus.ForeColor = Color.Green;
            }

            lblSprayVehicle.Text = $"Currently Spraying: {Spraybooth.Instance.GetCurrentVehicle()}";

            // Update order history display
            var history = _hq.GetOrderHistory();
            lstOrders.Items.Clear();

            int maxItems = Math.Min(20, history.Count);
            for (int i = 0; i < maxItems; i++)
            {
                lstOrders.Items.Add(history[i]);
            }
        }
    }
}