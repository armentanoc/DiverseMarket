namespace SalesApp.DomainLayer.Model.Users
{
    public class Moderator : User
    {
        //- Autoriza vendedores 
        //- Soluciona problemas entre vendedores e compradores
        //- Não pode comprar ou vender dentro do sistema
        //- Entra no sistema com login e senha como todos os usuarios
        //- Cria login e senha temporarios para vendedores
    
        public Moderator(string username, string name, string email, string password, int phone) : base(username, name, email, password, phone) { }

    }
}
