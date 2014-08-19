﻿using MvcTemplate.Objects;
using MvcTemplate.Tests.Objects;
using NUnit.Framework;
using System;

namespace MvcTemplate.Tests.Helpers
{
    public class ObjectFactory
    {
        public static String TestId
        {
            get
            {
                return TestContext.CurrentContext.Test.Name;
            }
        }

        public static Account CreateAccount(Int32 instanceNumber = 1)
        {
            return new Account()
            {
                Id = TestId + instanceNumber.ToString(),
                Username = "Username" + TestId + instanceNumber.ToString(),
                Passhash = "$2a$04$zNgYw403HgH1N69j4kj/peGI7SUvGiR5awIPZ2Yh/6O5BwyUO3qZe", // Password1
                Email = TestId + instanceNumber.ToString() + "@tests.com",

                RecoveryToken = TestId + instanceNumber.ToString(),
                RecoveryTokenExpirationDate = DateTime.Now.AddMinutes(5)
            };
        }
        public static AccountView CreateAccountView(Int32 instanceNumber = 1)
        {
            return new AccountView()
            {
                Id = TestId + instanceNumber.ToString(),
                Username = "Username" + TestId + instanceNumber.ToString(),
                Email = TestId + instanceNumber.ToString() + "@tests.com",
                Password = "Password1"
            };
        }
        public static ProfileEditView CreateProfileEditView(Int32 instanceNumber = 1)
        {
            return new ProfileEditView()
            {
                Id = TestId + instanceNumber.ToString(),
                Username = "Username" + TestId + instanceNumber.ToString(),
                Email = TestId + instanceNumber.ToString() + "@tests.com",
                NewPassword = "NewPassword1",
                Password = "Password1"
            };
        }
        public static AccountEditView CreateAccountEditView(Int32 instanceNumber = 1)
        {
            return new AccountEditView()
            {
                Id = TestId + instanceNumber.ToString(),
                Username = "Username" + TestId + instanceNumber.ToString(),
                RoleName = "Name" + TestId + instanceNumber.ToString(),
                RoleId = TestId + instanceNumber.ToString(),
            };
        }
        public static AccountLoginView CreateAccountLoginView(Int32 instanceNumber = 1)
        {
            return new AccountLoginView()
            {
                Id = TestId + instanceNumber.ToString(),
                Username = "Username" + TestId + instanceNumber.ToString(),
                Password = "Password1"
            };
        }
        public static AccountRecoveryView CreateAccountRecoveryView(Int32 instanceNumber = 1)
        {
            return new AccountRecoveryView()
            {
                Id = TestId + instanceNumber.ToString(),
                Email = TestId + instanceNumber.ToString() + "@tests.com"
            };
        }
        public static AccountResetView CreateAccountResetView(Int32 instanceNumber = 1)
        {
            return new AccountResetView()
            {
                Id = TestId + instanceNumber.ToString(),

                Token = TestId + instanceNumber.ToString(),
                NewPassword = "NewPassword1"
            };
        }

        public static Role CreateRole(Int32 instanceNumber = 1)
        {
            return new Role()
            {
                Id = TestId + instanceNumber.ToString(),
                Name = "Name" + TestId + instanceNumber.ToString()
            };
        }
        public static RoleView CreateRoleView(Int32 instanceNumber = 1)
        {
            return new RoleView()
            {
                Id = TestId + instanceNumber.ToString(),
                Name = "Name" + TestId + instanceNumber.ToString()
            };
        }

        public static RolePrivilege CreateRolePrivilege(Int32 instanceNumber = 1)
        {
            return new RolePrivilege()
            {
                Id = TestId + instanceNumber.ToString()
            };
        }
        public static RolePrivilegeView CreateRolePrivilegeView(Int32 instanceNumber = 1)
        {
            return new RolePrivilegeView()
            {
                Id = TestId + instanceNumber.ToString()
            };
        }

        public static Privilege CreatePrivilege(Int32 instanceNumber = 1)
        {
            return new Privilege()
            {
                Id = TestId + instanceNumber.ToString(),
                Area = "Area" + instanceNumber.ToString(),
                Action = "Action" + instanceNumber.ToString(),
                Controller = "Controller" + instanceNumber.ToString()
            };
        }
        public static PrivilegeView CreatePrivilegeView(Int32 instanceNumber = 1)
        {
            return new PrivilegeView()
            {
                Id = TestId + instanceNumber.ToString(),
                Controller = "Controller",
                Action = "Action",
                Area = "Area"
            };
        }

        public static TestModel CreateTestModel(Int32 instanceNumber = 1)
        {
            return new TestModel()
            {
                Id = TestId + instanceNumber.ToString(),
                Text = "Text" + instanceNumber.ToString()
            };
        }
    }
}
