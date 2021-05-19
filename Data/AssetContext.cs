using Microsoft.EntityFrameworkCore;
using AssetManagementSystem.Models;

namespace AssetManagementSystem.Data
{
    public class AssetManagementSystemContext : DbContext
    {
        public AssetManagementSystemContext (DbContextOptions<AssetManagementSystemContext> options)
            : base(options)
        {
        }
        public DbSet<BookAsset> BookAssets { get; set; }
        public DbSet<SoftwareAsset> SoftwareAssets { get; set; }
        public DbSet<HardwareAsset> HardwareAssets { get; set; }
    }
}