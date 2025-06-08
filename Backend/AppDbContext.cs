using MasFinal.Models;
using MasFinal.Models.Businesses;
using MasFinal.Models.PoliticalOrganisation;
using Microsoft.EntityFrameworkCore;

namespace MasFinal;


public class AppDbContext(DbContextOptions<AppDbContext> options) 
    : DbContext(options)
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Deal> Deals { get; set; }
    
    public DbSet<Bill> Bills { get; set; }
    
    public DbSet<PoliticalOrganisation> PoliticalOrganisations { get; set; }
    public DbSet<Party> Parties { get; set; }
    public DbSet<Movement> Movements { get; set; }
    
    public DbSet<PartyMembership> PartyMemberships { get; set; }
    public DbSet<MovementMembership> MovementMemberships { get; set; }
    
    public DbSet<Business> Businesses { get; set; }
    public DbSet<Worker> Workers { get; set; }
    public DbSet<StaticAttribute> StaticAttributes { get; set; }


    private static string DbPath => Path.Join(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), 
        "mas.db"
        );

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) 
            return;
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<StaticAttribute>()
            .ToTable("StaticAttributes");
        
        // -- PoliticalOrganisation TPT Inheritance --
        modelBuilder.Entity<PoliticalOrganisation>().ToTable("PoliticalOrganisations");
        modelBuilder.Entity<Party>().ToTable("Parties");
        modelBuilder.Entity<Movement>().ToTable("Movements");
        
        
        
        // -- Deal (Association Class) --
        modelBuilder.Entity<Deal>()
            .HasOne(d => d.Recipient)
            .WithMany(p => p.DealsReceived)
            .HasForeignKey(d => d.RecipientId)
            .OnDelete(DeleteBehavior.Restrict); // Keep deal if person is deleted

        modelBuilder.Entity<Deal>()
            .HasOne(d => d.Proposer)
            .WithMany(p => p.DealsProposed)
            .HasForeignKey(d => d.ProposerId)
            .OnDelete(DeleteBehavior.Restrict); // Keep deal if person is deleted

        
        
        // -- Bill (Supporters/Opposers & Proposer) --
        modelBuilder.Entity<Bill>()
            .HasMany(b => b.Supporters)
            .WithMany(p => p.BillsSupported)
            .UsingEntity(j => j.ToTable("BillSupporters"));

        modelBuilder.Entity<Bill>()
            .HasMany(b => b.Opposers)
            .WithMany(p => p.BillsOpposed)
            .UsingEntity(j => j.ToTable("BillOpposers"));

        modelBuilder.Entity<Bill>()
            .HasOne(b => b.Proposer)
            .WithMany(p => p.BillsProposed)
            .HasForeignKey(b => b.ProposerId)
            .OnDelete(DeleteBehavior.SetNull); // Set ProposerId to null if person is deleted

        
        // -- Party Membership  --
        modelBuilder.Entity<PartyMembership>()
            .HasOne(pm => pm.Politician)
            .WithMany(p => p.PartyMemberships)
            .HasForeignKey(pm => pm.PoliticianId);

        modelBuilder.Entity<PartyMembership>()
            .HasOne(pm => pm.Party)
            .WithMany(p => p.Memberships)
            .HasForeignKey(pm => pm.PartyId);

            
        // -- Movement Membership   --
        modelBuilder.Entity<MovementMembership>()
            .HasOne(mvm => mvm.Politician)
            .WithMany(p => p.MovementMemberships)
            .HasForeignKey(mvm => mvm.PoliticianId);
            
        modelBuilder.Entity<MovementMembership>()
            .HasOne(mvm => mvm.Movement)
            .WithMany(mv => mv.Memberships)
            .HasForeignKey(mvm => mvm.MovementId);
            
        
        
        // -- Business & Worker (Composition) --
        modelBuilder.Entity<Business>()
            .HasOne(b => b.Owner)
            .WithMany(p => p.OwnedBusinesses)
            .HasForeignKey(b => b.OwnerId)
            .OnDelete(DeleteBehavior.Cascade); // Remove business if person is deleted

        modelBuilder.Entity<Worker>()
            .HasOne(w => w.Business)
            .WithMany(b => b.Workers)
            .HasForeignKey(w => w.BusinessId)
            .OnDelete(DeleteBehavior.Cascade); // Remove workers if business is deleted
            
        
        
        // -- Constraints --
        
        // -- Unique --
        modelBuilder.Entity<PoliticalOrganisation>().HasIndex(o => o.Name).IsUnique();

        // -- Check --
        // modelBuilder.Entity<Deal>()
        //     .ToTable(tb => tb.HasCheckConstraint("CK_Deal_Participants", "[OligarchId] <> [PoliticianId]"));
        //
        // modelBuilder.Entity<Worker>()
        //     .ToTable(tb => tb.HasCheckConstraint("CK_Worker_Wage", "[Wage] >= [MinimumWage]"));

    }
}