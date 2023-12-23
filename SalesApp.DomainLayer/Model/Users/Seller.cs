using SalesApp.DomainLayer.Model.Products;

namespace SalesApp.DomainLayer.Model.Users
{
    public class Seller
    {

        //- Precisa estar vinculado a uma empresa com cnpj valido
        //- Pode adicionar, excluir, arquivas e atualizar produtos
        //- Tem uma quantidade de vendas alcançadas, quantos clientes atendeu e quantos produtos possui
        //- Entra com login e senha
        //- Podem alterar suas senhas, mas nao o login
        internal void CompleteSale(ProductOffer product, decimal price)
        {
            throw new NotImplementedException();
        }
    }
}
