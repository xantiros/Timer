using System;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Forms;
using System.Reflection;

namespace Timer
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DateTime new_date = new DateTime();

        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            new_date = DateTime.Now.AddSeconds(12);

            //try icon
            var notifyIcon = new NotifyIcon
            {
                Visible = true,
                Icon = System.Drawing.Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location),
                Text = Title
            };
            this.Hide();

            notifyIcon.ShowBalloonTip(1, "Hello World", "Description message", System.Windows.Forms.ToolTipIcon.Info);
            notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // Updating the Label which displays the current second
            //lbl_timer.Content = DateTime.Now;
            //lbl_timer.Content = DateTime.Now.ToString("yyyy-MM-dd hh.mm.ss");
            var date = new_date - DateTime.Now;
            if (new_date <= DateTime.Now)
                lbl_timer.Content = "Booom!";
            else
                lbl_timer.Content = date.ToString(@"hh\:mm\:ss");

            // Forcing the CommandManager to raise the RequerySuggested event
            //CommandManager.InvalidateRequerySuggested();
        }

        private void notifyIcon1_DoubleClick(object Sender, EventArgs e)
        {
            // Show the form when the user double clicks on the notify icon.

            // Set the WindowState to normal if the form is minimized.
            if (this.WindowState == WindowState.Minimized)
            {
                this.WindowState = WindowState.Normal;
                // Activate the form.
                this.Activate();
                this.Show();
            }
            else if(this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Minimized;
                this.Hide();
            }
        }
        protected override void OnStateChanged(EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
                this.Hide();

            base.OnStateChanged(e);
        }
    }
}
