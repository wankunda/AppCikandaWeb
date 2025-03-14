using Microsoft.EntityFrameworkCore;
using Models;
using Utilities;
using ViewModels;

namespace Services
{
    public class ArticleService
    {
        AppLocalDbContext bdContext;
        public ArticleService()
        {
            bdContext = new AppLocalDbContext();
        }

        public async Task<Response> Add(ArticleAddModel Model)
        {
            Article article = new Article
            {
                IdCategory = Model.IdCategory,
                Code = Model.Code.ToString(),
                DateCreated = Model.DateCreated.ToShortDateString(),
                Delete = false,
                Image = Model.Image,
                Designation = Model.Designation,
                Monnaie = (int)Model.Monnaie,
                PrixAchat = Model.PrixAchat,
                StockInitial = Model.StockInitial,
                StockSecurite = Model.StockSecurite,
                Synchronized = false
            };

            try
            {
                await bdContext.Articles.AddAsync(article);
                await bdContext.SaveChangesAsync();

                return new Response() { Message = "Article bien sauvegardé dans la base des données !!", TypeResponse = (int)TypeResponse.Success };
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message, TypeResponse = (int)TypeResponse.Error }; ;
            }
        }

        public async Task<IEnumerable<ArticleViewModel>?> GetAll()
        {
            var data = await bdContext.Articles
                .Include(e => e.Category)
                .OrderBy(e => e.Category.Designation)
                .ThenBy(e => e.Designation)
                .Where(e => !e.Delete)
                .ToListAsync();
            List<ArticleViewModel> list = new List<ArticleViewModel>();

            int num = 1;

            foreach (var i in data)
            {
                list.Add(new ArticleViewModel
                {
                    Id = i.Id,
                    Num = num,
                    Image = i.Image,
                    Designation = i.Designation,
                    Category = i.Category.Designation,
                    PrixAchat = new Money(i.PrixAchat, i.Monnaie),
                    StockInitial = i.StockInitial,
                    StockSecurite = i.StockSecurite,
                    Synchronized = i.Synchronized
                });
                num++;
            }

            return list;
        }

        public async Task<IEnumerable<CategoryViewModel>?> GetCategories()
        {
            var data = await bdContext.Categories
                .Where(e => !e.Delete)
                .ToListAsync();

            List<CategoryViewModel> list = new List<CategoryViewModel>();
            foreach (var i in data)
            {
                list.Add(new CategoryViewModel
                {
                    Id = i.Id,
                    Designation = i.Designation,

                });
            }
            return list;
        }

        public async Task<Response> Delete(int Id)
        {
            try
            {
                var article = await bdContext.Articles
                    .FirstOrDefaultAsync(e => e.Id == Id);
                if (article != null)
                {
                    article.Delete = true;
                    article.Synchronized = false;
                    bdContext.Articles.Update(article);
                    await bdContext.SaveChangesAsync();
                    return new Response()
                    {
                        TypeResponse = (int)TypeResponse.Success,
                        Message = "Article supprimé de la base des données",
                    };
                }
                else
                    return new Response()
                    {
                        TypeResponse = (int)TypeResponse.Warning,
                        Message = $"Erreur de suppression de l'article",
                    };
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    TypeResponse = (int)TypeResponse.Error,
                    Message = $"Impossible de supprimer cet article. Voir {ex.Message}",
                };
            }
        }

        public async Task<ArticleAddModel>? Get(int id)
        {
            try
            {
                var reponse = await bdContext.Articles
                    .Include(e => e.Category)
                    .FirstOrDefaultAsync(e => e.Id == id);
                var article = new ArticleAddModel()
                {
                    Code = new Guid(reponse.Code),
                    DateCreated = DateTime.Parse(reponse.DateCreated),
                    DateUpdated = reponse.DateUpdated == null ? DateTime.Now : DateTime.Parse(reponse.DateUpdated),
                    LastSynchronized = reponse.LastSynchronized == null ? DateTime.Now : DateTime.Parse(reponse.LastSynchronized),
                    Designation = reponse.Designation,
                    StockInitial = reponse.StockInitial,
                    PrixAchat = reponse.PrixAchat,
                    Category = reponse.Category.Designation,
                    Monnaie = (Monnaie)reponse.Monnaie,
                    Synchronized = reponse.Synchronized,
                };
                return article;
            }
            catch
            {
                return new ArticleAddModel();
            }
        }

        public async Task<ArticleViewModel>? Open(int id)
        {
            try
            {
                var reponse = await bdContext.Articles
                    .Include(e => e.Category)
                    .Include(e => e.PrixVentes)
                    .ThenInclude(e => e.PointVente)
                    .FirstOrDefaultAsync(e => e.Id == id);
                var article = new ArticleViewModel()
                {
                    Code = reponse.Code,
                    DateCreated = reponse.DateCreated,
                    DateUpdated = reponse.DateUpdated,
                    LastSynchronized = reponse.LastSynchronized,
                    Designation = reponse.Designation,
                    Image = reponse.Image,
                    StockSecurite = reponse.StockSecurite,
                    StockInitial = reponse.StockInitial,
                    PrixAchat = new Money(reponse.PrixAchat, reponse.Monnaie),
                    Synchronized = reponse.Synchronized,
                    PrixVentes = reponse.PrixVentes.Select(e => new PrixVenteArticleViewModel
                    {
                        PointVente = e.PointVente.Designation,
                        Active = e.Active,
                        PrixVente = new Money(e.Value, e.Monnaie)

                    }).ToList()
                };
                return article;
            }
            catch
            {
                return new ArticleViewModel();
            }
        }

        public async Task<Response> Update(ArticleAddModel Model)
        {
            Article article = new Article
            {
                IdCategory = Model.IdCategory,
                Code = Model.Code.ToString(),
                DateCreated = Model.DateCreated.ToShortDateString(),
                DateUpdated=Model.DateUpdated.ToShortDateString(),
                LastSynchronized=Model.LastSynchronized.ToShortDateString(),
                Delete = Model.Delete,
                Id = Model.Id,
                Image= Model.Image,
                StockSecurite = Model.StockSecurite,
                Designation = Model.Designation,
                Monnaie = (int)Model.Monnaie,
                PrixAchat = Model.PrixAchat,
                StockInitial = Model.StockInitial,
                Synchronized = Model.Synchronized
            };

            try
            {
                bdContext.Articles.Update(article);
                await bdContext.SaveChangesAsync();

                return new Response() { Message = "Article mis à jour dans la base des données !!", TypeResponse = (int)TypeResponse.Success };
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message, TypeResponse = (int)TypeResponse.Error }; ;
            }
        }
    }
}
