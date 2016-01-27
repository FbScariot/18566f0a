namespace LIFNE.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Menu
    {
        public Menu()
        {
            this.Filhos = new HashSet<Menu>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Codigo { get; set; }
        public string Tipo { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }

        public virtual ApplicationUser AspNetUser { get; set; }
        public virtual Menu MenuPai { get; set; }
        public virtual ICollection<Menu> Filhos { get; set; }
    }
}
