using Microsoft.EntityFrameworkCore;
using Models;
using Utilities;
using ViewModels;

namespace Services
{
    public class UserService
    {
        AppLocalDbContext bdContext;
        public UserService()
        {
            bdContext = new AppLocalDbContext();
        }

        public async Task<Response> Add(UserAddModel Model)
        {
            User user = new User
            {
                Code = Model.Code.ToString(),
                DateCreated = Model.DateCreated.ToShortDateString(),
                Delete = false,
                IdPointVente = Model.IdPointVente,
                Email = Model.Email,
                Nom = Model.Nom,
                Postnom = Model.Postnom,
                Prenom = Model.Prenom,
                Password = Model.Password,
                Photo = Model.Photo,
                Username = Model.Username,
                UserRole = Model.UserRole.ToString(),
                Synchronized = false,
            };

            try
            {
                await bdContext.Users.AddAsync(user);
                await bdContext.SaveChangesAsync();

                return new Response() { Message = "Utilisateur bien sauvegardé dans la base des données !!", TypeResponse = (int)TypeResponse.Success };
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message, TypeResponse = (int)TypeResponse.Error }; ;
            }
        }

        public async Task<IEnumerable<UserViewModel>?> GetAll()
        {
            var data = await bdContext.Users
                .Where(e => !e.Delete)
                .ToListAsync();
            List<UserViewModel> list = new List<UserViewModel>();

            int num = 1;

            foreach (var i in data)
            {
                list.Add(new UserViewModel
                {
                    Id = i.Id,
                    Num = num,
                    Synchronized = i.Synchronized,
                    DateCreated = i.DateCreated,
                    Email = i.Email,
                    NomComplet = $"{i.Nom} {i.Postnom} {i.Prenom}",
                    Photo = i.Photo,
                    Username = i.Username,
                    UserRole = i.UserRole,
                    LastSynchronized = i.LastSynchronized,
                });
                num++;
            }

            return list;
        }

        public async Task<Response> Delete(int Id)
        {
            try
            {
                var user = await bdContext.Users
                    .FirstOrDefaultAsync(e => e.Id == Id);
                if (user != null)
                {
                    user.Delete = true;
                    user.Synchronized = false;
                    bdContext.Users.Update(user);

                    await bdContext.SaveChangesAsync();
                    return new Response()
                    {
                        TypeResponse = (int)TypeResponse.Success,
                        Message = "Utilisateur supprimé de la base des données",
                    };
                }
                else
                    return new Response()
                    {
                        TypeResponse = (int)TypeResponse.Warning,
                        Message = $"Erreur de suppression de l'utilisateur",
                    };
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    TypeResponse = (int)TypeResponse.Error,
                    Message = $"Impossible de supprimer cet utilisateur. Voir {ex.Message}",
                };
            }
        }

        public async Task<UserAddModel>? Get(int id)
        {
            try
            {
                var reponse = await bdContext.Users
                    .FirstOrDefaultAsync(e => e.Id == id);

                var user = new UserAddModel()
                {
                    Code = new Guid(reponse.Code),
                    DateCreated = DateTime.Parse(reponse.DateCreated),
                    DateUpdated = DateTime.Parse(reponse.DateUpdated),
                    LastSynchronized = DateTime.Parse(reponse.LastSynchronized),
                    Id = reponse.Id,
                    Nom=reponse.Nom,
                    Postnom=reponse.Postnom,
                    UserRole= (UserRole)Enum.Parse(typeof(UserRole), reponse.UserRole),
                    Username=reponse.Username,
                    Email=reponse.Email,
                    Photo=reponse.Photo,
                    Password=reponse.Password,
                    Prenom=reponse.Prenom,
                    IdPointVente = reponse.IdPointVente,
                    Synchronized = reponse.Synchronized
                };
                return user;
            }
            catch
            {
                return new UserAddModel();
            }
        }

        public async Task<UserViewModel>? Open(int id)
        {
            try
            {
                var reponse = await bdContext.Users
                    .FirstOrDefaultAsync(e => e.Id == id);

                var user = new UserViewModel()
                {
                    Code = reponse.Code,
                    DateCreated = reponse.DateCreated,
                    DateUpdated = reponse.DateUpdated,
                    LastSynchronized = reponse.LastSynchronized,
                    Id = reponse.Id,
                    Email=reponse.Email,
                    NomComplet=$"{reponse.Nom} {reponse.Postnom} {reponse.Prenom}",
                    Photo=reponse.Photo,
                    UserRole=reponse.UserRole,
                    Username=reponse.Username,
                    Synchronized = reponse.Synchronized
                };
                return user;
            }
            catch
            {
                return new UserViewModel();
            }
        }

        public async Task<Response> Update(UserAddModel Model)
        {
            User user = new User
            {
                Code = Model.Code.ToString(),
                DateCreated = Model.DateCreated.ToShortDateString(),
                Delete = false,
                IdPointVente = Model.IdPointVente,
                Email = Model.Email,
                Nom = Model.Nom,
                Postnom = Model.Postnom,
                Prenom = Model.Prenom,
                Password = Model.Password,
                Photo = Model.Photo,
                Username = Model.Username,
                UserRole = Model.UserRole.ToString(),
                Synchronized = false,
                Id = Model.Id,
                DateUpdated = Model.DateUpdated.ToShortDateString(),
                LastSynchronized = Model.LastSynchronized.ToShortDateString()
            };

            try
            {
                bdContext.Users.Update(user);
                await bdContext.SaveChangesAsync();

                return new Response() { Message = "Utilisateur mis à jour dans la base des données !!", TypeResponse = (int)TypeResponse.Success };
            }
            catch (Exception ex)
            {
                return new Response() { Message = ex.Message, TypeResponse = (int)TypeResponse.Error }; ;
            }
        }

        public async Task<Response>? Login(LoginModel login)
        {
            try
            {
                var user = await bdContext.Users.FirstOrDefaultAsync(e => (e.Username == login.Email || e.Email == login.Email) && !e.Delete);

                if(user!=null)
                {
                    if(user.Password==login.Password)
                    {
                        SessionModel session = new SessionModel
                        {
                            Email = user.Email,
                            Photo = user.Photo,
                            Username = user.Username,
                            UserRole = user.UserRole
                        };
                        return new Response
                        {
                            Message = "connexion réussie à la base des données",
                            TypeResponse = (int)TypeResponse.Success,
                            Content = session
                        };
                    }
                    else
                        return new Response
                        {
                            Message = "Le mot de passe est incorrect, veuillez ressayer !!",
                            TypeResponse = (int)TypeResponse.Success,
                        };
                }
            }
            catch
            {
                return new Response()
                {
                    Message = "Erreur de connexion, Vérifiez vos identifiants !!",
                    TypeResponse= (int)TypeResponse.Error
                };
            }
            return new Response()
            {
                Message = "Erreur de connexion, Vérifiez vos identifiants !!",
                TypeResponse = (int)TypeResponse.Error
            };
        }
    }
}
