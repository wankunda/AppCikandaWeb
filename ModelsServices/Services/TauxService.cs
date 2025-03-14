using Microsoft.EntityFrameworkCore;
using Models;
using Utilities;
using ViewModels;

namespace Services
{
    public class TauxService
    {
        AppLocalDbContext bdContext;
        public TauxService()
        {
            bdContext = new AppLocalDbContext();
        }

        public async Task<Response> Add(TauxAddModel Model)
        {
            Taux taux = new Taux
            {
                Code = Model.Code.ToString(),
                DateCreated = Model.DateCreated.ToShortDateString(),
                Delete = false,
                IdPointVente = Model.IdPointVente,
                Monnaie1=(int)Model.Monnaie1,
                Monnaie2=(int)Model.Monnaie2,
                MonnaieLocale=Model.MonnaieLocal,
                MonnaieConvertie=Model.MonnaieConvertie,
                Synchronized = false,
            };

            try
            {
                await bdContext.Taux.AddAsync(taux);
                await bdContext.SaveChangesAsync();

                return new Response() { Message = "Taux du jour bien sauvegardé dans la base des données !!", TypeResponse = (int)TypeResponse.Success };
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message, TypeResponse = (int)TypeResponse.Error }; ;
            }
        }

        public async Task<IEnumerable<TauxViewModel>?> GetAll()
        {
            var data = await bdContext.Taux
                .Include(e => e.PointVente)
                .Where(e => !e.Delete)
                .ToListAsync();
            List<TauxViewModel> list = new List<TauxViewModel>();

            int num = 1;

            foreach (var i in data)
            {
                list.Add(new TauxViewModel
                {
                    Id = i.Id,
                    Num = num,
                    Synchronized = i.Synchronized,
                    DateCreated = i.DateCreated,
                    LastSynchronized = i.LastSynchronized,
                    PointVente=i.PointVente.Designation,
                    MonnaieLocale=new Money(i.MonnaieLocale, i.Monnaie1),
                    MonnaieConvertie=new Money(i.MonnaieConvertie, i.Monnaie2),
                });
                num++;
            }

            return list;
        }

        public async Task<Response> Delete(int Id)
        {
            try
            {
                var taux = await bdContext.Taux
                    .FirstOrDefaultAsync(e => e.Id == Id);
                if (taux != null)
                {
                    taux.Delete = true;
                    taux.Synchronized = false;
                    bdContext.Taux.Update(taux);

                    await bdContext.SaveChangesAsync();
                    return new Response()
                    {
                        TypeResponse = (int)TypeResponse.Success,
                        Message = "Taux du jour supprimé de la base des données",
                    };
                }
                else
                    return new Response()
                    {
                        TypeResponse = (int)TypeResponse.Warning,
                        Message = $"Erreur de suppression du taux du jour",
                    };
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    TypeResponse = (int)TypeResponse.Error,
                    Message = $"Impossible de supprimer ce taux du jour. Voir {ex.Message}",
                };
            }
        }

        public async Task<TauxAddModel>? Get(int id)
        {
            try
            {
                var reponse = await bdContext.Taux
                    .Include(e => e.PointVente)
                    .FirstOrDefaultAsync(e => e.Id == id);

                var taux = new TauxAddModel()
                {
                    Code = new Guid(reponse.Code),
                    DateCreated = DateTime.Parse(reponse.DateCreated),
                    DateUpdated = reponse.DateUpdated == null ? DateTime.Now : DateTime.Parse(reponse.DateUpdated),
                    LastSynchronized = reponse.LastSynchronized == null ? DateTime.Now : DateTime.Parse(reponse.LastSynchronized),
                    Id = reponse.Id,
                    MonnaieConvertie = reponse.MonnaieConvertie,
                    MonnaieLocal = reponse.MonnaieLocale,
                    Monnaie2 = (Monnaie)reponse.Monnaie2,
                    Monnaie1 = (Monnaie)reponse.Monnaie1,
                    IdPointVente = reponse.IdPointVente,
                    Synchronized = reponse.Synchronized
                };
                return taux;
            }
            catch
            {
                return new TauxAddModel();
            }
        }

        public async Task<Response> Update(TauxAddModel Model)
        {
            Taux taux = new Taux
            {
                Code = Model.Code.ToString(),
                DateCreated = Model.DateCreated.ToShortDateString(),
                Delete = false,
                IdPointVente = Model.IdPointVente,
                Monnaie1 = (int)Model.Monnaie1,
                Monnaie2 = (int)Model.Monnaie2,
                MonnaieLocale = Model.MonnaieLocal,
                MonnaieConvertie = Model.MonnaieConvertie,
                Synchronized = false,
                DateUpdated = Model.DateUpdated.ToShortDateString(),
                LastSynchronized = Model.LastSynchronized.ToShortDateString(),
                Id = Model.Id,
            };

            try
            {
                bdContext.Taux.Update(taux);
                await bdContext.SaveChangesAsync();

                return new Response() { Message = "Taux du jour mis à jour dans la base des données !!", TypeResponse = (int)TypeResponse.Success };
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message, TypeResponse = (int)TypeResponse.Error }; ;
            }
        }
    }
}
