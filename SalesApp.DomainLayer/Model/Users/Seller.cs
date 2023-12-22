using SalesApp.DomainLayer.Model.Companies;

namespace SalesApp.DomainLayer.Model.Users
{
    internal class Seller : User
    {
        //- Precisa estar vinculado a uma empresa com cnpj valido
        //- Pode adicionar, excluir, arquivas e atualizar produtos
        //- Tem uma quantidade de vendas alcançadas, quantos clientes atendeu e quantos produtos possui
        //- Entra com login e senha
        //- Podem alterar suas senhas, mas nao o login

        private Company _company;
        private int _salesCompleted;
        private int _totalClientsServed;
        private int _numberOfProducts;

        public int SalesCompleted { get { return _salesCompleted; } }
        public int TotalClientsServed { get { return _totalClientsServed; } }
        public int NumberOfProducts { get { return _numberOfProducts; } }

        public Seller(string username, string name, string email, string password, int phone, Company company) 
            : base(username, name, email, password, phone)
        {
            _company = company;
            _salesCompleted = 0;
            _totalClientsServed = 0;
        }

        public void AddNewNumberOfProducts(int products)
        {
            _numberOfProducts += products;
        }

        public void SubtractNumberOfProducts(int products) 
        {
            _numberOfProducts -= products;
        }

        private void UpdateSalesCompleted() 
        {
            _salesCompleted++; 
        }

        private void UpdateTotalClientsServed()
        {
            _totalClientsServed++;
        }

    }
}
