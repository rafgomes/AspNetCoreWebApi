using EFCore.Dominio;
using EFCore.Dominio.Interfaces;
using EFCore.Repo;
using Microsoft.Identity.Client;

namespace EFCore.Services
{
    public class HeroiServices : IHeroiServices
    {
        private readonly IEFCoreRepository eFCoreRepository;

        public HeroiServices(IEFCoreRepository eFCoreRepository)
        {
            this.eFCoreRepository = eFCoreRepository;
        }

        public async Task<Heroi[]> GetAllHerois(bool incluirBatalha = false)
        {
            Heroi[] result = await this.eFCoreRepository.GetAllHerois(incluirBatalha);

            List<Heroi> list = result.ToList();

            return list.Where(h => h.Identidade.NomeReal != null).ToArray();
        }

        public async Task<Heroi> GetHeroiById(int id, bool incluirBatalha = false)
        {
            return await this.eFCoreRepository.GetHeroiById(id, incluirBatalha); 
        }

        public async Task<Heroi[]> GetHeroiByNome(string nome, bool incluirBatalha = false)
        {
            return await this.eFCoreRepository.GetHeroiByNome(nome, incluirBatalha);
        }

        public async Task<String> AddHeroi(Heroi model)
        {
            eFCoreRepository.Add(model);

            if (await eFCoreRepository.SaveChangeAsync())
            {
                return "BAZINGA";
            }
            throw new Exception("\"Não Salvou\"");
        }

        public async Task<String> UpdateHeroi(int id, Heroi model)
        {
            Heroi heroi = await eFCoreRepository.GetHeroiById(id);
            if (heroi != null)
            {
                eFCoreRepository.Update(model);

                if (await eFCoreRepository.SaveChangeAsync())
                    return "BAZINGA";
            }

            throw new Exception($"Id {id} não encontrado no banco de daodos");

        }

        public async Task<string> DeleteHeroi(int id)
        {
            var heroi = await eFCoreRepository.GetHeroiById(id);
            if (heroi != null)
            {
                eFCoreRepository.Delete(heroi);

                if (await eFCoreRepository.SaveChangeAsync())
                    return "BAZINGA";
            }

             throw new Exception("Not Deleted");

        }


    }
}
