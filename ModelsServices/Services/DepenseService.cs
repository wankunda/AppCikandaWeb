using Microsoft.EntityFrameworkCore;
using Models;
using Utilities;
using ViewModels;

namespace Services
{
    public class DepenseService
    {
        AppLocalDbContext bdContext;
        public DepenseService()
        {
            bdContext = new AppLocalDbContext();
        }

        public async Task<Response> Add(DepenseAddModel Model)
        {
            Depense depense = new Depense
            {
                Code = Model.Code.ToString(),
                DateCreated = Model.DateCreated.ToShortDateString(),
                Delete = false,
                IdPointVente = Model.IdPointVente,
                Beneficiaire = Model.Beneficiaire,
                Monnaie = (int)Model.Monnaie,
                Montant = Model.Montant,
                Motif = Model.Motif,
                Synchronized = false,
            };

            try
            {
                await bdContext.Depenses.AddAsync(depense);
                await bdContext.SaveChangesAsync();

                return new Response() { Message = "Dépense bien sauvegardée dans la base des données !!", TypeResponse = (int)TypeResponse.Success };
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message, TypeResponse = (int)TypeResponse.Error }; ;
            }
        }

        public async Task<IEnumerable<DepenseViewModel>?> GetAll()
        {
            var data = await bdContext.Depenses
                .Include(e=>e.PointVente)
                .Where(e => !e.Delete)
                .ToListAsync();
            List<DepenseViewModel> list = new List<DepenseViewModel>();

            int num = 1;

            foreach (var i in data)
            {
                list.Add(new DepenseViewModel
                {
                    Id = i.Id,
                    Num = num,
                    Synchronized = i.Synchronized,
                    Beneficiaire = i.Beneficiaire,
                    DateCreated = i.DateCreated,
                    LastSynchronized = i.LastSynchronized,
                    PointVente = i.PointVente.Designation,
                    Montant=new Money(i.Montant, i.Monnaie),
                    Motif=i.Motif,
                });
                num++;
            }

            return list;
        }

        public async Task<Response> Delete(int Id)
        {
            try
            {
                var depense = await bdContext.Depenses
                    .FirstOrDefaultAsync(e => e.Id == Id);
                if (depense != null)
                {
                    depense.Delete = true;
                    depense.Synchronized = false;
                    bdContext.Depenses.Update(depense);

                    await bdContext.SaveChangesAsync();
                    return new Response()
                    {
                        TypeResponse = (int)TypeResponse.Success,
                        Message = "Dépense supprimée de la base des données",
                    };
                }
                else
                    return new Response()
                    {
                        TypeResponse = (int)TypeResponse.Warning,
                        Message = $"Erreur de suppression de la dépense",
                    };
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    TypeResponse = (int)TypeResponse.Error,
                    Message = $"Impossible de supprimer cette dépense. Voir {ex.Message}",
                };
            }
        }

        public async Task<DepenseAddModel>? Get(int id)
        {
            try
            {
                var reponse = await bdContext.Depenses
                    .Include(e => e.PointVente)
                    .FirstOrDefaultAsync(e => e.Id == id);

                var depense = new DepenseAddModel()
                {
                    Code = new Guid(reponse.Code),
                    DateCreated = DateTime.Parse(reponse.DateCreated),
                    DateUpdated = DateTime.Parse(reponse.DateUpdated),
                    LastSynchronized = DateTime.Parse(reponse.LastSynchronized),
                    Beneficiaire = reponse.Beneficiaire,
                    Id = reponse.Id,
                    IdPointVente = reponse.IdPointVente,
                    Monnaie = (Monnaie)reponse.Monnaie,
                    Motif = reponse.Motif,
                    Montant = reponse.Montant,
                    Synchronized = reponse.Synchronized
                };
                return depense;
            }
            catch
            {
                return new DepenseAddModel();
            }
        }

        public async Task<DepenseViewModel>? Open(int id)
        {
            try
            {
                var reponse = await bdContext.Depenses
                    .Include(e => e.PointVente)
                    .FirstOrDefaultAsync(e => e.Id == id);

                var depense = new DepenseViewModel()
                {
                    Code = reponse.Code,
                    DateCreated = reponse.DateCreated,
                    DateUpdated = reponse.DateUpdated,
                    LastSynchronized = reponse.LastSynchronized,
                    Beneficiaire = reponse.Beneficiaire,
                    Id = reponse.Id,
                    PointVente = reponse.PointVente.Designation,
                    Motif = reponse.Motif,
                    Montant = new Money(reponse.Monnaie, reponse.Monnaie),
                    Synchronized = reponse.Synchronized
                };
                return depense;
            }
            catch
            {
                return new DepenseViewModel();
            }
        }

        public async Task<Response> Update(DepenseAddModel Model)
        {
            Depense depense = new Depense
            {
                Code = Model.Code.ToString(),
                DateCreated = Model.DateCreated.ToShortDateString(),
                Delete = false,
                IdPointVente = Model.IdPointVente,
                Beneficiaire = Model.Beneficiaire,
                Monnaie = (int)Model.Monnaie,
                Montant = Model.Montant,
                Motif = Model.Motif,
                Synchronized = false,
                DateUpdated = Model.DateUpdated.ToShortDateString(),
                LastSynchronized = Model.LastSynchronized.ToShortDateString(),
                Id = Model.Id,
            };

            try
            {
                bdContext.Depenses.Update(depense);
                await bdContext.SaveChangesAsync();

                return new Response() { Message = "Dépense mise à jour dans la base des données !!", TypeResponse = (int)TypeResponse.Success };
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message, TypeResponse = (int)TypeResponse.Error }; ;
            }
        }
    }
}
