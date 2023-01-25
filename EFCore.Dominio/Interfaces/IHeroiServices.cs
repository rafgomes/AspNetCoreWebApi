using EFCore.Dominio;

namespace EFCore.Dominio.Interfaces
{
    public interface IHeroiServices
    {
        Task<string> AddHeroi(Heroi model);
        Task<string> DeleteHeroi(int id);
        Task<Heroi[]> GetAllHerois(bool incluirBatalha = false);
        Task<Heroi> GetHeroiById(int id, bool incluirBatalha = false);
        Task<Heroi[]> GetHeroiByNome(string nome, bool incluirBatalha = false);
        Task<string> UpdateHeroi(int id, Heroi model);
    }
}