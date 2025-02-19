using Microsoft.EntityFrameworkCore;
using ModelsServices.Data;
using ModelsServices.Models;
using ModelsServices.Utilisties;
using ModelsServices.Utilities;

namespace ModelsServices.Services
{
    public class PointVenteService
    {
        AppDbContext bdContext;
        public PointVenteService()
        {
            bdContext = new AppDbContext();
        }

        public async Task<Response> Add(PointVente Model)
        {
            try
            {
                await bdContext.PointVentes.AddAsync(Model);
                await bdContext.SaveChangesAsync();
                return new Response() { Message = "Point de vente créé avec succès !!", TypeResponse = (int)TypeResponse.Success }; ;
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message, TypeResponse = (int)TypeResponse.Error }; ;
            }
        }

        public async Task<IEnumerable<PointVente>?> GetAll()
        {
            return await bdContext.PointVentes
                .OrderBy(x => x.Designation)
                .ToListAsync();
        }

        public async Task<Response> Delete(Guid Id)
        {
            try
            {
                var user = await bdContext.PointVentes.FirstOrDefaultAsync(e => e.Id == Id);
                if (user != null)
                {
                    bdContext.PointVentes.Remove(user);
                    await bdContext.SaveChangesAsync();
                    return new Response() { Message = "Point de vente supprimé avec succès !!", TypeResponse = (int)TypeResponse.Success }; ;
                }
                return new Response() { Message = "Erreur de suppression des données", TypeResponse = (int)TypeResponse.Warning };
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message, TypeResponse = (int)TypeResponse.Error }; ;
            }
        }

        public async Task<PointVente>? Open(Guid id)
        {
            return await bdContext.PointVentes
                .Include(e => e.PrixVentes)
                .ThenInclude(e => e.Produit)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Response> Update(PointVente Model)
        {
            try
            {
                Model.DateAnsyc = DateTime.Now;
                Model.DownAsync = true;
                Model.UpAsync = false;
                bdContext.PointVentes.Update(Model);
                await bdContext.SaveChangesAsync();
                return new Response() { Message = "Point de vente mis à jour avec succès !!", TypeResponse = (int)TypeResponse.Success }; ;
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message, TypeResponse = (int)TypeResponse.Error }; ;
            }
        }

        public async Task<PointVente>? Get(Guid id)
        {
            return await bdContext.PointVentes
                .Include(e => e.PrixVentes)
                .ThenInclude(e => e.Produit)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
