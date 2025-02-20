using Microsoft.AspNetCore.Authorization;
using NHibernate;
using NHibernate.Linq;
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
        public async Task<DonoPosto> CreateDonoPosto(DonoPosto donoposto)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            donoposto.SenhaHasheada = HashSenha(donoposto.SenhaHasheada);
            await session.SaveAsync(donoposto);
            await transaction.CommitAsync();
            return donoposto;
        }
        public async Task<DonoPosto?> SearchForEmail(string email)
        {
            using var session = sessionFactory.OpenSession();
            return await session.Query<DonoPosto>().FirstOrDefaultAsync(d => d.Email == email);
        }
        public async Task<bool> ValidateCredentials(string email, string senha)
        {
            using var session = sessionFactory.OpenSession();
            var donoPosto = await session.Query<DonoPosto>().FirstOrDefaultAsync(d => d.Email == email);
            if (donoPosto == null)
            {
                BCrypt.Net.BCrypt.Verify(senha, BCrypt.Net.BCrypt.HashPassword("senha_falsa"));
                return false;
            }
            return BCrypt.Net.BCrypt.Verify(senha, donoPosto.SenhaHasheada);
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
            }
            posto.DonoPosto = donoPosto;
            posto.DonoPostoId = id;
            donoPosto.Postos.Add(posto);
            await session.SaveAsync(posto);
            await session.UpdateAsync(donoPosto);
            await transaction.CommitAsync();
            return posto;
        }
        public async Task<List<Posto>> ShowGasStations(int id)
        {
            using var session = sessionFactory.OpenSession();
            var donoPosto = await session.GetAsync<DonoPosto>(id);
            if (donoPosto == null)
            {
                throw new Exception("Dono do Posto não encontrado");
            }
            return donoPosto.Postos.ToList();
        }
        public async Task<DonoPosto> EditDonoPosto(int id, DonoPosto donoposto)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            var donoPosto = await session.GetAsync<DonoPosto>(id);
            if (donoPosto == null)
            {
                throw new Exception("Dono do Posto não encontrado");
            }
            donoPosto.Nome = donoposto.Nome;
            donoPosto.CPF = donoposto.CPF;
            donoPosto.Email = donoposto.Email;
            donoPosto.SenhaHasheada = HashSenha(donoposto.SenhaHasheada);
            donoPosto.Telefone = donoposto.Telefone;
            await session.UpdateAsync(donoPosto);
            await transaction.CommitAsync();
            return donoPosto;
        }
        public async Task<DonoPosto> ShowInfos(int id)
        {
            using var session = sessionFactory.OpenSession();
            var donoPosto = await session.GetAsync<DonoPosto>(id);
            if (donoPosto == null)
            {
                throw new Exception("Dono do Posto não encontrado");
            }
            return donoPosto;
        }
        public async Task<DonoPosto> DeleteDonoPosto(int id)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            var donoPosto = await session.QueryOver<DonoPosto>()
                           .Where(d => d.DonoPostoId == id)
                           .Fetch(d => d.Postos).Eager
                           .SingleOrDefaultAsync();
            if (donoPosto == null)
            {
                throw new Exception("Dono do Posto não encontrado");
            }
            await session.DeleteAsync(donoPosto);
            await transaction.CommitAsync();
            return donoPosto;
        }
    }
}