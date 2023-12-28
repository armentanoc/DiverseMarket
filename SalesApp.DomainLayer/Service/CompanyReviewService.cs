using SalesApp.DomainLayer.DTOs;
using SalesApp.DomainLayer.Model.Companies;
using SalesApp.DomainLayer.Model.Transactions.Reviews;
using SalesApp.DomainLayer.Model.Users;
using SalesApp.Infrastructure.Repositories;
using System.Drawing;

namespace SalesApp.DomainLayer.Service
{
    internal class CompanyReviewService
    {
        public static CompanyReviewDTO GetCompanyReviewDTO(int clientId, int companyId, string review, string? comment)
        { 
            return new CompanyReviewDTO(clientId, companyId, review, comment);
        }

        public static void AddCompanyReview(CompanyReviewDTO companyReviewDTO)
        {
            ReviewCompanyDB.AddCompanyReview(
                companyReviewDTO.ClientId,
                companyReviewDTO.CompanyId,
                companyReviewDTO.Review,
                companyReviewDTO.Comment
                );
        }

        internal static CompanyReview FakeCompanyReviewData()
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

            var companyReview = new CompanyReview(
                ReviewEnum.Excelente,
                fakeCompany,
                fakeCustomer,
                fakeComment
                );

            return companyReview;

        }
    }
}
