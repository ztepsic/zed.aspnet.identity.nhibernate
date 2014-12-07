using System.Linq;
using NHibernate.Mapping.ByCode;

namespace Zed.AspNet.Identity.NHibernate {
    /// <summary>
    /// NHibernate model mapper exension
    /// </summary>
    public static class ModelMapperExension {
        /// <summary>
        /// Adds Asp.Net Identity Domain mappings
        /// </summary>
        /// <returns>Mapping types</returns>
        public static void AddMappings(this ModelMapper modelMapper) {


            var types = from t in typeof(IdentityUserMapping).Assembly.GetTypes()
                        where t.Namespace == typeof(IdentityUserMapping).Namespace
                        select t;
            modelMapper.AddMappings(types);

        }
    }
}
