using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Mapping;
using NHibernate;
using NHibernate.SqlAzure;
using NHibernate.Tool.hbm2ddl;

namespace WebLoggR.Code
{
    public class LogMessage
    {
        public virtual Guid Id { get; set; }
        public virtual Guid ApiKey { get; set; }
        public virtual string LogLevel { get; set; }
        public virtual string Message { get; set; }
        public virtual string Title { get; set; }
        public virtual DateTime Time { get; set; }
    }

    public class LogMessageMap : ClassMap<LogMessage>
    {
        public LogMessageMap()
        {
            Id(x => x.Id);
            Map(x => x.ApiKey);
            Map(x => x.LogLevel);
            Map(x => x.Message);
            Map(x => x.Title);
            Map(x => x.Time);
        }

    }

    public class NHibernateHelper
    {
        public static ISessionFactory Session
        {
            get { return Nested.instance; }
        }
        public static void ExecuteTransaction(ISession session, Action a)
        {
            using (var transaction = session.BeginTransaction())
            {
                a.Invoke();
                transaction.Commit();
            }
        }
        private class Nested
        {
            internal static readonly ISessionFactory instance =
                Fluently.Configure()
                    .Database(
                        MsSqlConfiguration.MsSql2008
                            .ConnectionString(
                                @"Server=tcp:l92bw1uhe4.database.windows.net,1433;Database=GreenRDb;User ID=degree@l92bw1uhe4;" +
                                @"Password=Nfalk1988;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;"
                            )
                            .Driver<SqlAzureClientDriver>()
                    )
                    .Mappings(m =>
                        m.FluentMappings.Add<LogMessage>()
                    )
                    
                    //.ExposeConfiguration(BuildSchema)
                    .BuildSessionFactory();

            private static void BuildSchema(NHibernate.Cfg.Configuration config)
            {
                try
                {
                    new SchemaExport(config).Create(false, true);
                }
                catch (Exception e)
                {

                }
            }
            static Nested() { }
        }

        //internal static ISessionFactory CreateSessionFactory()
        //{
        //    ISessionFactory factory = Fluently.Configure()
        //            .Database(
        //                MsSqlConfiguration.MsSql2008
        //                    .ConnectionString(
        //                        @"Server=tcp:l92bw1uhe4.database.windows.net,1433;Database=GreenRDb;User ID=degree@l92bw1uhe4;" +
        //                        @"Password=Nfalk1988;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;"
        //                    )
        //                    .Driver<SqlAzureClientDriver>()
        //            )
        //            .Mappings(m =>
        //                m.FluentMappings.Add<LogMessageMap>()
        //            )
        //            //.ExposeConfiguration(BuildSchema)
        //            .BuildSessionFactory();

        //    return factory; 
        //}
        //private static void BuildSchema(NHibernate.Cfg.Configuration config)
        //{
        //    try
        //    {
        //        new SchemaExport(config).Create(false, true);
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //}
    }
}