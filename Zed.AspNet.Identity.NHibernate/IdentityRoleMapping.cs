using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Zed.AspNet.Identity.NHibernate {
    class IdentityRoleMapping : ClassMapping<IdentityRole> {
        public IdentityRoleMapping() {
            Table("Roles");

            Id(x => x.Id, m => m.Generator(Generators.Native));
            Property(x => x.Name, m => m.NotNullable(true));
        }
    }
}
