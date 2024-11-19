namespace Alert
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("Alert Details", typeof(AlertDetailPage));
        }
    }
}
