using Models;

namespace ModelsServices.ServiceSynchronized
{
    public class DownloadService
    {
        AppLocalDbContext _context;
        public DownloadService()
        {
            _context = new AppLocalDbContext();
        }
    }
}
