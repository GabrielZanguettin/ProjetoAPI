using MySqlX.XDevAPI;
using NHibernate;
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
        public Posto Remover(int id)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            var posto = session.Get<Posto>(id);
            if (posto == null)
            {
                throw new Exception("Posto não encontrado");
            }
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
            }
            posto.CNPJ = postodto.CNPJ;
            posto.Endereco = postodto.Endereco;
            posto.Telefone = postodto.Telefone;
            posto.HoraFuncionamento = postodto.HoraFuncionamento;
            posto.Bandeira = postodto.Bandeira;
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
            }
            posto.Gasolina = postodtogas.Gasolina;
            posto.Diesel = postodtogas.Diesel;
            posto.Etanol = postodtogas.Etanol;
            session.Update(posto);
            transaction.Commit();
            return true;
        }
    }
}
