using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace QATestLog.Mapping
{
    public class SessionFactoryFactory
    {
        public static ISessionFactory CreateSessionFactory()
        {
            var config = Fluently.Configure();
            config = config.Database(MsSqlConfiguration.MsSql2008.ConnectionString("Data Source=(local);Initial Catalog=QATestLog;Integrated Security=SSPI;"));
            config = config.Mappings(m => m.FluentMappings.AddFromAssemblyOf<QATestMap>());
            return config.BuildSessionFactory();
        }

        public static ISessionFactory CreateSessionFactoryInMemory()
        {
            return
                Fluently
                    .Configure()
                    .Database(SQLiteConfiguration.Standard.InMemory().ShowSql())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<SessionFactoryFactory>())
                    .ExposeConfiguration((c) => SavedConfig = c)
                    .BuildSessionFactory();
        }

        private static Configuration SavedConfig;

        public static void BuildSchema(ISession session)
        {
            var export = new SchemaExport(SavedConfig);
            export.Execute(true, true, false, session.Connection, null);
        }  
    }
}
