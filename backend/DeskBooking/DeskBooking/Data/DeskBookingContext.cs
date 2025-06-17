using Microsoft.EntityFrameworkCore;
using DeskBookingAPI.Models;
using System;
using System.Collections.Generic;

namespace DeskBookingAPI.Data
{
    public class DeskBookingContext : DbContext
    {
        public DeskBookingContext(DbContextOptions<DeskBookingContext> options) : base(options) { }
        public DbSet<CoworkingModel> Coworkings { get; set; }
        public DbSet<BookingModel> Bookings { get; set; }
        public DbSet<Workspace> Workspaces { get; set; }
        public DbSet<RoomModel> WorkspaceAvailabilityOptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<BookingModel>()
                .HasOne(b => b.Workspace)
                .WithMany(w => w.Bookings)
                .HasForeignKey(b => b.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<BookingModel>()
                .HasOne(b => b.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RoomModel>()
                .HasOne(o => o.Workspace)
                .WithMany(w => w.AvailabilityOptions)
                .HasForeignKey(o => o.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CoworkingModel>()
    .HasMany(c => c.Workspaces)
    .WithOne(w => w.Coworking)
    .HasForeignKey(w => w.CoworkingId);

modelBuilder.Entity<CoworkingModel>().HasData(
    new CoworkingModel
    {
        Id = Guid.Parse("c1aaf8dc-34a1-4c3d-a8a0-f9e9e53e1f01"),
        Name = "WorkClub Pechersk",
        Address = "123 Yaroslaviv Val St, Kyiv",
        Description = "Modern coworking in the heart of Pechersk with quiet rooms and coffee on tap."
        
    },
    new CoworkingModel
    {
        Id = Guid.Parse("d9b6a892-8f4e-45b1-9a8e-5483edb4e771"),
        Name = "UrbanSpace Podil",
        Address = "78 Naberezhno-Khreshchatytska St, Kyiv",
        Description = "A creative riverside hub ideal for freelancers and small startups."
    
    },
    new CoworkingModel
    {
        Id = Guid.Parse("e2fa83d3-78c2-41cd-bb44-d1c601dbb55e"),
        Name = "Creative Hub Lvivska",
        Address = "12 Lvivska Square, Kyiv",
        Description = "A compact, design-focused space with open desks and strong community vibes."
    },
     new CoworkingModel
     {
         Id = Guid.Parse("d9b9a892-8f48-45b1-9a8e-5483edb4e771"),
         Name = "TechNest Olimpiiska",
         Address = "45 Velyka Vasylkivska St, Kyiv",
         Description = "A high-tech space near Olimpiiska metro, perfect for team sprints and solo focus."

     },
      new CoworkingModel
    {
        Id = Guid.Parse("d9b6a892-8f4e-45b1-9a8e-5483edb4e071"),
        Name = "Hive Station Troieshchyna",
        Address = "102 Zakrevskogo St, Kyiv",
        Description = "A quiet, affordable option in the city's northeast—great for remote workers."
    
    }
);
            modelBuilder.Entity<Workspace>().HasData(
                new Workspace
                {
                    Id = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"),
                    Name = "Open Space",
                    Description = "A vibrant shared area perfect for freelancers or small teams who enjoy a collaborative atmosphere. Choose any available desk and get to work with flexibility and ease.",
                    Type = WorkspaceType.OpenSpace,
                    Capacity = new int[] { },
                    Amenities = new string[] { "Conditioning", "Play", "Wi-Fi", "Coffee" },
                    CoworkingId = Guid.Parse("c1aaf8dc-34a1-4c3d-a8a0-f9e9e53e1f01")

                },
                 new Workspace
                 {
                     Id = Guid.Parse("f1d76762-e0ff-4b68-82d7-9733fb47f3bd"),
                     Name = "Open Space №2",
                     Description = "A vibrant shared area perfect for freelancers or small teams who enjoy a collaborative atmosphere. Choose any available desk and get to work with flexibility and ease.",
                     Type = WorkspaceType.OpenSpace,
                     Capacity = new int[] { },
                     Amenities = new string[] { "Conditioning"},
                     CoworkingId = Guid.Parse("c1aaf8dc-34a1-4c3d-a8a0-f9e9e53e1f01")

                 },
                new Workspace
                {
                    Id = Guid.Parse("b2844205-b49b-4cf1-8789-b7cdb0a54372"),
                    Name = "Private Room",
                    Description = "Ideal for focused work, video calls, or small team huddles. These fully enclosed rooms offer privacy and come in a variety of sizes to fit your needs.",
                    Type = WorkspaceType.PrivateRoom,
                    Capacity = new int[] { 1, 2, 5, 10 },
                    Amenities = new string[] { "Wi-Fi", "Conditioning", "Music" },
                    CoworkingId = Guid.Parse("c1aaf8dc-34a1-4c3d-a8a0-f9e9e53e1f01")
                },
                new Workspace
                {
                    Id = Guid.Parse("51de8af8-a981-49d9-a276-8144384595bb"),
                    Name = "Meeting Room",
                    Description = "Designed for productive meetings, workshops, or client presentations. Equipped with screens, whiteboards, and comfortable seating to keep your sessions running smoothly.",
                    Type = WorkspaceType.MeetingRoom,
                    Capacity = new int[] { 10, 20 },
                    Amenities = new string[] { "Wi-Fi", "Conditioning", "Music", "Micro" },
                    CoworkingId = Guid.Parse("c1aaf8dc-34a1-4c3d-a8a0-f9e9e53e1f01")
                }
            );

            modelBuilder.Entity<RoomModel>().HasData(
                new RoomModel { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new RoomModel { Id = Guid.Parse("11111111-1111-1111-1111-111111111112"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new RoomModel { Id = Guid.Parse("11111111-1111-1111-1111-111111111113"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new RoomModel { Id = Guid.Parse("11111111-1111-1111-1111-111111111114"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new RoomModel { Id = Guid.Parse("11111111-1111-1111-1111-111111111115"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new RoomModel { Id = Guid.Parse("11111111-1111-1111-1111-111111111116"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new RoomModel { Id = Guid.Parse("11111111-1111-1111-1111-111111111117"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new RoomModel { Id = Guid.Parse("11111111-1111-1111-1111-111111111118"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new RoomModel { Id = Guid.Parse("11111111-1111-1111-1111-111111111119"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new RoomModel { Id = Guid.Parse("11111111-1111-1111-1111-111111111120"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new RoomModel { Id = Guid.Parse("11111111-1111-1111-1111-111111111121"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new RoomModel { Id = Guid.Parse("11111111-1111-1111-1111-111111111122"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new RoomModel { Id = Guid.Parse("11111111-1111-1111-1111-111111111123"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new RoomModel { Id = Guid.Parse("11111111-1111-1111-1111-111111111124"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new RoomModel { Id = Guid.Parse("11111111-1111-1111-1111-111111111125"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new RoomModel { Id = Guid.Parse("11111111-1111-1111-1111-111111111126"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new RoomModel { Id = Guid.Parse("11111111-1111-1111-1111-111111111127"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new RoomModel { Id = Guid.Parse("11111111-1111-1111-1111-111111111128"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new RoomModel { Id = Guid.Parse("11111111-1111-1111-1111-111111111129"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new RoomModel { Id = Guid.Parse("11111111-1111-1111-1111-111111111130"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new RoomModel { Id = Guid.Parse("11111111-1111-1111-1111-111111111131"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new RoomModel { Id = Guid.Parse("11111111-1111-1111-1111-111111111132"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new RoomModel { Id = Guid.Parse("11111111-1111-1111-1111-111111111133"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" },
                new RoomModel { Id = Guid.Parse("11111111-1111-1111-1111-111111111134"), WorkspaceId = Guid.Parse("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), Capacity = 0, UnitType = "desk" }
            );


            modelBuilder.Entity<RoomModel>().HasData(
                new RoomModel { Id = Guid.Parse("22222222-2222-2222-2222-222222222201"), WorkspaceId = Guid.Parse("b2844205-b49b-4cf1-8789-b7cdb0a54372"), Capacity = 1, UnitType = "room" },
                new RoomModel { Id = Guid.Parse("22222222-2222-2222-2222-222222222202"), WorkspaceId = Guid.Parse("b2844205-b49b-4cf1-8789-b7cdb0a54372"), Capacity = 2, UnitType = "room" },
                new RoomModel { Id = Guid.Parse("22222222-2222-2222-2222-222222222205"), WorkspaceId = Guid.Parse("b2844205-b49b-4cf1-8789-b7cdb0a54372"), Capacity = 5, UnitType = "room" },
                new RoomModel { Id = Guid.Parse("22222222-2222-2222-2222-222222222210"), WorkspaceId = Guid.Parse("b2844205-b49b-4cf1-8789-b7cdb0a54372"), Capacity = 10, UnitType = "room" }
            );


            modelBuilder.Entity<RoomModel>().HasData(
                new RoomModel { Id = Guid.Parse("33333333-3333-3333-3333-333333333310"), WorkspaceId = Guid.Parse("51de8af8-a981-49d9-a276-8144384595bb"), Capacity = 10, UnitType = "room" },
                new RoomModel { Id = Guid.Parse("33333333-3333-3333-3333-333333333320"), WorkspaceId = Guid.Parse("51de8af8-a981-49d9-a276-8144384595bb"), Capacity = 20, UnitType = "room" }
            );
        }
    }
}
