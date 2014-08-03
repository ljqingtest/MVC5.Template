﻿using AutoMapper;
using MvcTemplate.Components.Logging;
using MvcTemplate.Data.Core;
using MvcTemplate.Objects;
using MvcTemplate.Tests.Data;
using MvcTemplate.Tests.Helpers;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace MvcTemplate.Tests.Unit.Data.Core
{
    [TestFixture]
    public class UnitOfWorkTests
    {
        private UnitOfWork unitOfWork;
        private AContext context;

        [SetUp]
        public void SetUp()
        {
            context = new TestingContext();
            unitOfWork = new UnitOfWork(context);
        }

        [TearDown]
        public void TearDown()
        {
            unitOfWork.Dispose();
        }

        #region Method: Repository<TModel>()

        [Test]
        public void Repository_UsesContextsRepository()
        {
            Assert.AreSame(context.Repository<Account>(), unitOfWork.Repository<Account>());
        }

        #endregion

        #region Method: ToModel<TView, TModel>(TView view)

        [Test]
        public void ToModel_ConvertsViewToModel()
        {
            AccountView view = ObjectFactory.CreateAccountView();
            Account expected = Mapper.Map<AccountView, Account>(view);
            Account actual = unitOfWork.ToModel<AccountView, Account>(view);

            TestHelper.PropertyWiseEquals(expected, actual);
        }

        #endregion

        #region Method: ToView<TModel, TView>(TModel model)

        [Test]
        public void ToView_ConvertsModelToView()
        {
            Account model = ObjectFactory.CreateAccount();
            model.Role = ObjectFactory.CreateRole();
            model.RoleId = model.Role.Id;

            AccountView expected = Mapper.Map<Account, AccountView>(model);
            AccountView actual = unitOfWork.ToView<Account, AccountView>(model);

            TestHelper.PropertyWiseEquals(expected, actual);
        }

        #endregion

        #region Method: Rollback()

        [Test]
        public void RollBack_RollbacksChanges()
        {
            Account model = ObjectFactory.CreateAccount();
            context.Set<Account>().Add(model);

            unitOfWork.Rollback();
            unitOfWork.Commit();

            Assert.IsNull(unitOfWork.Repository<Account>().GetById(model.Id));
        }

        #endregion

        #region Method: Commit()

        [Test]
        public void Commit_SavesChanges()
        {
            Account expected = ObjectFactory.CreateAccount(2);
            unitOfWork.Repository<Account>().Insert(expected);
            unitOfWork.Commit();

            Account actual = unitOfWork.Repository<Account>().GetById(expected.Id);
            unitOfWork.Repository<Account>().Delete(expected.Id);
            unitOfWork.Commit();

            TestHelper.PropertyWiseEquals(expected, actual);
        }

        [Test]
        public void Commit_LogsEntities()
        {
            IEntityLogger logger = Substitute.For<IEntityLogger>();
            unitOfWork = new UnitOfWork(context, logger);
            unitOfWork.Commit();

            logger.Received().Log(Arg.Any<IEnumerable<DbEntityEntry>>());
            logger.Received().Save();
        }

        [Test]
        public void Commit_DoesNotSaveLogsOnFailedCommit()
        {
            IEntityLogger logger = Substitute.For<IEntityLogger>();
            unitOfWork = new UnitOfWork(context, logger);

            try
            {
                unitOfWork.Repository<Account>().Insert(new Account());
                unitOfWork.Commit();
            }
            catch
            {
            }

            logger.Received().Log(Arg.Any<IEnumerable<DbEntityEntry>>());
            logger.DidNotReceive().Save();
        }

        #endregion

        #region Method: Dispose()

        [Test]
        public void Dispose_DiposesLogger()
        {
            IEntityLogger logger = Substitute.For<IEntityLogger>();
            UnitOfWork unitOfWork = new UnitOfWork(new TestingContext(), logger);

            unitOfWork.Dispose();

            logger.Received().Dispose();
        }

        [Test]
        public void Dispose_DiposesContext()
        {
            AContext context = Substitute.For<AContext>();
            UnitOfWork unitOfWork = new UnitOfWork(context);

            unitOfWork.Dispose();

            context.Received().Dispose();
        }

        [Test]
        public void Dispose_CanBeCalledMultipleTimes()
        {
            unitOfWork.Dispose();
            unitOfWork.Dispose();
        }

        #endregion
    }
}
