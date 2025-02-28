using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Services;
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

            //Inscription des services
            services.AddScoped<CategoryService>();
            services.AddScoped<ArticleService>();


            blazorWebView1.HostPage = "wwwroot\\index.html";
            blazorWebView1.Dock = DockStyle.Fill;
            blazorWebView1.Services = services.BuildServiceProvider();
            blazorWebView1.RootComponents.Add<App>("#app");

            blazorWebView1.Services = services.BuildServiceProvider();
        }

        private AppLocalDbContext? dbContext;
        private void MainForm_Load(object sender, EventArgs e)
        {
            dbContext = new AppLocalDbContext();
            dbContext.Database.EnsureCreated();
        }
    }
}
