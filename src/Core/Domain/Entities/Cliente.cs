using SaaS.Domain.Common;


namespace SaaS.Domain.Entities
{
    public class Cliente : BaseEntity

    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public Guid TentantId { get; set; }

    }
}