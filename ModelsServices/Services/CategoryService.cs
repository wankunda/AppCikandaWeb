using Microsoft.EntityFrameworkCore;
using ModelsServices.Data;
using ModelsServices.Models;
using ModelsServices.Utilisties;
using ModelsServices.Utilities;

namespace ModelsServices.Services
{
    public class CategoryService
    {
        AppDbContext bdContext;
        public CategoryService()
        {
            bdContext = new AppDbContext();
        }

        public async Task<Response> Add(Category Model)
        {
            try
            {
                await bdContext.Categories.AddAsync(Model);
                await bdContext.SaveChangesAsync();
                return new Response() { Message = "Catégorie créée avec succès !!", TypeResponse = (int)TypeResponse.Success };
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message, TypeResponse = (int)TypeResponse.Error }; ;
            }
        }

        public async Task<IEnumerable<Category>?> GetAll()
        {
            return await bdContext.Categories.ToListAsync();
        }

        public async Task<Response> Delete(Guid Id)
        {
            try
            {
                var user = await bdContext.Categories.FirstOrDefaultAsync(e => e.Id == Id);
                if (user != null)
                {
                    bdContext.Categories.Remove(user);
                    await bdContext.SaveChangesAsync();
                    return new Response() { Message = "Catégorie supprimée avec succès !!", TypeResponse = (int)TypeResponse.Success };
                }
                return new Response() { Message = "Erreur de suppression des données", TypeResponse = (int)TypeResponse.Warning };
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message, TypeResponse = (int)TypeResponse.Error };
            }
        }

        public async Task<Category>? Open(Guid id)
        {
            return await bdContext.Categories
                .Include(e => e.Produits)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Response> Update(Category Model)
        {
            try
            {
                Model.DateAnsyc = DateTime.Now;
                Model.DownAsync = true;
                Model.UpAsync = false;
                bdContext.Categories.Update(Model);
                await bdContext.SaveChangesAsync();
                return new Response() { Message = "Catégorie mise à jour avec succès !!", TypeResponse = (int)TypeResponse.Success }; ;
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message, TypeResponse = (int)TypeResponse.Error }; ;
            }
        }

        public async Task<Category>? Get(Guid id)
        {
            return await bdContext.Categories
                .Include(e => e.Produits)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
