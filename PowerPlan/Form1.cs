using PowerPlan.Services;
using System.Diagnostics;

namespace PowerPlan
{
    public partial class Form1 : Form
    {
        private readonly PowerManagementService _powerManagementService;
        public Form1()
        {
            InitializeComponent();
            _powerManagementService = new PowerManagementService();
            Load += FormLoad;
            Resize += FormResize;
            notifyIcon.MouseDoubleClick += NotifyIconMouseDoubleClick;
        }

        private void FormLoad(object? sender, EventArgs e)
        {
            Debug.WriteLine("This is a message in the Output window.");

            string currentPlan = _powerManagementService.GetCurrentPlan();
            Debug.WriteLine("Current plan:" + currentPlan);
        }

        private void FormResize(object? sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                ShowInTaskbar = false;
                Hide();
                notifyIcon.Icon = new Icon("powerplan.ico");
                notifyIcon.Visible = true;
            }
        }

        private void NotifyIconMouseDoubleClick(object? sender, MouseEventArgs e)
        {
            ShowInTaskbar = true;
            notifyIcon.Visible = false;
            WindowState = FormWindowState.Normal;
        }
    }
}
