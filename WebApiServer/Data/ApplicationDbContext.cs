using Microsoft.EntityFrameworkCore;
using SharedData.Models;

namespace WebApiServer.Data;

public class ApplicationDbContext : DbContext
{
    public virtual DbSet<SoundSetting> SoundSettings { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
} 