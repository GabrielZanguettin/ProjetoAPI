using NHibernate;
using NHibernate.Linq;
using ProjetoAPI.Dtos;
using ProjetoAPI.Entidades;

namespace ProjetoAPI.Services
{
    public class PostoService
    {
        private readonly ISessionFactory sessionFactory;
        public PostoService(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }
        public async Task<Posto> RemoveGasStation(int id)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            var posto = await session.GetAsync<Posto>(id);
            if (posto == null)
            {
                throw new Exception("Posto não encontrado");
            }
            await session.DeleteAsync(posto);
            await transaction.CommitAsync();
            return posto;
        }
        public async Task<Posto> GetGasStationById(int id)
        {
            using var session = sessionFactory.OpenSession();
            var posto = await session.GetAsync<Posto>(id);
            return posto;
        }
        public async Task<Posto> EditGasStation(int id, PostoDTO postodto)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            var posto = await session.GetAsync<Posto>(id);
            if (posto == null)
            {
                throw new Exception("Posto não encontrado");
            }
            posto.CNPJ = postodto.CNPJ;
            posto.Endereco = postodto.Endereco;
            posto.Telefone = postodto.Telefone;
            posto.HoraFuncionamento = postodto.HoraFuncionamento;
            posto.NomePosto = postodto.NomePosto;
            await session.UpdateAsync(posto);
            await transaction.CommitAsync();
            return posto;
        }
        public async Task<Posto> EditPrices(int id, PostoDTOGas postodtogas)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            var posto = await session.GetAsync<Posto>(id);
            if (posto == null)
            {
                throw new Exception("Posto não encontrado");
            }
            posto.Gasolina = postodtogas.Gasolina;
            posto.Diesel = postodtogas.Diesel;
            posto.Etanol = postodtogas.Etanol;
            await session.UpdateAsync(posto);
            await transaction.CommitAsync();
            return posto;
        }
        public async Task<IEnumerable<Posto>> GetGasStationsByGasoline()
        {
            using var session = sessionFactory.OpenSession();
            var postos = await session.Query<Posto>().OrderBy(posto => posto.Gasolina).ToListAsync();
            return postos;
        }
        public async Task<IEnumerable<Posto>> GetGasStationsByDiesel()
        {
            using var session = sessionFactory.OpenSession();
            var postos = await session.Query<Posto>().OrderBy(posto => posto.Diesel).ToListAsync();
            return postos;
        }
        public async Task<IEnumerable<Posto>> GetGasStationByEthanol()
        {
            using var session = sessionFactory.OpenSession();
            var postos = await session.Query<Posto>().OrderBy(posto => posto.Etanol).ToListAsync();
            return postos;
        }
        public async Task<IEnumerable<Posto>> GetNewGasStations()
        {
            using var session = sessionFactory.OpenSession();
            var postos = await session.Query<Posto>().OrderByDescending(posto => posto.PostoId).ToListAsync();
            return postos;
        }
    }
}