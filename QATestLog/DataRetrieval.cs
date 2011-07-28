using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;
using QATestLog.Mapping;

namespace QATestLog
{
	public class DataRetrieval
	{
		#region Project Functions

		public Project GetProject(Guid id)
		{
			Project result = null;

			ISessionFactory sessionFactory = SessionFactoryFactory.CreateSessionFactory();
			using (var session = sessionFactory.OpenSession())
			{
				result = session.Get<Project>(id);
			}
			return result;
		}
		
		public IList<Project> GetExistingProjects()
		{
			IList<Project> result = null;

			ISessionFactory sessionFactory = SessionFactoryFactory.CreateSessionFactory();
			using (var session = sessionFactory.OpenSession())
			{
				result = session.CreateCriteria(typeof(Project)).List<Project>();
				//result = session.Query<Project>().Fetch(p => p).ToList();
			}

			return result;
		}

		public IList<Project> LoadProjectFull(Guid id)
		{
			IList<Project> result = null;

			ISessionFactory sessionFactory = SessionFactoryFactory.CreateSessionFactory();
			using (var session = sessionFactory.OpenSession())
			{
				result = session.Query<Project>()
					.Where(pj => pj.Id == id)
					.FetchMany(pj => pj.Builds)
						.ThenFetchMany(b => b.Products).ToList();
			}
			return result;
		}
		#endregion

		#region Prodcut Functions

		public Product GetProduct(Guid productId)
		{
			Product result = null;

			ISessionFactory sessionFactory = SessionFactoryFactory.CreateSessionFactory();
			using (var session = sessionFactory.OpenSession())
			{
				//result = (from prod in session.Query<Product>()
				//          where prod.Id == id
				//          select prod).Single();
				result = session.Get<Product>(productId);

			}
			return result;
		}
		public IList<Product> GetProductsForBuild(Guid buildId)
		{
			IList<Product> result = null;

			ISessionFactory sessionFactory = SessionFactoryFactory.CreateSessionFactory();
			using (var session = sessionFactory.OpenSession())
			{
				result = (from b in session.Query<Product>()
						  where b.BuildId == buildId
						  select b).ToList();

			}
			return result;
		}

		#endregion

		#region Build Functions

		public Build GetBuild(Guid buildId)
		{
			Build result = null;

			ISessionFactory sessionFactory = SessionFactoryFactory.CreateSessionFactory();
			using (var session = sessionFactory.OpenSession())
			{
				//result = (from prod in session.Query<Product>()
				//          where prod.Id == id
				//          select prod).Single();
				result = session.Get<Build>(buildId);

			}
			return result;
		}
		public IList<Build> GetBuildsForProject(Guid projectId)
		{
			IList<Build> result = null;

			ISessionFactory sessionFactory = SessionFactoryFactory.CreateSessionFactory();
			using (var session = sessionFactory.OpenSession())
			{
				result = (from b in session.Query<Build>()
				          where b.ProjectId == projectId
				          select b).ToList();

			}
			return result;
		}

		#endregion

		#region QATest

		public QATest GetQATest(Guid id)
		{
			QATest result = null;

			ISessionFactory sessionFactory = SessionFactoryFactory.CreateSessionFactory();
			using (var session = sessionFactory.OpenSession())
			{
				result = session.Get<QATest>(id);

			}
			return result;
		}
		public IList<QATest> GetQATestsForProduct(Guid productId)
		{
			IList<QATest> result = null;

			ISessionFactory sessionFactory = SessionFactoryFactory.CreateSessionFactory();
			using (var session = sessionFactory.OpenSession())
			{
				result = (from b in session.Query<QATest>()
						  where b.ProductId == productId
						  select b).ToList();

			}
			return result;
		}

		#endregion
	}
}
