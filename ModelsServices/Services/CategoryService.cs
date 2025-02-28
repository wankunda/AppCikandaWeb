using Microsoft.EntityFrameworkCore;
using Models;
using Utilities;
using ViewModels;

namespace Services
{
    public class CategoryService
    {
        AppLocalDbContext bdContext;
        public CategoryService()
        {
            bdContext = new AppLocalDbContext();
        }

        public async Task<Response> Add(CategoryAddModel Model)
        {
            Category category = new Category
            {
                Code = Model.Code.ToString(),
                DateCreated = Model.DateCreated.ToShortDateString(),
                Delete = false,
                Designation = Model.Designation,
                Synchronized = false,
            };
            try
            {
                await bdContext.Categories.AddAsync(category);
                await bdContext.SaveChangesAsync();
                return new Response()
                {
                    TypeResponse = (int)TypeResponse.Success,
                    Message = "Catégorie bien sauvegardée dans la base des données",
                };
            }
            catch (Exception ex)
            {
                {
                    return new Response()
                    {
                        TypeResponse = (int)TypeResponse.Error,
                        Message = $"Impossible d'ajouter une catégorie. Voir {ex.Message}",
                    };
                }
            }
        }

        public async Task<IEnumerable<CategoryViewModel>?> GetAll()
        {
            try
            {
                var reponse = await bdContext.Categories.ToListAsync();
                List<CategoryViewModel> categories = new();
                int id = 1;
                foreach (var i in reponse)
                {
                    if (!i.Delete)
                    {
                        categories.Add(new CategoryViewModel()
                        {
                            Code = i.Code,
                            DateCreated = i.DateCreated,
                            Designation = i.Designation,
                            Id = i.Id,
                            Num = id,
                            DateUpdated = i.DateUpdated,
                            Synchronized = i.Synchronized,
                        }
                        );
                        id++;
                    }
                }
                return categories;
            }
            catch (Exception)
            {
                return new List<CategoryViewModel>();
            }
        }

        public async Task<Response> Delete(int Id)
        {
            try
            {
                var reponse = await bdContext.Categories.FirstOrDefaultAsync(e => e.Id == Id);
                if (reponse != null)
                {
                    reponse.Delete = true;
                    reponse.Synchronized = false;
                    bdContext.Categories.Update(reponse);
                    await bdContext.SaveChangesAsync();
                    return new Response()
                    {
                        TypeResponse = (int)TypeResponse.Success,
                        Message = "Catégorie supprimée de la base des données",
                    };
                }
                else
                    return new Response()
                    {
                        TypeResponse = (int)TypeResponse.Warning,
                        Message = $"Erreur de suppression de la catégorie",
                    };
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    TypeResponse = (int)TypeResponse.Error,
                    Message = $"Impossible de supprimer cette catégorie. Voir {ex.Message}",
                };
            }
        }

        public async Task<Response> Update(CategoryAddModel Model)
        {
            Category category = new Category
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
                bdContext.Categories.Update(category);
                await bdContext.SaveChangesAsync();

                return new Response()
                {
                    TypeResponse = (int)TypeResponse.Success,
                    Message = "Catégorie mise à jour dans la base des données",
                };
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    TypeResponse = (int)TypeResponse.Error,
                    Message = $"Impossible de modifier cette catégorie. Voir {ex.Message}",
                };
            }
        }

        public async Task<CategoryViewModel>? Open(int id)
        {
            try
            {
                var reponse = await bdContext.Categories
                    .Include(e => e.Articles)
                    .FirstOrDefaultAsync(e => e.Id == id);
                var category = new CategoryViewModel()
                {
                    Code = reponse.Code,
                    DateCreated = reponse.DateCreated,
                    DateUpdated = reponse.DateUpdated,
                    LastSynchronized = reponse.LastSynchronized,
                    Designation = reponse.Designation,
                    Synchronized = reponse.Synchronized,
                    Articles = reponse.Articles.Select(e => new CategoryProduitViewModel
                    {
                        Designation = e.Designation,
                        PrixAchat = new Money(e.PrixAchat, e.Monnaie),
                        StockInitial = e.StockInitial,
                        Synchronized = e.Synchronized,
                        DateCreated = e.DateCreated

                    }).ToList()
                };
                return category;
            }
            catch
            {
                return new CategoryViewModel();
            }
        }

        public async Task<CategoryAddModel>? Get(int id)
        {
            try
            {
                var reponse = await bdContext.Categories
                    .Include(e => e.Articles)
                    .FirstOrDefaultAsync(e => e.Id == id);
                var category = new CategoryAddModel()
                {
                    Code = new Guid(reponse.Code),
                    DateCreated = DateTime.Parse(reponse.DateCreated),
                    DateUpdated = DateTime.Parse(reponse.DateUpdated),
                    LastSynchronized = DateTime.Parse(reponse.LastSynchronized),
                    Designation = reponse.Designation,
                    Synchronized = reponse.Synchronized,
                    Id = reponse.Id,
                    Delete = reponse.Delete,
                };
                return category;
            }
            catch
            {
                return new CategoryAddModel();
            }
        }
    }
}
