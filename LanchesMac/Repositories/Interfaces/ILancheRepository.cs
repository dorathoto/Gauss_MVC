using LanchesMac.Attributes;
using LanchesMac.Models;
using Implementation = LanchesMac.Repositories.LancheRepository;

namespace LanchesMac.Repositories.Interfaces
{

    /// <summary>
    /// Aqui coloquei o Implementation para assim poder da um F12 e ir direto para a implementação
    /// </summary>
    public interface ILancheRepository
    {
        [ImplementedMethod(nameof(Implementation.Lanches))]
        IEnumerable<Lanche> Lanches { get; }

        [ImplementedMethod(nameof(Implementation.LanchesPreferidos))]
        IEnumerable<Lanche> LanchesPreferidos { get; }

        [ImplementedMethod(nameof(Implementation.GetLancheById))]
        Lanche GetLancheById(int lancheId);

    }
}
