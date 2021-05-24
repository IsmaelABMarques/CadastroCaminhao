using System.ComponentModel.DataAnnotations;

namespace CadastroCaminhao.Models.Entity
{
    public class Caminhao
    {
        public int Id { get; set; }
        public int ModeloId { get; set; }

        [Display(Name = "Ano de Fabricação")]
        public string AnoFabricacao { get; set; }

        [Display(Name = "Ano do Modelo")]
        public string AnoModelo { get; set; }


        public Modelo Modelo { get; set; }
    }
}
