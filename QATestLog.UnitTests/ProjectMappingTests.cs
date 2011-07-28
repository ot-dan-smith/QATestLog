using System;
using System.Collections.Generic;
using NHibernate;
using NUnit.Framework;
using QATestLog.Mapping;

namespace QATestLog.UnitTests
{
    [TestFixture]
    public class ProjectMappingTests
    {
        //more of an integration test, not really a unit test
        [Test]
        public void ProjectCascadeIsStoredToDatabaseOnSave()
        {
            ISessionFactory sessionFactory = SessionFactoryFactory.CreateSessionFactory(); 
            TestDataFactory testDataFac = new TestDataFactory();

        	var productDefinitionList = testDataFac.GenerateTestProductDefinitionList(4);
        	var qaTestDefinitionList = new List<QATestDefinition>();

			foreach (ProductDefinition product in productDefinitionList)
			{
				qaTestDefinitionList.AddRange(testDataFac.GenerateTestQATestDefinitionList(product, 3));
			}

			List<Project> projectList = testDataFac.PopulateTestData(productDefinitionList,qaTestDefinitionList,2);

			using (var session = sessionFactory.OpenSession())
			{
				using (var transaction = session.BeginTransaction())
				{
					foreach (ProductDefinition pd in productDefinitionList)
					{
						session.Save(pd);
					}
					foreach (QATestDefinition qad in qaTestDefinitionList)
					{
						session.Save(qad);
					}
					foreach(Project pj in projectList)
					{
					    session.Save(pj);
					}
                    transaction.Commit();
				}
			}

			using (var session = sessionFactory.OpenSession())
			{
				using (var transaction = session.BeginTransaction())
				{
					var projects = session.CreateCriteria(typeof(Project)).List<Project>();
				}
			}

        }

		[Test]
		public void LoadProjectLoadsCorrectProject()
		{
			ISessionFactory sessionFactory = SessionFactoryFactory.CreateSessionFactory();
			TestDataFactory testDataFac = new TestDataFactory();

			var productDefinitionList = testDataFac.GenerateTestProductDefinitionList(4);
			var qaTestDefinitionList = new List<QATestDefinition>();

			foreach (ProductDefinition product in productDefinitionList)
			{
				qaTestDefinitionList.AddRange(testDataFac.GenerateTestQATestDefinitionList(product, 3));
			}

			List<Project> projectList = testDataFac.PopulateTestData(productDefinitionList, qaTestDefinitionList, 1);

			using (var session = sessionFactory.OpenSession())
			{
				using (var transaction = session.BeginTransaction())
				{
					foreach (ProductDefinition pd in productDefinitionList)
					{
						session.Save(pd);
					}
					foreach (QATestDefinition qad in qaTestDefinitionList)
					{
						session.Save(qad);
					}
					foreach (Project pj in projectList)
					{
						session.Save(pj);
					}
					transaction.Commit();
				}
			}

			DataRetrieval dr = new DataRetrieval();
			var loadedProj = dr.GetProject(projectList[0].Id);
		}

    	[Test]
        public void ProjectIsStoredToDatabaseOnSave()
        {
            ISessionFactory sessionFactory = SessionFactoryFactory.CreateSessionFactory();
            Project TestProject = new Project(Guid.NewGuid(),"StandAlone Test Project 1","This is the Project that has no products or builds or tests.",DateTime.Now);

            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    
                    session.Save(TestProject);
                    
                    transaction.Commit();
                }
            }

        }

		[Test]
		public void BuildIsStoredToDatabaseOnSave()
		{ 
			TestDataFactory tdf = new TestDataFactory();
			ISessionFactory sessionFactory = SessionFactoryFactory.CreateSessionFactory();
			

			var masterProductItem = tdf.GenerateTestProductDefinitionList(1)[0];
			var testProject = new Project(Guid.NewGuid(), "StandAlone Project 2","This is the project for the standalone build test.",DateTime.Now);
			var testBuild = new Build(Guid.NewGuid(), testProject.Id, "StandAlone Build 1", "This is the build that has no products or tests.");


			using (var session = sessionFactory.OpenSession())
			{
				using (var transaction = session.BeginTransaction())
				{
					session.Save(masterProductItem);
					session.Save(testProject);
					session.Save(testBuild);

					transaction.Commit();
				}
			}

		}

		[Test]
		public void ProductIsStoredToDatabaseOnSave()
		{
			TestDataFactory tdf = new TestDataFactory();
			ISessionFactory sessionFactory = SessionFactoryFactory.CreateSessionFactory();


			var masterProductItem = tdf.GenerateTestProductDefinitionList(1)[0];
			var testProject = new Project(Guid.NewGuid(), "StandAlone Project 3", "This is the project for the standalone build test.", DateTime.Now);
			var testBuild = new Build(Guid.NewGuid(), testProject.Id, "StandAlone Build 2", "This is the build for the standalone product test.");
			var testProduct = new Product(Guid.NewGuid(), masterProductItem.Id, testBuild.Id, "StandAlone Test Product 1", "This is the Product and has no tests.");

			using (var session = sessionFactory.OpenSession())
			{
				using (var transaction = session.BeginTransaction())
				{
					session.Save(masterProductItem);
					session.Save(testProject);
					session.Save(testBuild);
					session.Save(testProduct);

					transaction.Commit();
				}
			}

		}

		[Test]
		public void QATestIsStoredToDatabaseOnSave()
		{
			TestDataFactory tdf = new TestDataFactory();
			ISessionFactory sessionFactory = SessionFactoryFactory.CreateSessionFactory();


			var masterProductItem = tdf.GenerateTestProductDefinitionList(1)[0];
			var masterQATestItem = tdf.GenerateTestQATestDefinitionList(masterProductItem, 1)[0];

			var testProject = new Project(Guid.NewGuid(), "StandAlone Project 4", "This is the project for the standalone QATest test.", DateTime.Now);
			var testBuild = new Build(Guid.NewGuid(), testProject.Id, "StandAlone Build 3", "This is the build for the standalone QAtest test.");
			var testProduct = new Product(Guid.NewGuid(), masterProductItem.Id, testBuild.Id, "StandAlone Test Product 2", "This is the Product for the standalone QATest test.");
			var testQATest = new QATest(Guid.NewGuid(), masterQATestItem.Id, testProduct.Id, "StandAlone Test QATest 1", "This is the QA test for testing the QA test save.",0,"");

			using (var session = sessionFactory.OpenSession())
			{
				using (var transaction = session.BeginTransaction())
				{
					session.Save(masterProductItem);
					session.Save(masterQATestItem);
					session.Save(testProject);
					session.Save(testBuild); 
					session.Save(testProduct);
					session.Save(testQATest);

					transaction.Commit();
				}
			}

		}
    }
}
