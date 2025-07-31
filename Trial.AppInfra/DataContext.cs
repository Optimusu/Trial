using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Trial.Domain.EntitesSoftSec;
using Trial.Domain.Entities;
using Trial.Domain.EntitiesGen;
using Trial.Domain.EntitiesStudy;

namespace Trial.AppInfra;

public class DataContext : IdentityDbContext<User>
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    //EntitiesSoftSec

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<UsuarioRole> UsuarioRoles => Set<UsuarioRole>();

    //Manejo de UserRoles por Usuario

    public DbSet<UserRoleDetails> UserRoleDetails => Set<UserRoleDetails>();

    //Entities

    public DbSet<Country> Countries => Set<Country>();
    public DbSet<State> States => Set<State>();
    public DbSet<City> Cities => Set<City>();
    public DbSet<SoftPlan> SoftPlans => Set<SoftPlan>();
    public DbSet<Manager> Managers => Set<Manager>();
    public DbSet<Corporation> Corporations => Set<Corporation>();

    //EntitiesGen

    public DbSet<DocumentType> DocumentTypes => Set<DocumentType>();
    public DbSet<TherapeuticArea> TherapeuticAreas => Set<TherapeuticArea>();
    public DbSet<Indication> Indications => Set<Indication>();
    public DbSet<Sponsor> Sponsors => Set<Sponsor>();
    public DbSet<Enrolling> Enrollings => Set<Enrolling>();
    public DbSet<Irb> Irbs => Set<Irb>();
    public DbSet<Cro> Cros => Set<Cro>();

    //EntitiesGen

    public DbSet<Study> Studies => Set<Study>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Para tomar los calores de ConfigEntities
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}