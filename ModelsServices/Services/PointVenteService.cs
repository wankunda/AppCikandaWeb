using Microsoft.EntityFrameworkCore;
using Models;
using Utilities;
using ViewModels;

namespace Services
{
    public class PointVenteService
    {
        AppLocalDbContext bdContext;
        public PointVenteService()
        {
            bdContext = new AppLocalDbContext();
        }

        public async Task<Response> Add(PointVenteAddModel Model)
        {
            PointVente pointVente = new PointVente
            {
                Code = Model.Code.ToString(),
                DateCreated = Model.DateCreated.ToShortDateString(),
                Delete = false,
                Designation = Model.Designation,
                Synchronized = false,
            };
            try
            {
                var reponse = await bdContext.PointVentes.AddAsync(pointVente);
                await bdContext.SaveChangesAsync();
                return new Response()
                {
                    TypeResponse = (int)TypeResponse.Success,
                    Message = "Point de vente bien sauvegardé dans la base des données",
                };
            }
            catch (Exception ex)
            {
                {
                    return new Response()
                    {
                        TypeResponse = (int)TypeResponse.Error,
                        Message = $"Impossible d'ajouter un point de vente. Voir {ex.Message}",
                    };
                }
            }
        }

        public async Task<IEnumerable<PointVenteViewModel>?> GetAll()
        {
            try
            {
                var reponse = await bdContext.PointVentes.ToListAsync();
                List<PointVenteViewModel> pointVentes = new();
                int id = 1;
                foreach (var i in reponse)
                {
                    if (!i.Delete)
                    {
                        pointVentes.Add(new PointVenteViewModel()
                        {
                            Code = i.Code,
                            DateCreated = i.DateCreated,
                            Designation = i.Designation,
                            Id = i.Id,
                            Num = id,
                            DateUpdated = i.DateUpdated,
                            Synchronized = i.Synchronized,
                            LastSynchronized = i.LastSynchronized,
                        }
                        );
                        id++;
                    }
                }
                return pointVentes;
            }
            catch (Exception)
            {
                return new List<PointVenteViewModel>();
            }
        }

        public async Task<Response> Delete(int Id)
        {
            try
            {
                var reponse = await bdContext.PointVentes.FirstOrDefaultAsync(e => e.Id == Id);
                if (reponse != null)
                {
                    reponse.Delete = true;
                    reponse.Synchronized = false;
                    bdContext.PointVentes.Update(reponse);
                    await bdContext.SaveChangesAsync();
                    return new Response()
                    {
                        TypeResponse = (int)TypeResponse.Success,
                        Message = "Point de vente supprimé de la base des données",
                    };
                }
                else
                    return new Response()
                    {
                        TypeResponse = (int)TypeResponse.Warning,
                        Message = $"Erreur de suppression du point de vente",
                    };
            }
            catch (Exception ex)
            {
                {
                    return new Response()
                    {
                        TypeResponse = (int)TypeResponse.Error,
                        Message = $"Impossible de supprimer ce point de vente. Voir {ex.Message}",
                    };
                }
            }
        }

        public async Task<Response> Update(PointVenteAddModel Model)
        {
            PointVente pointVente = new PointVente
            {
                Code = Model.Code.ToString(),
                DateCreated = Model.DateCreated.ToShortDateString(),
                DateUpdated = Model.DateUpdated.ToShortDateString(),
                LastSynchronized = Model.LastSynchronized.ToShortDateString(),
                Delete = Model.Delete,
                Designation = Model.Designation,
                Id = Model.Id,
                Synchronized = Model.Synchronized,
            };
            try
            {
                bdContext.PointVentes.Update(pointVente);
                await bdContext.SaveChangesAsync();

                return new Response()
                {
                    TypeResponse = (int)TypeResponse.Success,
                    Message = "Point de vente mis à jour dans la base des données",
                };
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    TypeResponse = (int)TypeResponse.Error,
                    Message = $"Impossible de modifier ce point de vente. Voir {ex.Message}",
                };
            }
        }

        public async Task<PointVenteViewModel>? Open(int id)
        {
            try
            {
                var reponse = await bdContext.PointVentes
                    .Include(e => e.PrixVentes)
                    .FirstOrDefaultAsync(e => e.Id == id);
                var pointVente = new PointVenteViewModel()
                {
                    Code = reponse.Code,
                    DateCreated = reponse.DateCreated,
                    DateUpdated = reponse.DateUpdated,
                    LastSynchronized = reponse.LastSynchronized,
                    Designation = reponse.Designation,
                    Synchronized = reponse.Synchronized,
                    PrixVentes = reponse.PrixVentes.Where(e => e.Active).Select(e => new PrixPointVenteViewModel
                    {
                        Synchronized = e.Synchronized,
                        DateCreated = e.DateCreated,
                        Article = e.Article.Designation,
                        PrixVente = new Money(e.Value, e.Monnaie),
                        LastSynchronized = e.LastSynchronized,
                    }).ToList()
                };
                return pointVente;
            }
            catch
            {
                return new PointVenteViewModel();
            }
        }

        public async Task<PointVenteAddModel>? Get(int id)
        {
            try
            {
                var reponse = await bdContext.PointVentes
                    .Include(e => e.PrixVentes)
                    .FirstOrDefaultAsync(e => e.Id == id);
                var pointVente = new PointVenteAddModel()
                {
                    Code = new Guid(reponse.Code),
                    DateCreated = DateTime.Parse(reponse.DateCreated),
                    DateUpdated = reponse.DateUpdated == null ? DateTime.Now : DateTime.Parse(reponse.DateUpdated),
                    LastSynchronized = reponse.LastSynchronized == null ? DateTime.Now : DateTime.Parse(reponse.LastSynchronized),
                    Designation = reponse.Designation,
                    Synchronized = reponse.Synchronized,
                    Id = reponse.Id,
                    Delete = reponse.Delete
                };
                return pointVente;
            }
            catch
            {
                return new PointVenteAddModel();
            }
        }
    }
}
