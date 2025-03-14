using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using Models;
using MudBlazor.Services;
using Services;
using ViewModels;

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
            services.AddScoped<DepenseService>();
            services.AddScoped<UserService>();
            services.AddScoped<PrixVenteService>();
            services.AddScoped<PointVenteService>();
            services.AddScoped<TauxService>();
            services.AddScoped<CommandService>();


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

        public static SessionModel UserConnected;
    }
}
