﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DARE.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class npruessnerEEntities : DbContext
    {
        public npruessnerEEntities()
            : base("name=npruessnerEEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<Privilege> Privileges { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<SensorData> SensorDatas { get; set; }
        public virtual DbSet<DARESystem> DARESystems { get; set; }
        public virtual DbSet<User> Users { get; set; }
    
        public virtual int AddPrivilegeToUser(Nullable<long> userID, string privilegeName)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(long));
    
            var privilegeNameParameter = privilegeName != null ?
                new ObjectParameter("PrivilegeName", privilegeName) :
                new ObjectParameter("PrivilegeName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddPrivilegeToUser", userIDParameter, privilegeNameParameter);
        }
    
        public virtual int AuthenticateUser(string username, string hash)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            var hashParameter = hash != null ?
                new ObjectParameter("Hash", hash) :
                new ObjectParameter("Hash", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AuthenticateUser", usernameParameter, hashParameter);
        }
    
        public virtual int CreateUser(string username, string email, string password, byte[] salt, string phoneNumber, string passwordQuestion, string passwordAnswer, Nullable<System.DateTime> dateOfBirth, string firstName, string lastName)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var saltParameter = salt != null ?
                new ObjectParameter("Salt", salt) :
                new ObjectParameter("Salt", typeof(byte[]));
    
            var phoneNumberParameter = phoneNumber != null ?
                new ObjectParameter("PhoneNumber", phoneNumber) :
                new ObjectParameter("PhoneNumber", typeof(string));
    
            var passwordQuestionParameter = passwordQuestion != null ?
                new ObjectParameter("PasswordQuestion", passwordQuestion) :
                new ObjectParameter("PasswordQuestion", typeof(string));
    
            var passwordAnswerParameter = passwordAnswer != null ?
                new ObjectParameter("PasswordAnswer", passwordAnswer) :
                new ObjectParameter("PasswordAnswer", typeof(string));
    
            var dateOfBirthParameter = dateOfBirth.HasValue ?
                new ObjectParameter("DateOfBirth", dateOfBirth) :
                new ObjectParameter("DateOfBirth", typeof(System.DateTime));
    
            var firstNameParameter = firstName != null ?
                new ObjectParameter("FirstName", firstName) :
                new ObjectParameter("FirstName", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("LastName", lastName) :
                new ObjectParameter("LastName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CreateUser", usernameParameter, emailParameter, passwordParameter, saltParameter, phoneNumberParameter, passwordQuestionParameter, passwordAnswerParameter, dateOfBirthParameter, firstNameParameter, lastNameParameter);
        }
    
        public virtual int GetIdFromEmail(string email)
        {
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetIdFromEmail", emailParameter);
        }
    
        public virtual int RemovePrivilegeFromUser(Nullable<long> userID, string name)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(long));
    
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("RemovePrivilegeFromUser", userIDParameter, nameParameter);
        }
    
        public virtual int SetupSystem(string systemName, string homeAddress, string city, string state, string zIP, string description, string familyName, string username, string email, string password, byte[] salt, string phoneNumber, string passwordQuestion, string passwordAnswer, Nullable<System.DateTime> dateOfBirth, string firstName, string lastName)
        {
            var systemNameParameter = systemName != null ?
                new ObjectParameter("SystemName", systemName) :
                new ObjectParameter("SystemName", typeof(string));
    
            var homeAddressParameter = homeAddress != null ?
                new ObjectParameter("HomeAddress", homeAddress) :
                new ObjectParameter("HomeAddress", typeof(string));
    
            var cityParameter = city != null ?
                new ObjectParameter("City", city) :
                new ObjectParameter("City", typeof(string));
    
            var stateParameter = state != null ?
                new ObjectParameter("State", state) :
                new ObjectParameter("State", typeof(string));
    
            var zIPParameter = zIP != null ?
                new ObjectParameter("ZIP", zIP) :
                new ObjectParameter("ZIP", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            var familyNameParameter = familyName != null ?
                new ObjectParameter("FamilyName", familyName) :
                new ObjectParameter("FamilyName", typeof(string));
    
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var saltParameter = salt != null ?
                new ObjectParameter("Salt", salt) :
                new ObjectParameter("Salt", typeof(byte[]));
    
            var phoneNumberParameter = phoneNumber != null ?
                new ObjectParameter("PhoneNumber", phoneNumber) :
                new ObjectParameter("PhoneNumber", typeof(string));
    
            var passwordQuestionParameter = passwordQuestion != null ?
                new ObjectParameter("PasswordQuestion", passwordQuestion) :
                new ObjectParameter("PasswordQuestion", typeof(string));
    
            var passwordAnswerParameter = passwordAnswer != null ?
                new ObjectParameter("PasswordAnswer", passwordAnswer) :
                new ObjectParameter("PasswordAnswer", typeof(string));
    
            var dateOfBirthParameter = dateOfBirth.HasValue ?
                new ObjectParameter("DateOfBirth", dateOfBirth) :
                new ObjectParameter("DateOfBirth", typeof(System.DateTime));
    
            var firstNameParameter = firstName != null ?
                new ObjectParameter("FirstName", firstName) :
                new ObjectParameter("FirstName", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("LastName", lastName) :
                new ObjectParameter("LastName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SetupSystem", systemNameParameter, homeAddressParameter, cityParameter, stateParameter, zIPParameter, descriptionParameter, familyNameParameter, usernameParameter, emailParameter, passwordParameter, saltParameter, phoneNumberParameter, passwordQuestionParameter, passwordAnswerParameter, dateOfBirthParameter, firstNameParameter, lastNameParameter);
        }
    
        public virtual int UpdateUser(string newEmail, string email, string passwordHash, string firstName, string lastName, string phone)
        {
            var newEmailParameter = newEmail != null ?
                new ObjectParameter("NewEmail", newEmail) :
                new ObjectParameter("NewEmail", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var passwordHashParameter = passwordHash != null ?
                new ObjectParameter("PasswordHash", passwordHash) :
                new ObjectParameter("PasswordHash", typeof(string));
    
            var firstNameParameter = firstName != null ?
                new ObjectParameter("FirstName", firstName) :
                new ObjectParameter("FirstName", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("LastName", lastName) :
                new ObjectParameter("LastName", typeof(string));
    
            var phoneParameter = phone != null ?
                new ObjectParameter("Phone", phone) :
                new ObjectParameter("Phone", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateUser", newEmailParameter, emailParameter, passwordHashParameter, firstNameParameter, lastNameParameter, phoneParameter);
        }
    }
}
