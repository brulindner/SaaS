using SaaS.Domain.Common;


namespace SaaS.Domain.Entities
{
    public class Cliente : BaseEntity

    {
        public string Nome { get; set; }  = null!; //Falta Endereço
        public string Cpf { get; set; }  = null!;
        public string Telefone { get; set; }  = null!;
        public string Email { get; set; }  = null!;

        public ICollection<Orcamento> Orcamentos { get; set; } = new List<Orcamento>();



    }
}