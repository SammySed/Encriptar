using Microsoft.EntityFrameworkCore;
using TrabalhoAluno.Models;

namespace TrabalhoAluno.Data
{
    public class AppCont : DbContext
    {
        public AppCont(DbContextOptions<AppCont> options) : base(options)
        {

        }

        public DbSet<Aluno> Aluno { get; set; }
    }
}
