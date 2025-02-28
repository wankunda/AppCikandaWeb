using Microsoft.EntityFrameworkCore;
using Models;
using Utilities;
using ViewModels;

namespace Services
{
    public class AchatService
    {
        AppLocalDbContext bdContext;
        public AchatService()
        {
            bdContext = new AppLocalDbContext();
        }

        public async Task<Response> Add(AchatAddModel Model)
        {
            Achat article = new Achat
            {
                Code = Model.Code.ToString(),
                DateCreated = Model.DateCreated.ToShortDateString(),
                Delete = false,
                Designation = Model.Designation,
                Synchronized = false,
            };

            try
            {
                await bdContext.Achats.AddAsync(article);
                await bdContext.SaveChangesAsync();

                return new Response() { Message = "Achat bien sauvegardé dans la base des données !!", TypeResponse = (int)TypeResponse.Success };
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message, TypeResponse = (int)TypeResponse.Error }; ;
            }
        }

        public async Task<IEnumerable<AchatViewModel>?> GetAll()
        {
            var data = await bdContext.Achats
                .Include(e => e.Produits)
                .Where(e => !e.Delete)
                .ToListAsync();
            List<AchatViewModel> list = new List<AchatViewModel>();

            int num = 1;

            foreach (var i in data)
            {
                list.Add(new AchatViewModel
                {
                    Id = i.Id,
                    Num = num,
                    Designation = i.Designation,
                    Synchronized = i.Synchronized
                });
                num++;
            }

            return list;
        }

        public async Task<Response> Delete(int Id)
        {
            try
            {
                var achat = await bdContext.Achats
                    .FirstOrDefaultAsync(e => e.Id == Id);
                if (achat != null)
                {
                    achat.Delete = true;
                    achat.Synchronized = false;
                    bdContext.Achats.Update(achat);

                    foreach (var i in achat.Produits)
                    {
                        i.Delete = true;
                        i.Synchronized = false;
                        bdContext.ProduitAchats.Update(i);
                    }

                    await bdContext.SaveChangesAsync();
                    return new Response()
                    {
                        TypeResponse = (int)TypeResponse.Success,
                        Message = "Achat supprimé de la base des données",
                    };
                }
                else
                    return new Response()
                    {
                        TypeResponse = (int)TypeResponse.Warning,
                        Message = $"Erreur de suppression de l'achat",
                    };
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    TypeResponse = (int)TypeResponse.Error,
                    Message = $"Impossible de supprimer cet achat. Voir {ex.Message}",
                };
            }
        }

        public async Task<AchatAddModel>? Get(int id)
        {
            try
            {
                var reponse = await bdContext.Articles
                    .Include(e => e.Category)
                    .Include(e => e.PrixVentes)
                    .ThenInclude(e => e.PointVente)
                    .FirstOrDefaultAsync(e => e.Id == id);

                var achat = new AchatAddModel()
                {
                    Code = new Guid(reponse.Code),
                    DateCreated = DateTime.Parse(reponse.DateCreated),
                    DateUpdated = DateTime.Parse(reponse.DateUpdated),
                    LastSynchronized = DateTime.Parse(reponse.LastSynchronized),
                    Designation = reponse.Designation,
                    Synchronized = reponse.Synchronized
                };
                return achat;
            }
            catch
            {
                return new AchatAddModel();
            }
        }

        public async Task<AchatViewModel>? Open(int id)
        {
            try
            {
                var reponse = await bdContext.Achats
                    .Include(e => e.Produits)
                    .FirstOrDefaultAsync(e => e.Id == id);

                var article = new AchatViewModel()
                {
                    Code = reponse.Code,
                    DateCreated = reponse.DateCreated,
                    DateUpdated = reponse.DateUpdated,
                    LastSynchronized = reponse.LastSynchronized,
                    Designation = reponse.Designation,
                    Synchronized = reponse.Synchronized,
                    Produits = reponse.Produits.Select(e => new ProduitAchatViewModel
                    {
                        Article=e.Article.Designation,
                        CoutAchat=new Money(e.CoutAchat, e.Monnaie),
                        Quantite=e.Quantite,
                        ValeurTotale=new Money((e.CoutAchat*e.Quantite), e.Monnaie),
                    }).ToList()
                };
                return article;
            }
            catch
            {
                return new AchatViewModel();
            }
        }

        public async Task<Response> Update(AchatAddModel Model)
        {
            Achat achat = new Achat
            {
                Code = Model.Code.ToString(),
                DateCreated = Model.DateCreated.ToShortDateString(),
                DateUpdated = Model.DateUpdated.ToShortDateString(),
                LastSynchronized = Model.LastSynchronized.ToShortDateString(),
                Delete = Model.Delete,
                Designation = Model.Designation,
                Synchronized = Model.Synchronized,
                Id = Model.Id,
            };

            try
            {
                bdContext.Achats.Update(achat);
                await bdContext.SaveChangesAsync();

                return new Response() { Message = "Achat mis à jour dans la base des données !!", TypeResponse = (int)TypeResponse.Success };
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message, TypeResponse = (int)TypeResponse.Error }; ;
            }
        }
    }
}
