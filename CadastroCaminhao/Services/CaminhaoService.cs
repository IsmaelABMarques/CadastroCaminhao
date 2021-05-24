using CadastroCaminhao.Data.Repository;
using CadastroCaminhao.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroCaminhao.Services
{
    public class CaminhaoService
    {
        private readonly RepositoryBase _context;

        public CaminhaoService(RepositoryBase context)
        {
            _context = context;
        }
        public async Task<List<Caminhao>> GetAll()
        {
            var caminhao = await _context.Caminhao.ToListAsync();

            caminhao.ForEach(c => {
                c.Modelo = _context.Modelo.Find(c.ModeloId);
            });

            return caminhao;
        }

        public async Task<List<Modelo>> GetAllModelo()
        {
            var modelo = await _context.Modelo.ToListAsync();

            return modelo;
        }

        public async Task<Caminhao> Get(int? id)
        {
            var caminhao = await _context.Caminhao
                .FirstOrDefaultAsync(m => m.Id == id);

            return caminhao;
        }

        public async Task<Modelo> GetModelo(int? id)
        {
            var modelo = await _context.Modelo
                .FirstOrDefaultAsync(m => m.Id == id);

            return modelo;
        }

        public async Task<Modelo> GetModelo(string desc)
        {
            var modelo = await _context.Modelo
                .FirstOrDefaultAsync(m => m.Descricao == desc);

            return modelo;
        }

        public async Task<int> Insert(Caminhao caminhao)
        {
            var c = _context.Add(caminhao);
            await _context.SaveChangesAsync();

            return c.Entity.Id;
        }

        public async Task<int> Insert(Modelo modelo)
        {
            var c = _context.Add(modelo);
            await _context.SaveChangesAsync();

            return c.Entity.Id;
        }

        public async Task<int> Update(Caminhao caminhao)
        {
            var c = _context.Update(caminhao);
            await _context.SaveChangesAsync();

            return c.Entity.Id;
        }

        public async Task Delete(int id)
        {
            var Caminhao = await _context.Caminhao.FindAsync(id);
            _context.Caminhao.Remove(Caminhao);
            await _context.SaveChangesAsync();
        }

        public bool Exists(int id)
        {
            return _context.Caminhao.Any(e => e.Id == id);
        }
    }
}
