using Microsoft.AspNetCore.Authorization;
using NHibernate;
<<<<<<< HEAD
=======
using NHibernate.Linq;
>>>>>>> b27b432 (Adicionando API)
using ProjetoAPI.Dtos;
using ProjetoAPI.Entidades;
using System.ComponentModel.DataAnnotations;

namespace ProjetoAPI.Services
{
    public class DonoPostoService
    {
        private readonly ISessionFactory sessionFactory;
        private readonly TokenService tokenService;
        public DonoPostoService(ISessionFactory sessionFactory, TokenService tokenService)
        {
            this.sessionFactory = sessionFactory;
            this.tokenService = tokenService;
        }
        public string HashSenha(string senha)
        {
            return BCrypt.Net.BCrypt.HashPassword(senha);
        }
<<<<<<< HEAD
        public bool CadastrarDonoPosto(DonoPosto donoposto)
=======
        public async Task<DonoPosto> CreateDonoPosto(DonoPosto donoposto)
>>>>>>> b27b432 (Adicionando API)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            donoposto.SenhaHasheada = HashSenha(donoposto.SenhaHasheada);
<<<<<<< HEAD
            session.Save(donoposto);
            transaction.Commit();
            return true;
        }
        public bool ValidarCredenciais(string email, string senha)
        {
            using var session = sessionFactory.OpenSession();
            var donoPosto = session.Query<DonoPosto>().FirstOrDefault(d => d.Email == email);
            if (donoPosto == null)
            {
=======
            await session.SaveAsync(donoposto);
            await transaction.CommitAsync();
            return donoposto;
        }
        public async Task<bool> ValidateCredentials(string email, string senha)
        {
            using var session = sessionFactory.OpenSession();
            var donoPosto = await session.Query<DonoPosto>().FirstOrDefaultAsync(d => d.Email == email);
            if (donoPosto == null)
            {
                BCrypt.Net.BCrypt.Verify(senha, BCrypt.Net.BCrypt.HashPassword("senha_falsa"));
>>>>>>> b27b432 (Adicionando API)
                return false;
            }
            return BCrypt.Net.BCrypt.Verify(senha, donoPosto.SenhaHasheada);
        }
<<<<<<< HEAD
        public bool ValidarPosto(string cnpj, string endereco)
        {
            using var session = sessionFactory.OpenSession();
            var posto = session.Query<Posto>().FirstOrDefault(p => p.CNPJ == cnpj || p.Endereco == endereco);
            if (posto == null)
            {
                return true;
            }
            return false;
        }
        public bool CriarPosto(int id, Posto posto)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            var donoPosto = session.Get<DonoPosto>(id);
            if (donoPosto == null)
            {
                return false;
=======

        public async Task<DonoPosto?> SearchForEmail(string email)
        {
            using var session = sessionFactory.OpenSession();
            return await session.Query<DonoPosto>().FirstOrDefaultAsync(d => d.Email == email);
        }
        public async Task<bool> ValidateGasStation(string cnpj, string endereco)
        {
            using var session = sessionFactory.OpenSession();
            return (await session.Query<Posto>().FirstOrDefaultAsync(p => p.CNPJ == cnpj || p.Endereco == endereco)) == null;
        }
        public async Task<Posto> CreateGasStation(int id, Posto posto)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            var donoPosto = await session.GetAsync<DonoPosto>(id);
            if (donoPosto == null)
            {
                throw new Exception("Dono do Posto não encontrado");
>>>>>>> b27b432 (Adicionando API)
            }
            posto.DonoPosto = donoPosto;
            posto.DonoPostoId = id;
            donoPosto.Postos.Add(posto);
<<<<<<< HEAD
            session.Save(posto);
            session.Update(donoPosto);
            transaction.Commit();
            return true;
        }
        public List<Posto> MostrarPostos(int id)
        {
            using var session = sessionFactory.OpenSession();
            var donoPosto = session.Get<DonoPosto>(id);
=======
            await session.SaveAsync(posto);
            await session.UpdateAsync(donoPosto);
            await transaction.CommitAsync();
            return posto;
        }
        public async Task<List<Posto>> ShowGasStations(int id)
        {
            using var session = sessionFactory.OpenSession();
            var donoPosto = await session.GetAsync<DonoPosto>(id);
>>>>>>> b27b432 (Adicionando API)
            if (donoPosto == null)
            {
                throw new Exception("Dono do Posto não encontrado");
            }
            return donoPosto.Postos.ToList();
        }
<<<<<<< HEAD
        public DonoPosto EditarDonoPosto (int id, DonoPosto donoposto)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            var donoPosto = session.Get<DonoPosto>(id);
            if (donoPosto == null)
            {
                throw new Exception("DonoPosto nulo");
=======
        public async Task<DonoPosto> EditDonoPosto (int id, DonoPosto donoposto)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            var donoPosto = await session.GetAsync<DonoPosto>(id);
            if (donoPosto == null)
            {
                throw new Exception("Dono do Posto não encontrado");
>>>>>>> b27b432 (Adicionando API)
            }
            donoPosto.Nome = donoposto.Nome;
            donoPosto.CPF = donoposto.CPF;
            donoPosto.Email = donoposto.Email;
            donoPosto.SenhaHasheada = HashSenha(donoposto.SenhaHasheada);
            donoPosto.Telefone = donoposto.Telefone;
<<<<<<< HEAD
            session.Update(donoPosto);
            transaction.Commit();
            return donoPosto;
        }
        public DonoPosto MostrarInformacoes(int id)
        {
            using var session = sessionFactory.OpenSession();
            var donoPosto = session.Get<DonoPosto>(id);
=======
            await session.UpdateAsync(donoPosto);
            await transaction.CommitAsync();
            return donoPosto;
        }
        public async Task<DonoPosto> ShowInfos(int id)
        {
            using var session = sessionFactory.OpenSession();
            var donoPosto = await session.GetAsync<DonoPosto>(id);
>>>>>>> b27b432 (Adicionando API)
            if (donoPosto == null)
            {
                throw new Exception("Dono do Posto não encontrado");
            }
            return donoPosto;
        }
<<<<<<< HEAD
        public DonoPosto Remover(int id)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            var donoPosto = session.QueryOver<DonoPosto>()
                           .Where(d => d.DonoPostoId == id)
                           .Fetch(d => d.Postos).Eager
                           .SingleOrDefault();
=======
        public async Task<DonoPosto> DeleteDonoPosto(int id)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            var donoPosto = await session.QueryOver<DonoPosto>()
                           .Where(d => d.DonoPostoId == id)
                           .Fetch(d => d.Postos).Eager
                           .SingleOrDefaultAsync();
>>>>>>> b27b432 (Adicionando API)
            if (donoPosto == null)
            {
                throw new Exception("Dono do Posto não encontrado");
            }
<<<<<<< HEAD
            session.Delete(donoPosto);
            transaction.Commit();
=======
            await session.DeleteAsync(donoPosto);
            await transaction.CommitAsync();
>>>>>>> b27b432 (Adicionando API)
            return donoPosto;
        }
    }
}
