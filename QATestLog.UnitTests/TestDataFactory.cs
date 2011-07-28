using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QATestLog.UnitTests
{
    public class TestDataFactory
    {

		public List<Project> PopulateTestData(List<ProductDefinition> productDefinitionList, List<QATestDefinition> qaTestDefinitionList, int numberOfProjects)
		{
			List<Project> projectList = GenerateTestProjects(numberOfProjects);
			projectList = GenerateTestBuilds(projectList, numberOfProjects);
			projectList = GenerateTestProducts(projectList, productDefinitionList);
			projectList = GenerateTestQATests(projectList, qaTestDefinitionList);

			return projectList;
		}
       
        
        public List<ProductDefinition> GenerateTestProductDefinitionList(int numberOfProducts)
        {
            var masterProductList = new List<ProductDefinition>();
            for (int i = 1; i <= numberOfProducts; i++ )
            {
                masterProductList.Add(new ProductDefinition(Guid.NewGuid(),
                                                                "Product " + i,
                                                                "This is Product Number " + i));
            }
                
            return masterProductList;
        }

        public List<QATestDefinition> GenerateTestQATestDefinitionList(ProductDefinition product, int numberOfTests)
        {
            var masterTestList = new List<QATestDefinition>();
            for (int i = 1; i <= numberOfTests; i++)
            {
                masterTestList.Add(new QATestDefinition(Guid.NewGuid(),
                                                                product.Id,
                                                                product.Name + " Test " + i,
                                                                "This is Test Number " + i + " for the " + product.Name + " Product."));
            }
            return masterTestList;
        }

        public List<Project> GenerateTestProjects(int numberOfProjects)
        {
            List<Project> projectList = new List<Project>(numberOfProjects);
            for(int i = 0; i<numberOfProjects;i++)
            {
                projectList.Add(new Project(Guid.NewGuid(),
                                            "Project" + i,
                                            "Test Project Number " + i,
                                            DateTime.Now));
            }

            return projectList;
        }

        public List<Project> GenerateTestBuilds(List<Project> projectList, int numberOfBuilds)
        {
            foreach (Project pj in projectList)
            {
				for (int i = 1; i <= numberOfBuilds; i++)
                {
					pj.Builds.Add(new Build(Guid.NewGuid(), 
											pj.Id,
											"Build " + i,
											"Test Build Number " + i));
				}
            }

            return projectList;
        }

		public List<Project> GenerateTestProducts(List<Project> projectList, List<ProductDefinition> masterProductList)
		{
			foreach (Project pj in projectList)
			{
				foreach (Build b in pj.Builds)
				{
					foreach (ProductDefinition masterP in masterProductList)
					{
						b.Products.Add(new Product(Guid.NewGuid(), masterP.Id, b.Id, masterP.Name, masterP.Description));
					}
				}
			}

			return projectList;
		}

        public List<Project> GenerateTestQATests(List<Project> projectList,List<QATestDefinition> masterQATestList)
        {
            foreach (Project pj in projectList)
            {
				foreach (Build b in pj.Builds)
				{
					foreach (Product pd in b.Products)
					{
						foreach(QATestDefinition masterQATest in masterQATestList)
                        {
                            if (masterQATest.isActive && masterQATest.MasterProductListId == pd.MasterProductListId)
                            {
                                pd.Tests.Add(new QATest(Guid.NewGuid(),masterQATest.Id, pd.Id, masterQATest.Name,masterQATest.Description,0,""));
                            }
                        }
                    }
                }
            }

            return projectList;
        }
	}

    
    
}
