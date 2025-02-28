using Microsoft.EntityFrameworkCore;
using Models;
using Utilities;
using ViewModels;

namespace Services
{
    public class CommandService
    {
        AppLocalDbContext bdContext;
        public CommandService()
        {
            bdContext = new AppLocalDbContext();
        }

        public async Task<Response> Add(CommandAddModel Model)
        {
            Command command = new Command
            {
                Code = Model.Code.ToString(),
                DateCreated = Model.DateCreated.ToShortDateString(),
                Delete = false,
                IdPointVente=Model.IdPointVente,
                NomClient=Model.NomClient,
                NomServante=Model.NomServante,
                NumCmd=Model.NumCmd,
                Statut= (int)Model.Statut,
                Synchronized = false,
            };

            try
            {
                await bdContext.Commands.AddAsync(command);
                await bdContext.SaveChangesAsync();

                return new Response() { Message = "Commande bien sauvegardée dans la base des données !!", TypeResponse = (int)TypeResponse.Success };
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message, TypeResponse = (int)TypeResponse.Error }; ;
            }
        }

        public async Task<IEnumerable<CommandViewModel>?> GetAll()
        {
            var data = await bdContext.Commands
                .Include(e => e.ProduitVendus)
                .Where(e => !e.Delete)
                .ToListAsync();
            List<CommandViewModel> list = new List<CommandViewModel>();

            int num = 1;

            foreach (var i in data)
            {
                list.Add(new CommandViewModel
                {
                    Id = i.Id,
                    Num = num,
                    Synchronized = i.Synchronized,
                    NomServante = i.NomServante,
                    Statut = ((StatutCommand)i.Statut).ToString(),
                    DateCreated = i.DateCreated,
                    LastSynchronized = i.LastSynchronized,
                    NomClient = i.NomClient,
                    NumCmd = i.NumCmd,
                    PointVente = i.PointVente.Designation,
                });
                num++;
            }

            return list;
        }

        public async Task<Response> Delete(int Id)
        {
            try
            {
                var cmd = await bdContext.Commands
                    .FirstOrDefaultAsync(e => e.Id == Id);
                if (cmd != null)
                {
                    cmd.Delete = true;
                    cmd.Synchronized = false;
                    bdContext.Commands.Update(cmd);

                    foreach (var i in cmd.ProduitVendus)
                    {
                        i.Delete = true;
                        i.Synchronized = false;
                        bdContext.ProduitVendus.Update(i);
                    }

                    await bdContext.SaveChangesAsync();
                    return new Response()
                    {
                        TypeResponse = (int)TypeResponse.Success,
                        Message = "Commande supprimée de la base des données",
                    };
                }
                else
                    return new Response()
                    {
                        TypeResponse = (int)TypeResponse.Warning,
                        Message = $"Erreur de suppression de la commande",
                    };
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    TypeResponse = (int)TypeResponse.Error,
                    Message = $"Impossible de supprimer cette commande. Voir {ex.Message}",
                };
            }
        }

        public async Task<CommandAddModel>? Get(int id)
        {
            try
            {
                var reponse = await bdContext.Commands
                    .Include(e => e.ProduitVendus)
                    .Include(e => e.PointVente)
                    .FirstOrDefaultAsync(e => e.Id == id);

                var cmd = new CommandAddModel()
                {
                    Code = new Guid(reponse.Code),
                    DateCreated = DateTime.Parse(reponse.DateCreated),
                    DateUpdated = DateTime.Parse(reponse.DateUpdated),
                    LastSynchronized = DateTime.Parse(reponse.LastSynchronized),
                    NomClient = reponse.NomClient,
                    NomServante = reponse.NomServante,
                    IdPointVente = reponse.IdPointVente,
                    NumCmd = reponse.NumCmd,
                    Statut = (StatutCommand)reponse.Statut,
                    Synchronized = reponse.Synchronized
                };
                return cmd;
            }
            catch
            {
                return new CommandAddModel();
            }
        }

        public async Task<CommandViewModel>? Open(int id)
        {
            try
            {
                var reponse = await bdContext.Commands
                    .Include(e => e.ProduitVendus)
                    .ThenInclude(e => e.PrixVente)
                    .ThenInclude(e => e.Article)
                    .Include(e => e.PointVente)
                    .FirstOrDefaultAsync(e => e.Id == id);

                var cmd = new CommandViewModel()
                {
                    Code = reponse.Code,
                    DateCreated = reponse.DateCreated,
                    DateUpdated = reponse.DateUpdated,
                    LastSynchronized = reponse.LastSynchronized,
                    NomServante = reponse.NomServante,
                    NomClient = reponse.NomClient,
                    NumCmd = reponse.NumCmd,
                    PointVente = reponse.PointVente.Designation,
                    Statut = ((StatutCommand)(reponse.Statut)).ToString(),
                    Synchronized = reponse.Synchronized,
                    ProduitVendus = reponse.ProduitVendus.Select(e => new ProduitCommandViewModel
                    {
                        PrixVente = new Money(e.PrixVente.Value, e.PrixVente.Monnaie),
                        Quantite = e.Quantite,
                        Raison = e.Raison,
                        Article = e.PrixVente.Article.Designation,
                        Statut = ((StatutVente)e.Statut).ToString(),
                        ValeurTotale = new Money((e.Quantite * e.PrixVente.Value), e.PrixVente.Monnaie)
                    }).ToList()
                };
                return cmd;
            }
            catch
            {
                return new CommandViewModel();
            }
        }

        public async Task<Response> Update(CommandAddModel Model)
        {
            Command command = new Command
            {
                Code = Model.Code.ToString(),
                DateCreated = Model.DateCreated.ToShortDateString(),
                Delete = false,
                IdPointVente = Model.IdPointVente,
                NomClient = Model.NomClient,
                NomServante = Model.NomServante,
                NumCmd = Model.NumCmd,
                Statut = (int)Model.Statut,
                Synchronized = false,
                Id = Model.Id,
                DateUpdated = Model.DateUpdated.ToShortDateString(),
                LastSynchronized = Model.LastSynchronized.ToShortDateString(),
            };

            try
            {
                bdContext.Commands.Update(command);
                await bdContext.SaveChangesAsync();

                return new Response() { Message = "Commande mise à jour dans la base des données !!", TypeResponse = (int)TypeResponse.Success };
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message, TypeResponse = (int)TypeResponse.Error }; ;
            }
        }
    }
}
