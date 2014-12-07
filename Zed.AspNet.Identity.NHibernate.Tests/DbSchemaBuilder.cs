using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Zed.NHibernate;

namespace Zed.AspNet.Identity.NHibernate.Tests {
    //[TestFixture]
    public class DbSchemaBuilder : SQLiteNHibernateTestFixture {
        //[Test]
        public void Build() {

            var cfg = NHibernateSessionProvider.Configuration;
            var schemaExport = new SchemaExport(cfg);
            schemaExport.Create(true, true);
            schemaExport.Execute(false, true, false, Session.Connection, null);
        }
    }
}
