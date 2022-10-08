using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

/*
NAME

    Data Context - is a type of session with the database
    Inherits from the db context class provided from the .NET entity framework
    
SYNOPSIS
    
    ##DATABASE SETS
        public DbSet<AppUser> Users { get; set; }
        public DbSet<BillingInfo> Billing { get; set; }
        public DbSet<MedicationInfo> Medication { get; set; }
        public DbSet<TriageInfo> Triage { get; set; }
        public DbSet<TestInfo> Test { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Message> Messages { get; set; }
        
        ##MODEL BUILDERS 
        protected override void OnModelCreating(ModelBuilder builder)
     
DESCRIPTION
    
    This is class that inherits from the base class "DbContext" provided by the .NET entity framework core.
    This class defines the structure of databases to create an entity framework 
    
    
*/

public class DataContext : DbContext
{
    //Constructor of the data context class 
    //Instantiates the options from db context options class
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    
    //Database set of app users entity
    public DbSet<AppUser> Users { get; set; }
    
    //Database set of billing entity
    public DbSet<BillingInfo> Billing { get; set; }
    
    //Database set of medication entity
    public DbSet<MedicationInfo> Medication { get; set; }
    //Database set of app triage entity
    public DbSet<TriageInfo> Triage { get; set; }
    
    //Database set for the test entity 
    public DbSet<TestInfo> Test { get; set; }

    //Database set for the appointments
    public DbSet<Appointment> Appointments { get; set; }
    
    //Database set for the messages
    public DbSet<Message> Messages { get; set; }

    //Builds a model based on the shape of appointment and Message Classes
    protected override void OnModelCreating(ModelBuilder builder)
    {
        //The Appointment data table has a one to many relationship 
        //One user can have multiple appointment requests to another 
        //Multiple users can request an appointment to the user
        base.OnModelCreating(builder);
        builder.Entity<Appointment>()
            .HasKey(k => new { k.SourceUserId, k.AskedUserId });
        builder.Entity<Appointment>()
            .HasOne(s => s.SourceUser)
            .WithMany(l => l.AppointmentAsked)
            .HasForeignKey(s => s.SourceUserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<Appointment>()
            .HasOne(s => s.AskedUser)
            .WithMany(l => l.AppointmentReceived)
            .HasForeignKey(s => s.AskedUserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        //The Appointment data table has a one to many relationship 
        //One user can have message multiple people requests to another 
        //Multiple users can receive a message from the user
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