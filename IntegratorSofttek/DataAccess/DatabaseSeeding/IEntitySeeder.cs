using Microsoft.EntityFrameworkCore;

namespace IntegratorSofttek.DataAccess.DatabaseSeeding
{
    public interface IEntitySeeder
    {
        void SeedDatabase(ModelBuilder modelBuilder);

    }
}
