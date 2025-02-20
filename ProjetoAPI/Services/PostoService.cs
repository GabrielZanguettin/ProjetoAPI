using MySqlX.XDevAPI;
using NHibernate;
<<<<<<< HEAD
=======
using NHibernate.Linq;
>>>>>>> b27b432 (Adicionando API)
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
<<<<<<< HEAD
        public Posto Remover(int id)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            var posto = session.Get<Posto>(id);
=======
        public async Task<Posto> RemoveGasStation(int id)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            var posto = await session.GetAsync<Posto>(id);
>>>>>>> b27b432 (Adicionando API)
            if (posto == null)
            {
                throw new Exception("Posto não encontrado");
            }
<<<<<<< HEAD
            session.Delete(posto);
            transaction.Commit();
            return posto;
        }
        public Posto GetPorId(int id)
        {
            using var session = sessionFactory.OpenSession();
            return session.Get<Posto>(id);
        }
        public bool Editar(int id, PostoDTO postodto)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            var posto = session.Get<Posto>(id);
            if (posto == null)
            {
                return false;
=======
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
>>>>>>> b27b432 (Adicionando API)
            }
            posto.CNPJ = postodto.CNPJ;
            posto.Endereco = postodto.Endereco;
            posto.Telefone = postodto.Telefone;
            posto.HoraFuncionamento = postodto.HoraFuncionamento;
            posto.NomePosto = postodto.NomePosto;
<<<<<<< HEAD
            session.Update(posto);
            transaction.Commit();
            return true;
        }
        public bool EditarPreco(int id, PostoDTOGas postodtogas)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            var posto = session.Get<Posto>(id);
            if (posto == null)
            {
                return false;
=======
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
>>>>>>> b27b432 (Adicionando API)
            }
            posto.Gasolina = postodtogas.Gasolina;
            posto.Diesel = postodtogas.Diesel;
            posto.Etanol = postodtogas.Etanol;
<<<<<<< HEAD
            session.Update(posto);
            transaction.Commit();
            return true;
        }
        public IEnumerable<Posto> GetPostos()
        {
            using var session = sessionFactory.OpenSession();
            var postos = session.Query<Posto>().OrderBy(posto => posto.Gasolina).ToList();
            return postos;
        }
        public IEnumerable<Posto> GetPostos2()
        {
            using var session = sessionFactory.OpenSession();
            var postos = session.Query<Posto>().OrderBy(posto => posto.Diesel).ToList();
            return postos;
        }
        public IEnumerable<Posto> GetPostos3()
        {
            using var session = sessionFactory.OpenSession();
            var postos = session.Query<Posto>().OrderBy(posto => posto.Etanol).ToList();
            return postos;
        }
        public IEnumerable<Posto> GetPostos4()
        {
            using var session = sessionFactory.OpenSession();
            var postos = session.Query<Posto>().OrderByDescending(posto => posto.PostoId).ToList();
=======
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
>>>>>>> b27b432 (Adicionando API)
            return postos;
        }
    }
}
