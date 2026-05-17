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

        private Button btnCarBlack, btnCarWhite;
        private Button btnBusBlack, btnBusWhite;

        private Label lblCarStatus, lblCarTask, lblCarQueue, lblCarCompleted;
        private Label lblBusStatus, lblBusTask, lblBusQueue, lblBusCompleted;
        private Label lblSprayStatus, lblSprayVehicle;
        private Label lblMembers;

        private ListBox lstOrders;

        private GroupBox grpCar, grpBus, grpSpray, grpHistory, grpMembers;

        public MainForm()
        {
            InitializeComponent();

            _carLine = new CarAssemblyLine();
            _minibusLine = new MinibusAssemblyLine();
            _hq = new HQ(_carLine, _minibusLine);

            Task.Run(() => _carLine.ProcessQueueAsync());
            Task.Run(() => _minibusLine.ProcessQueueAsync());

            _statusTimer = new Timer();
            _statusTimer.Interval = 500;
            _statusTimer.Tick += UpdateStatus;
            _statusTimer.Start();
        }

        private void InitializeComponent()
        {
            this.Text = "Cool Rides Production System";
            this.Size = new Size(900, 860);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.LightGray;

            grpCar = new GroupBox()
            {
                Text = "Car Assembly Line - LUX1000",
                Location = new Point(15, 15),
                Size = new Size(410, 190),
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
            btnCarBlack.Click += (s, e) => _hq.OrderVehicle("LUX1000", "Black");

            btnCarWhite = new Button()
            {
                Text = "Order White",
                Location = new Point(125, 35),
                Size = new Size(100, 40),
                BackColor = Color.White,
                ForeColor = Color.Black
            };
            btnCarWhite.Click += (s, e) => _hq.OrderVehicle("LUX1000", "White");

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
                Size = new Size(380, 25)
            };

            lblCarQueue = new Label()
            {
                Text = "Queue: 0",
                Location = new Point(15, 140),
                Size = new Size(150, 25)
            };

            lblCarCompleted = new Label()
            {
                Text = "Completed: 0",
                Location = new Point(15, 165),
                Size = new Size(150, 25)
            };

            grpCar.Controls.AddRange(new Control[]
            {
                btnCarBlack,
                btnCarWhite,
                lblCarStatus,
                lblCarTask,
                lblCarQueue,
                lblCarCompleted
            });

            grpBus = new GroupBox()
            {
                Text = "Minibus Assembly Line - MV500",
                Location = new Point(440, 15),
                Size = new Size(410, 190),
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
            btnBusBlack.Click += (s, e) => _hq.OrderVehicle("MV500", "Black");

            btnBusWhite = new Button()
            {
                Text = "Order White",
                Location = new Point(125, 35),
                Size = new Size(100, 40),
                BackColor = Color.White,
                ForeColor = Color.Black
            };
            btnBusWhite.Click += (s, e) => _hq.OrderVehicle("MV500", "White");

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
                Size = new Size(380, 25)
            };

            lblBusQueue = new Label()
            {
                Text = "Queue: 0",
                Location = new Point(15, 140),
                Size = new Size(150, 25)
            };

            lblBusCompleted = new Label()
            {
                Text = "Completed: 0",
                Location = new Point(15, 165),
                Size = new Size(150, 25)
            };

            grpBus.Controls.AddRange(new Control[]
            {
                btnBusBlack,
                btnBusWhite,
                lblBusStatus,
                lblBusTask,
                lblBusQueue,
                lblBusCompleted
            });

            grpSpray = new GroupBox()
            {
                Text = "Spraybooth",
                Location = new Point(15, 220),
                Size = new Size(835, 80),
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
                Size = new Size(400, 30)
            };

            grpSpray.Controls.AddRange(new Control[]
            {
                lblSprayStatus,
                lblSprayVehicle
            });

            grpHistory = new GroupBox()
            {
                Text = "Order History & Activity Log",
                Location = new Point(15, 315),
                Size = new Size(835, 250),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            lstOrders = new ListBox()
            {
                Location = new Point(10, 25),
                Size = new Size(810, 210),
                Font = new Font("Consolas", 9)
            };

            grpHistory.Controls.Add(lstOrders);

            grpMembers = new GroupBox()
            {
                Text = "Group Members",
                Location = new Point(15, 580),
                Size = new Size(835, 140),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            lblMembers = new Label()
            {
                Location = new Point(15, 25),
                Size = new Size(780, 100),
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                AutoSize = false,
                Text =
                    "• Motheo Kekana: 229237754\r\n" +
                    "• Cohen Geswint: 229785689\r\n" +
                    "• Aiden Engelbrecht: 229234720\r\n" +
                    "• Malik Ndayisaba: 229818994 \r\n"
            };

            grpMembers.Controls.Add(lblMembers);

            this.Controls.AddRange(new Control[]
            {
                grpCar,
                grpBus,
                grpSpray,
                grpHistory,
                grpMembers
            });
        }

        private void UpdateStatus(object sender, EventArgs e)
        {
            lblCarStatus.Text = _carLine.IsBusy ? "Status: Busy" : "Status: Idle";
            lblCarStatus.ForeColor = _carLine.IsBusy ? Color.Red : Color.Green;
            lblCarTask.Text = $"Current: {_carLine.GetCurrentTaskDescription()}";
            lblCarQueue.Text = $"Queue: {_carLine.GetQueueCount()}";
            lblCarCompleted.Text = $"Completed: {_carLine.GetCompletedCount()}";

            lblBusStatus.Text = _minibusLine.IsBusy ? "Status: Busy" : "Status: Idle";
            lblBusStatus.ForeColor = _minibusLine.IsBusy ? Color.Red : Color.Green;
            lblBusTask.Text = $"Current: {_minibusLine.GetCurrentTaskDescription()}";
            lblBusQueue.Text = $"Queue: {_minibusLine.GetQueueCount()}";
            lblBusCompleted.Text = $"Completed: {_minibusLine.GetCompletedCount()}";

            lblSprayStatus.Text = Spraybooth.Instance.IsSpraying ? "Status: Spraying" : "Status: Ready";
            lblSprayStatus.ForeColor = Spraybooth.Instance.IsSpraying ? Color.Orange : Color.Green;
            lblSprayVehicle.Text = $"Currently Spraying: {Spraybooth.Instance.GetCurrentVehicle()}";

            lstOrders.Items.Clear();

            var history = _hq.GetOrderHistory();

            foreach (var item in history)
            {
                lstOrders.Items.Add(item);
            }
        }
    }
}