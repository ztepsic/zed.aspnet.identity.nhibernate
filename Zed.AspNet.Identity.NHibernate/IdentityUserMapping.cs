using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Zed.AspNet.Identity.NHibernate {
    class IdentityUserMapping : ClassMapping<IdentityUser> {

        public IdentityUserMapping() {
            Table("Users");

            Id(x => x.Id, m => m.Generator(Generators.Native));

            Property(x => x.UserName, m => m.NotNullable(true));
            Property(x => x.Email);
            Property(x => x.EmailConfirmed, m => m.NotNullable(true));
            Property(x => x.PasswordHash);
            Property(x => x.SecurityStamp);
            Property(x => x.PhoneNumber);
            Property(x => x.PhoneNumberConfirmed, m => m.NotNullable(true));
            Property(x => x.TwoFactorEnabled, m => m.NotNullable(true));
            Property(x => x.LockoutEndDateUtc);
            Property(x => x.LockoutEnabled, m => m.NotNullable(true));
            Property(x => x.AccessFailedCount, m => m.NotNullable(true));

            Set(x => x.Roles, c => {
                c.Table("UserRoles");
                //c.Schema("schemaName");

                c.Access(Accessor.Field);

                c.Fetch(CollectionFetchMode.Join);
                //c.BatchSize(100);
                c.Lazy(CollectionLazy.NoLazy);

                c.Key(k => {
                    k.Column("UserId");
                    k.NotNullable(true);
                    k.OnDelete(OnDeleteAction.NoAction);
                    k.Unique(true);
                });

                c.Cascade(Cascade.All);
                c.Inverse(true);
                c.OrderBy(x => x.Name);

            }, r => r.ManyToMany(m => {
                m.Column("RoleId");
                m.Lazy(LazyRelation.NoLazy);
                m.NotFound(NotFoundMode.Exception);
                m.Class(typeof(IdentityRole));
                
            }));

            Set(x => x.Logins, c => {
                c.Table("UserLogins");
                //c.Schema("schemaName");

                c.Access(Accessor.Field);

                c.Key(k => {
                    k.Column("UserId");
                    k.NotNullable(true);
                });

                c.Fetch(CollectionFetchMode.Join);
                //c.BatchSize(100);
                c.Lazy(CollectionLazy.NoLazy);

            }, r => r.Component(c => {
                c.Property(x => x.LoginProvider, m => {
                    m.Access(Accessor.Field);
                    m.NotNullable(true);
                });
                c.Property(x => x.ProviderKey, m => {
                    m.Access(Accessor.Field);
                    m.NotNullable(true);
                });
            }));

            Set(x => x.Claims, c => {
                c.Table("UserClaims");
                //c.Schema("schemaName");

                c.Access(Accessor.Field);

                c.Key(k => {
                    k.Column("UserId");
                    k.NotNullable(true);
                });

                c.Fetch(CollectionFetchMode.Join);
                //c.BatchSize(100);
                c.Lazy(CollectionLazy.NoLazy);

            }, r => r.Component(c => {
                c.Property(x => x.ClaimType, m => {
                    m.Access(Accessor.Field);
                    m.NotNullable(true);
                });
                c.Property(x => x.ClaimValue, m => {
                    m.Access(Accessor.Field);
                    m.NotNullable(true);
                });
            }));

        }

    }
}
