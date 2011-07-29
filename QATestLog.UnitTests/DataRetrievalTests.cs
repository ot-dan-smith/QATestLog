using System;
using System.Collections.Generic;
using NHibernate;
using NUnit.Framework;
using QATestLog.Mapping;

namespace QATestLog.UnitTests
{
	[TestFixture]
	public class DataRetrievalTests
	{
		//more of an integration test, not really a unit test, because it is hitting the database
		private List<Project> _projectList;
		private ISessionFactory _sessionFactory;
		private Project _firstProject;
		private Build _firstBuild;
		private Product _firstProduct;
		private QATest _firstQATest;

		[TestFixtureSetUp]
		public void Init()
		{
			TestDataFactory testDataFac = new TestDataFactory();

			_sessionFactory = SessionFactoryFactory.CreateSessionFactory();
			
			var productDefinitionList = testDataFac.GenerateTestProductDefinitionList(1);
			var qaTestDefinitionList = new List<QATestDefinition>();

			foreach (ProductDefinition product in productDefinitionList)
			{
				qaTestDefinitionList.AddRange(testDataFac.GenerateTestQATestDefinitionList(product, 1));
			}

			_projectList = testDataFac.PopulateTestData(productDefinitionList, qaTestDefinitionList, 1);
			_firstProject = _projectList[0];
			_firstBuild = _firstProject.Builds[0];
			_firstProduct = _firstBuild.Products[0];
			_firstQATest = _firstProduct.Tests[0];


			using (var session = _sessionFactory.OpenSession())
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
					foreach (Project pj in _projectList)
					{
						session.Save(pj);
					}

					transaction.Commit();
				}
			}
		}

		[TestFixtureTearDown]
		public void FixtureTearDown()
		{
			_sessionFactory.Dispose();
			_projectList = null;
		}

		[Test]
		public void GetProjectShouldLoadTheCorrectProject()
		{
			var dr = new DataRetrieval();
			var targetProject = dr.GetProject(_firstProject.Id);

			Assert.AreEqual(targetProject.Id,		
							_firstProject.Id);
			Assert.AreEqual(targetProject.Name,		
							_firstProject.Name);
			Assert.AreEqual(targetProject.CreatedOn.ToShortTimeString(), 
							_firstProject.CreatedOn.ToShortTimeString());
		}

		[Test]
		public void GetBuildLoadsTheCorrectBuild()
		{
			var dr = new DataRetrieval();
			var targetBuild = dr.GetBuild(_firstBuild.Id);

			Assert.AreEqual(targetBuild.Id, 
							_firstBuild.Id);
			Assert.AreEqual(targetBuild.Name, 
							_firstBuild.Name);
			Assert.AreEqual(targetBuild.Description, 
							_firstBuild.Description);
			Assert.AreEqual(targetBuild.ProjectId, 
							_firstBuild.ProjectId);
		}

		[Test]
		public void GetProductLoadsTheCorrectProduct()
		{
			var dr = new DataRetrieval();
			var targetProduct = dr.GetProduct(_firstProduct.Id);

			Assert.AreEqual(targetProduct.Id,
							_firstProduct.Id);
			Assert.AreEqual(targetProduct.Name, 
							_firstProduct.Name);
			Assert.AreEqual(targetProduct.Description, 
							_firstProduct.Description);
			Assert.AreEqual(targetProduct.BuildId, 
							_firstProduct.BuildId);
			Assert.AreEqual(targetProduct.MasterProductListId, 
							_firstProduct.MasterProductListId);
		}

		[Test]
		public void GetQATestLoadsTheCorrectQATest()
		{
			var dr = new DataRetrieval();
			var targetQATest = dr.GetQATest(_firstQATest.Id);
			
			Assert.AreEqual(targetQATest.Id,
							_firstQATest.Id);
			Assert.AreEqual(targetQATest.Name, 
							_firstQATest.Name);
			Assert.AreEqual(targetQATest.Description, 
							_firstQATest.Description);
			Assert.AreEqual(targetQATest.ProductId, 
							_firstQATest.ProductId);
			Assert.AreEqual(targetQATest.MasterTestListId, 
							_firstQATest.MasterTestListId);
		}

		[Test]
		public void GetQATestsForProductShouldReturnAllOfTheTestsForASpecificProduct()
		{
			var dr = new DataRetrieval();
			var targetQATestList = dr.GetQATestsForProduct(_firstProduct.Id);

			Assert.AreEqual(targetQATestList.Count, 
							_firstProduct.Tests.Count);		
		}

		[Test]
		public void GetProductsForBuildShouldReturnAllOfTheProductsForASpecificBuild()
		{
			var dr = new DataRetrieval();
			var targetQATestList = dr.GetProductsForBuild(_firstBuild.Id);

			Assert.AreEqual(targetQATestList.Count, 
							_firstProduct.Tests.Count);
		}

		[Test]
		public void GetBuildsForProjectShouldReturnAllOfTheBuildsForASpecificProject()
		{
			var dr = new DataRetrieval();
			var targetQATestList = dr.GetBuildsForProject(_firstProject.Id);

			Assert.AreEqual(targetQATestList.Count, _firstProduct.Tests.Count);
		}
	}
}
