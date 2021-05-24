
using CadastroCaminhao.Models.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CadastroCaminhao.Models.Model
{
    public class CadastroCaminhaoModel
    {
        public Caminhao Caminhao { get; set; }
        public List<SelectListItem> Modelos { get; set; }

       
    }
}
