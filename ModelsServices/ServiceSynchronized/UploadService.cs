using Models;

namespace ModelsServices.ServiceSynchronized
{
    public class UploadService
    {
        AppLocalDbContext _context;
        public UploadService()
        {
            _context = new AppLocalDbContext();
        }
    }
}
