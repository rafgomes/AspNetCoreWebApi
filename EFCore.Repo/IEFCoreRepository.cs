using EFCore.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Repo
{
    public interface IEFCoreRepository
    {
        void Add<T>(Task entity) where T : class; //assinatura generica determinando qualquer tipo como parametro
        void Update<T>(Task entity) where T : class; 
        void Delete<T>(Task entity) where T : class;

        Task<bool> SaveChangeAsync();

        Task<Heroi[]> GetAllHerois(bool incluirBatalha = false);
        Task<Heroi> GetHeroiById(int id);
        Task<Heroi> GetHeroiByNome(string nome);
    }
}
