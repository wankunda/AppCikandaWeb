using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.Metrics;
using MudBlazor.Services;

namespace AppCikanda
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            var services = new ServiceCollection();
            services.AddWindowsFormsBlazorWebView();
            services.AddMudServices();


            blazorWebView1.HostPage = "wwwroot\\index.html";
            blazorWebView1.Dock = DockStyle.Fill;
            blazorWebView1.Services = services.BuildServiceProvider();
            blazorWebView1.RootComponents.Add<MainLayout>("#app");
        }
    }
}
