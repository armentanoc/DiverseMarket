using SalesApp.DomainLayer.DTOs;
using SalesApp.DomainLayer.Model.Companies;
using SalesApp.DomainLayer.Model.Transactions.Reviews;
using SalesApp.DomainLayer.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.DomainLayer.Service
{
    internal class Test
    {
        internal static void TrySavingCompanyReview()
        {
            var fakeAddress = new Address(
                 zipCode: "12345-678",
                 street: "Fake Street",
                 neighborhood: "Fake Neighborhood",
                 city: "Fake City",
                 state: "FS",
                 complement: "Apt 123");

            var fakeCompany = new Company(
            username: "fakecompany",
            name: "Fake Company",
            email: "fakecompany@example.com",
            password: "fakepassword",
            phone: 123456789,
            address: fakeAddress,
            cnpj: "12345678901234",
            corporateName: "Fake Corp",
            tradeName: "Fake Trade"
        );

            var fakeCustomer = new Customer(
           username: "fakecustomer",
           name: "Fake Customer",
           email: "fakecustomer@example.com",
           password: "fakepassword",
           phone: 987654321,
           role: RolesEnum.Client,
           address: fakeAddress
       );

            string fakeComment = "Excelente! Muito bom";

            // Example CompanyReviewDTO
            var companyReview = new CompanyReview(
                ReviewEnum.Excelente,
                fakeCompany,
                fakeCustomer,
                fakeComment
                );

            // Create an instance of ReviewRepository and save the data
            var companyReviewDTO = new CompanyReviewDTO(companyReview);
            companyReviewDTO.Create();

            Console.WriteLine("Dados salvos com sucesso em ReviewCompany!");
        }
    }
}
