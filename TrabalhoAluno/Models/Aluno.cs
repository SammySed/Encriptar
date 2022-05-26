using System.ComponentModel.DataAnnotations;

namespace TrabalhoAluno.Models
{
    public class Aluno
    {
        [Key]
        public int Id { get; set; }
        
        public string Nome { get; set; }
        
        public string Email { get; set; }
        
        public string Logradouro { get; set; }
        
        public string Bairro { get; set; }
       
        public string Cidade { get; set; }
        
        public string UF { get; set; }
    }
}
