using CadastroCaminhao.Models.Entity;
using CadastroCaminhao.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CadastroCaminhao.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CadastroCaminhaoTest
{
    [TestClass]
    public class CaminhaoTests
    {
        private CaminhaoService _service;
        public async Task Initialize()
        {
            var context = new DbContextOptionsBuilder<RepositoryBase>();
            context.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CaminhaoTest;Trusted_Connection=True;MultipleActiveResultSets=true");
            _service = new CaminhaoService(new RepositoryBase(context.Options));

            var model1 = await _service.GetModelo("FM");
            var model2 = await _service.GetModelo("FH");

            if (model1 == null || model1.Id == 0)
            {
                await _service.Insert(
                new Modelo
                {
                    Descricao = "FM"
                }
                );
            }
            if (model1 == null || model2.Id == 0)
            {
                await _service.Insert(
                new Modelo
                {
                    Descricao = "FH"
                }
                );
            }
        }
        [TestMethod]
        public async Task Create()
        {
            await Initialize();
            Caminhao caminhao = new Caminhao();
            Caminhao caminhaoBuscado = new Caminhao();
            var modelo1 = await _service.GetModelo("FH");

            caminhao = new Caminhao { ModeloId = modelo1.Id, AnoFabricacao = "2010", AnoModelo = "2010" };
            caminhao.Id = await _service.Insert(caminhao);
            caminhaoBuscado = await _service.Get(caminhao.Id);

            await _service.Delete(caminhao.Id);

            Assert.IsFalse(
                caminhaoBuscado == null || caminhao.Id != caminhaoBuscado.Id || caminhao.ModeloId != caminhaoBuscado.ModeloId
                || caminhao.AnoModelo != caminhaoBuscado.AnoModelo || caminhao.AnoFabricacao != caminhaoBuscado.AnoFabricacao,
                "O Caminhão não foi inserido corretamente."
            );
        }
        [TestMethod]
        public async Task Edit()
        {
            await Initialize();
            Caminhao caminhao = new Caminhao();
            Caminhao caminhaoBuscado = new Caminhao();
            var modelo1 = await _service.GetModelo("FH");
            var modelo2 = await _service.GetModelo("FM");

            caminhao = new Caminhao { ModeloId = modelo1.Id, AnoFabricacao = "2010", AnoModelo = "2010" };

            caminhao.Id = await _service.Insert(caminhao);

            caminhao.ModeloId = modelo2.Id;
            caminhao.AnoFabricacao = "2011";
            caminhao.AnoModelo = "2011";

            await _service.Update(caminhao);
            caminhaoBuscado = await _service.Get(caminhao.Id);

            await _service.Delete(caminhao.Id);

            Assert.IsFalse(
                caminhaoBuscado == null || caminhao.Id != caminhaoBuscado.Id || caminhao.ModeloId != caminhaoBuscado.ModeloId
                || caminhao.AnoModelo != caminhaoBuscado.AnoModelo || caminhao.AnoFabricacao != caminhaoBuscado.AnoFabricacao,
                "O Caminhão não foi atualizado corretamente."
            );
        }

        [TestMethod]
        public async Task Delete()
        {
            await Initialize();
            Caminhao caminhao = new Caminhao();
            Caminhao caminhaoBuscado = new Caminhao();
            var modelo1 = await _service.GetModelo("FH");

            caminhao = new Caminhao { ModeloId = modelo1.Id, AnoFabricacao = "2010", AnoModelo = "2010" };

            caminhao.Id = await _service.Insert(caminhao);

            await _service.Delete(caminhao.Id);
            caminhaoBuscado = await _service.Get(caminhao.Id);

            Assert.IsTrue(
                caminhaoBuscado == null ,
                "O Caminhão não foi excluído corretamente."
            );
        }

        [TestMethod]
        public async Task Index()
        {
            await Initialize();
            Caminhao caminhao1 = new Caminhao();
            Caminhao caminhao2 = new Caminhao();
            Caminhao caminhao3 = new Caminhao();
    
            var modelo1 = await _service.GetModelo("FH");

            caminhao1 = new Caminhao { ModeloId = modelo1.Id, AnoFabricacao = "2010", AnoModelo = "2010" };
            caminhao2 = new Caminhao { ModeloId = modelo1.Id, AnoFabricacao = "2011", AnoModelo = "2011" };
            caminhao3 = new Caminhao { ModeloId = modelo1.Id, AnoFabricacao = "2012", AnoModelo = "2012" };

            caminhao1.Id = await _service.Insert(caminhao1);
            caminhao2.Id = await _service.Insert(caminhao2);
            caminhao3.Id = await _service.Insert(caminhao3);

            var caminhoes = await _service.GetAll();

            await _service.Delete(caminhao1.Id);
            await _service.Delete(caminhao2.Id);
            await _service.Delete(caminhao3.Id);

            
            Assert.IsTrue(
                caminhoes.Contains(caminhao1) && caminhoes.Contains(caminhao2) && caminhoes.Contains(caminhao2),
                "O Index não está funcionando corretamente."
            );
        }

    }
}
