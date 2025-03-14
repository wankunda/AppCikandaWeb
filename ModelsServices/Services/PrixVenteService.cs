using Microsoft.EntityFrameworkCore;
using Models;
using Utilities;
using ViewModels;

namespace Services
{
    public class PrixVenteService
    {
        AppLocalDbContext bdContext;
        public PrixVenteService()
        {
            bdContext = new AppLocalDbContext();
        }

        public async Task<Response> Add(PrixVenteAddModel Model)
        {
            PrixVente prixvente = new PrixVente
            {
                Code = Model.Code.ToString(),
                DateCreated = Model.DateCreated.ToShortDateString(),
                Delete = false,
                IdPointVente = Model.IdPointVente,
                Monnaie = (int)Model.Monnaie,
                Value = Model.Value,
                Active=true,
                IdArticle=Model.IdArticle,
                Synchronized = false,
            };

            try
            {
                await bdContext.PrixVentes.AddAsync(prixvente);
                await bdContext.SaveChangesAsync();

                return new Response() { Message = "Prix de vente bien sauvegardé dans la base des données !!", TypeResponse = (int)TypeResponse.Success };
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message, TypeResponse = (int)TypeResponse.Error }; ;
            }
        }

        public async Task<IEnumerable<PrixVenteViewModel>?> GetAll()
        {
            var data = await bdContext.PrixVentes
                .Include(e => e.PointVente)
                .Where(e => !e.Delete && e.Active)
                .ToListAsync();
            List<PrixVenteViewModel> list = new List<PrixVenteViewModel>();

            int num = 1;

            foreach (var i in data)
            {
                list.Add(new PrixVenteViewModel
                {
                    Id = i.Id,
                    Num = num,
                    Synchronized = i.Synchronized,
                    DateCreated = i.DateCreated,
                    LastSynchronized = i.LastSynchronized,
                    PointVente = i.PointVente.Designation,
                    Value = new Money(i.Value, i.Monnaie),
                    Article=i.Article.Designation,
                });
                num++;
            }

            return list;
        }

        public async Task<Response> Delete(int Id)
        {
            try
            {
                var prixvente = await bdContext.PrixVentes
                    .FirstOrDefaultAsync(e => e.Id == Id);
                if (prixvente != null)
                {
                    prixvente.Delete = true;
                    prixvente.Synchronized = false;
                    bdContext.PrixVentes.Update(prixvente);

                    await bdContext.SaveChangesAsync();
                    return new Response()
                    {
                        TypeResponse = (int)TypeResponse.Success,
                        Message = "Prix de vente supprimé de la base des données",
                    };
                }
                else
                    return new Response()
                    {
                        TypeResponse = (int)TypeResponse.Warning,
                        Message = $"Erreur de suppression du prix de vente",
                    };
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    TypeResponse = (int)TypeResponse.Error,
                    Message = $"Impossible de supprimer ce prix de vente. Voir {ex.Message}",
                };
            }
        }

        public async Task<PrixVenteAddModel>? Get(int id)
        {
            try
            {
                var reponse = await bdContext.PrixVentes
                    .Include(e => e.PointVente)
                    .FirstOrDefaultAsync(e => e.Id == id);

                var prixvente = new PrixVenteAddModel()
                {
                    Code = new Guid(reponse.Code),
                    DateCreated = DateTime.Parse(reponse.DateCreated),
                    DateUpdated = reponse.DateUpdated == null ? DateTime.Now : DateTime.Parse(reponse.DateUpdated),
                    LastSynchronized = reponse.LastSynchronized == null ? DateTime.Now : DateTime.Parse(reponse.LastSynchronized),
                    Id = reponse.Id,
                    IdPointVente = reponse.IdPointVente,
                    Monnaie = (Monnaie)reponse.Monnaie,
                    Value=reponse.Value,
                    IdArticle=reponse.IdArticle,
                    Synchronized = reponse.Synchronized
                };
                return prixvente;
            }
            catch
            {
                return new PrixVenteAddModel();
            }
        }

        public async Task<PrixVenteViewModel>? Open(int id)
        {
            try
            {
                var reponse = await bdContext.PrixVentes
                    .Include(e => e.PointVente)
                    .FirstOrDefaultAsync(e => e.Id == id);

                var prixvente = new PrixVenteViewModel()
                {
                    Code = reponse.Code,
                    DateCreated = reponse.DateCreated,
                    DateUpdated = reponse.DateUpdated,
                    LastSynchronized = reponse.LastSynchronized,
                    Id = reponse.Id,
                    PointVente = reponse.PointVente.Designation,
                    Article=reponse.Article.Designation,
                    Value=new Money(reponse.Value, reponse.Monnaie),
                    Synchronized = reponse.Synchronized
                };
                return prixvente;
            }
            catch
            {
                return new PrixVenteViewModel();
            }
        }

        public async Task<Response> Update(PrixVenteAddModel Model)
        {
            PrixVente prixvente = new PrixVente
            {
                Code = Model.Code.ToString(),
                DateCreated = Model.DateCreated.ToShortDateString(),
                Delete = false,
                IdPointVente = Model.IdPointVente,
                Monnaie = (int)Model.Monnaie,
                Value = Model.Value,
                Active = true,
                IdArticle = Model.IdArticle,
                Synchronized = false,
            };

            var oldprixvente = await bdContext.PrixVentes.FirstOrDefaultAsync(e => e.Id == Model.Id);

            try
            {
                if (oldprixvente != null)
                {
                    oldprixvente.Active = false;
                    bdContext.PrixVentes.Update(oldprixvente);
                }

                await bdContext.PrixVentes.AddAsync(prixvente);
                await bdContext.SaveChangesAsync();

                return new Response() { Message = "Prix de vente mis à jour dans la base des données !!", TypeResponse = (int)TypeResponse.Success };
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message, TypeResponse = (int)TypeResponse.Error }; ;
            }
        }

        public async Task<ArticleViewModel>? GetArticle(int id)
        {
            try
            {
                var reponse = await bdContext.Articles
                    .Include(e => e.Category)
                    .FirstOrDefaultAsync(e => e.Id == id);
                var article = new ArticleViewModel()
                {
                    Designation = reponse.Designation,
                    PrixAchat = new Money(reponse.PrixAchat, reponse.Monnaie),
                };
                return article;
            }
            catch
            {
                return new ArticleViewModel();
            }
        }

        public async Task<IEnumerable<PointVenteViewModel>?> GetPointVentes(int id)
        {
            try
            {
                var reponse = bdContext.PointVentes;
                List<PointVenteViewModel> pointVentes = new();
                foreach (var i in reponse)
                {
                    if (!i.Delete)
                    {
                        pointVentes.Add(new PointVenteViewModel()
                        {
                            Designation = i.Designation,
                            Id = i.Id,
                        }
                        );
                    }
                }
                return pointVentes;
            }
            catch (Exception)
            {
                return new List<PointVenteViewModel>();
            }
        }
    }
}
