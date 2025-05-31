using Microsoft.EntityFrameworkCore;
using DeskBookingAPI.Models;
using System;
using System.Collections.Generic;

namespace DeskBookingAPI.Data
{
    public class DeskBookingContext : DbContext
    {
        public DeskBookingContext(DbContextOptions<DeskBookingContext> options) : base(options) { }

        public DbSet<BookingModel> Bookings { get; set; }
        public DbSet<Workspace> Workspaces { get; set; }
        public DbSet<WorkspaceAvailabilityOption> WorkspaceAvailabilityOptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Booking → Workspace
            modelBuilder.Entity<BookingModel>()
                .HasOne(b => b.Workspace)
                .WithMany(w => w.Bookings)
                .HasForeignKey(b => b.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Booking → AvailabilityOption (Room)
            modelBuilder.Entity<BookingModel>()
                .HasOne(b => b.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            // Workspace → AvailabilityOptions
            modelBuilder.Entity<WorkspaceAvailabilityOption>()
                .HasOne(o => o.Workspace)
                .WithMany(w => w.AvailabilityOptions)
                .HasForeignKey(o => o.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Workspaces
            modelBuilder.Entity<Workspace>().HasData(
                new Workspace
                {
                    Id = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"),
                    Name = "Open Space",
                    Type = WorkspaceType.OpenSpace,
                    Capacity = new int[] { },
                    Amenities = new string[] { "Conditioning", "Play", "Wi-Fi", "Coffee" }
                },
                new Workspace
                {
                    Id = Guid.Parse("b2844205-b49b-4cf1-8389-b7cdb0a54372"),
                    Name = "Private Room",
                    Type = WorkspaceType.PrivateRoom,
                    Capacity = new int[] { 1, 2, 5, 10 },
                    Amenities = new string[] { "Wi-Fi", "Conditioning", "Music" }
                },
                new Workspace
                {
                    Id = Guid.Parse("51de8af8-a981-49d9-a276-8144384555bb"),
                    Name = "Meeting Room",
                    Type = WorkspaceType.MeetingRoom,
                    Capacity = new int[] { 10, 20 },
                    Amenities = new string[] { "Wi-Fi", "Conditioning", "Music", "Micro" }
                }
            );

            // Seed WorkspaceAvailabilityOptions for Open Space (24 desks)
            modelBuilder.Entity<WorkspaceAvailabilityOption>().HasData(
                new WorkspaceAvailabilityOption { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new WorkspaceAvailabilityOption { Id = Guid.Parse("11111111-1111-1111-1111-111111111112"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new WorkspaceAvailabilityOption { Id = Guid.Parse("11111111-1111-1111-1111-111111111113"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new WorkspaceAvailabilityOption { Id = Guid.Parse("11111111-1111-1111-1111-111111111114"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new WorkspaceAvailabilityOption { Id = Guid.Parse("11111111-1111-1111-1111-111111111115"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new WorkspaceAvailabilityOption { Id = Guid.Parse("11111111-1111-1111-1111-111111111116"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new WorkspaceAvailabilityOption { Id = Guid.Parse("11111111-1111-1111-1111-111111111117"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new WorkspaceAvailabilityOption { Id = Guid.Parse("11111111-1111-1111-1111-111111111118"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new WorkspaceAvailabilityOption { Id = Guid.Parse("11111111-1111-1111-1111-111111111119"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new WorkspaceAvailabilityOption { Id = Guid.Parse("11111111-1111-1111-1111-111111111120"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new WorkspaceAvailabilityOption { Id = Guid.Parse("11111111-1111-1111-1111-111111111121"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new WorkspaceAvailabilityOption { Id = Guid.Parse("11111111-1111-1111-1111-111111111122"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new WorkspaceAvailabilityOption { Id = Guid.Parse("11111111-1111-1111-1111-111111111123"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new WorkspaceAvailabilityOption { Id = Guid.Parse("11111111-1111-1111-1111-111111111124"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new WorkspaceAvailabilityOption { Id = Guid.Parse("11111111-1111-1111-1111-111111111125"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new WorkspaceAvailabilityOption { Id = Guid.Parse("11111111-1111-1111-1111-111111111126"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new WorkspaceAvailabilityOption { Id = Guid.Parse("11111111-1111-1111-1111-111111111127"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new WorkspaceAvailabilityOption { Id = Guid.Parse("11111111-1111-1111-1111-111111111128"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new WorkspaceAvailabilityOption { Id = Guid.Parse("11111111-1111-1111-1111-111111111129"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new WorkspaceAvailabilityOption { Id = Guid.Parse("11111111-1111-1111-1111-111111111130"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new WorkspaceAvailabilityOption { Id = Guid.Parse("11111111-1111-1111-1111-111111111131"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new WorkspaceAvailabilityOption { Id = Guid.Parse("11111111-1111-1111-1111-111111111132"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new WorkspaceAvailabilityOption { Id = Guid.Parse("11111111-1111-1111-1111-111111111133"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new WorkspaceAvailabilityOption { Id = Guid.Parse("11111111-1111-1111-1111-111111111134"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" }
            );

            // Seed WorkspaceAvailabilityOptions for Private Room (4 capacity options)
            modelBuilder.Entity<WorkspaceAvailabilityOption>().HasData(
                new WorkspaceAvailabilityOption { Id = Guid.Parse("22222222-2222-2222-2222-222222222201"), WorkspaceId = Guid.Parse("b2844205-b49b-4cf1-8389-b7cdb0a54372"), Capacity = 1, UnitType = "room" },
                new WorkspaceAvailabilityOption { Id = Guid.Parse("22222222-2222-2222-2222-222222222202"), WorkspaceId = Guid.Parse("b2844205-b49b-4cf1-8389-b7cdb0a54372"), Capacity = 2, UnitType = "room" },
                new WorkspaceAvailabilityOption { Id = Guid.Parse("22222222-2222-2222-2222-222222222205"), WorkspaceId = Guid.Parse("b2844205-b49b-4cf1-8389-b7cdb0a54372"), Capacity = 5, UnitType = "room" },
                new WorkspaceAvailabilityOption { Id = Guid.Parse("22222222-2222-2222-2222-222222222210"), WorkspaceId = Guid.Parse("b2844205-b49b-4cf1-8389-b7cdb0a54372"), Capacity = 10, UnitType = "room" }
            );

            // Seed WorkspaceAvailabilityOptions for Meeting Room (2 capacity options)
            modelBuilder.Entity<WorkspaceAvailabilityOption>().HasData(
                new WorkspaceAvailabilityOption { Id = Guid.Parse("33333333-3333-3333-3333-333333333310"), WorkspaceId = Guid.Parse("51de8af8-a981-49d9-a276-8144384555bb"), Capacity = 10, UnitType = "room" },
                new WorkspaceAvailabilityOption { Id = Guid.Parse("33333333-3333-3333-3333-333333333320"), WorkspaceId = Guid.Parse("51de8af8-a981-49d9-a276-8144384555bb"), Capacity = 20, UnitType = "room" }
            );
        }
    }
}
