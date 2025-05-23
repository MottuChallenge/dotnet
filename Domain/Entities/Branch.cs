﻿using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MottuGrid_Dotnet.Domain.DTO.Request;
using System.Xml.Linq;

namespace MottuGrid_Dotnet.Domain.Entities
{
    public class Branch
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string CNPJ { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public Guid AddressId { get; private set; }
        public Address Address { get; set; }
        public ICollection<Yard> Yards { get; private set; } = new List<Yard>();
        public Branch(string name, string cnpj, Guid addressId, string phone, string email)
        {
            ValidateCNPJ(cnpj);
            ValidateEmail(email);
            ValidatePhone(phone);
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.CNPJ = cnpj;
            this.AddressId = addressId;
            this.Phone = phone;
            this.Email = email;
        }

        public Branch() { }

        private void ValidateCNPJ(string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj))
            {
                throw new ArgumentException("CNPJ must not be null");
            }
            if (cnpj.Length != 14)
            {
                throw new ArgumentException("CNPJ must have 14 characters");
            }
        }

        private void ValidatePhone(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                throw new ArgumentException("Phone must not be null");
            }
            if (phone.Length != 11)
            {
                throw new ArgumentException("Phone must have 11 characters");
            }
        }
        private void ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email must not be null");
            }
            if (!email.Contains("@"))
            {
                throw new ArgumentException("Email must contain @");
            }
            if (!email.Contains("."))
            {
                throw new ArgumentException("Email must contain .");
            }
        }

        public void UpdateBranch(BranchRequest branchRequest)
        {
            if (branchRequest == null)
            {
                throw new ArgumentNullException("Invalid must be not null");
            }
            ValidateCNPJ(branchRequest.CNPJ);
            ValidateEmail(branchRequest.Email);
            ValidatePhone(branchRequest.Phone);
            this.Name = branchRequest.Name;
            this.CNPJ = branchRequest.CNPJ;
            this.Phone = branchRequest.Phone;
            this.Email = branchRequest.Email;

        }
    }
}
