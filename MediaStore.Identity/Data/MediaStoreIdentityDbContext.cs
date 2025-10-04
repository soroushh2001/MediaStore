using MediaStore.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MediaStore.Identity.Data
{
    public class MediaStoreIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public MediaStoreIdentityDbContext(DbContextOptions<MediaStoreIdentityDbContext> options) : base(options)
        {
        }
    }
}
