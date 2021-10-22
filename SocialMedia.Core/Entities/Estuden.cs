using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SocialMedia.Core.Entities
{
    public class Estuden : BaseEntity
    {

        public Estuden()
        {
            Comments = new HashSet<Comment>();
        }

        public int IdEstudiante { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Materia { get; set; }
        public string Nota1 { get; set; }
        public string Nota2 { get; set; }
        public string Nota3 { get; set; }


        //public virtual User User   { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }


    }
}
