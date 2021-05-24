
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CadastroCaminhao.Models.Entity
{
    public class Modelo
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public List<Caminhao> Caminhoes { get; set; }
    }
}
