using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<AppUser> Users { get; set; }
    public DbSet<BillingInfo> Billing { get; set; }
    public DbSet<MedicationInfo> Medication { get; set; }
    public DbSet<TriageInfo> Triage { get; set; }
    public DbSet<TestInfo> Test { get; set; }

    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Appointment>()
            .HasKey(k => new { k.SourceUserId, k.AskedUserId });
        builder.Entity<Appointment>()
            .HasOne(s => s.SourceUser)
            .WithMany(l => l.AppointmentAsked)
            .HasForeignKey(s => s.SourceUserId)
            .OnDelete(DeleteBehavior.Cascade);
        base.OnModelCreating(builder);


        builder.Entity<Appointment>()
            .HasOne(s => s.AskedUser)
            .WithMany(l => l.AppointmentReceived)
            .HasForeignKey(s => s.AskedUserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Message>()
            .HasOne(u => u.Recipient)
            .WithMany(m => m.MessagesReceived)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Message>()
            .HasOne(u => u.Sender)
            .WithMany(m => m.MessagesSent)
            .OnDelete(DeleteBehavior.Restrict);
    }
}