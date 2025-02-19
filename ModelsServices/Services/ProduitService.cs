using Microsoft.EntityFrameworkCore;
using ModelsServices.Data;
using ModelsServices.Models;
using ModelsServices.Utilisties;
using ModelsServices.Utilities;

namespace ModelsServices.Services
{
    public class ProduitService
    {
        AppDbContext bdContext;
        public ProduitService()
        {
            bdContext = new AppDbContext();
        }

        public async Task<Response> Add(Produit Model)
        {
            try
            {
                await bdContext.Produits.AddAsync(Model);
                await bdContext.SaveChangesAsync();

                return new Response() { Message = "Article créé avec succès !!", Id = Model.Id, TypeResponse = (int)TypeResponse.Success };
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message, TypeResponse = (int)TypeResponse.Error }; ;
            }
        }


        public async Task<IEnumerable<Produit>?> GetAll()
        {
            return await bdContext.Produits
                .Include(e => e.Category)
                .OrderBy(e => e.Category.Libellé)
                .ThenBy(e => e.Designation)
                .ToListAsync();
        }

        public async Task<IEnumerable<Category>?> GetCategories()
        {
            return await bdContext.Categories.ToListAsync();
        }

        public async Task<Response> Delete(Guid Id)
        {
            try
            {
                var produit = await bdContext.Produits
                    .Include(e => e.Category)
                    .FirstOrDefaultAsync(e => e.Id == Id);
                if (produit != null)
                {
                    bdContext.Produits.Remove(produit);
                    await bdContext.SaveChangesAsync();
                    return new Response() { Message = "Article supprimé avec succès !!", TypeResponse = (int)TypeResponse.Success }; ;
                }
                return new Response() { Message = "Erreur de suppression des données", TypeResponse = (int)TypeResponse.Warning };
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message }; ;
            }
        }

        public async Task<Produit>? Get(Guid id)
        {
            return await bdContext.Produits
                .Include(e => e.Category)
                .Include(e => e.PrixVentes)
                .ThenInclude(e => e.PointVente)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Response> Update(Produit Model)
        {
            try
            {
                Model.DateAnsyc = DateTime.Now;
                Model.DownAsync = true;
                Model.UpAsync = false;
                bdContext.Produits.Update(Model);
                await bdContext.SaveChangesAsync();
                return new Response() { Message = "Article mis à jour avec succès !!", Id = Model.Id, TypeResponse = (int)TypeResponse.Success };
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message, TypeResponse = (int)TypeResponse.Warning }; ;
            }
        }
    }
}
