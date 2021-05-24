using CadastroCaminhao.Data.Repository;
using CadastroCaminhao.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace CadastroCaminhao.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new RepositoryBase(
                serviceProvider.GetRequiredService<
                    DbContextOptions<RepositoryBase>>()))
            {
                if (!context.Modelo.Any())
                {
                    context.Modelo.AddRange(
                    new Modelo
                    {
                        Descricao = "FM"
                    }
                    );

                    context.Modelo.AddRange(
                    new Modelo
                    {
                        Descricao = "FH"
                    }
                    );

                    context.SaveChanges();
                }

                if (!context.Caminhao.Any())
                {
                    context.Caminhao.AddRange(
                        new Caminhao
                        {
                            AnoFabricacao = "1989-2-12",
                            AnoModelo = "1989-2-12",
                            ModeloId = context.Modelo.FirstOrDefault(m => m.Descricao == "FM").Id
                        }
                    ) ;

                    context.SaveChanges();
                }
            }
        }
    }
}
