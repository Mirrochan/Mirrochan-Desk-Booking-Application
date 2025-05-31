using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DeskBookingAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Workspaces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Capacity = table.Column<int[]>(type: "integer[]", nullable: false),
                    Amenities = table.Column<string[]>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workspaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkspaceAvailabilityOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkspaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    UnitType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkspaceAvailabilityOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkspaceAvailabilityOptions_Workspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalTable: "Workspaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    UserEmail = table.Column<string>(type: "text", nullable: false),
                    WorkspaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_WorkspaceAvailabilityOptions_RoomId",
                        column: x => x.RoomId,
                        principalTable: "WorkspaceAvailabilityOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Workspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalTable: "Workspaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Workspaces",
                columns: new[] { "Id", "Amenities", "Capacity", "Name", "Type" },
                values: new object[,]
                {
                    { new Guid("51de8af8-a981-49d9-a276-8144384555bb"), new[] { "Wi-Fi", "Conditioning", "Music", "Micro" }, new[] { 10, 20 }, "Meeting Room", 2 },
                    { new Guid("b2844205-b49b-4cf1-8389-b7cdb0a54372"), new[] { "Wi-Fi", "Conditioning", "Music" }, new[] { 1, 2, 5, 10 }, "Private Room", 1 },
                    { new Guid("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd"), new[] { "Conditioning", "Play", "Wi-Fi", "Coffee" }, new int[0], "Open Space", 0 }
                });

            migrationBuilder.InsertData(
                table: "WorkspaceAvailabilityOptions",
                columns: new[] { "Id", "Capacity", "UnitType", "WorkspaceId" },
                values: new object[,]
                {
                    { new Guid("0267e785-ccac-4512-9d93-5eb8d90e6f40"), 10, "room", new Guid("51de8af8-a981-49d9-a276-8144384555bb") },
                    { new Guid("03d742f8-2494-407b-97ec-ebf3b7c14720"), 0, "desk", new Guid("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd") },
                    { new Guid("04d993b9-3619-4a99-9fd7-4793855483d8"), 0, "desk", new Guid("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd") },
                    { new Guid("05192287-1759-4fe6-9762-e241967386b9"), 0, "desk", new Guid("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd") },
                    { new Guid("059ffa7c-0826-4f9e-accb-42907db4af39"), 0, "desk", new Guid("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd") },
                    { new Guid("05a2f6d4-1df8-4ba7-b6e0-b96382eccc48"), 0, "desk", new Guid("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd") },
                    { new Guid("0722764f-6f83-4b1a-9d2c-97ea351bb77e"), 0, "desk", new Guid("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd") },
                    { new Guid("0a6e9e4e-e369-4450-aae8-2b168e62679b"), 0, "desk", new Guid("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd") },
                    { new Guid("0fe5759f-c080-451c-9db0-7fdcc4a20888"), 0, "desk", new Guid("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd") },
                    { new Guid("11a29aa8-2e0d-4c6c-8452-c73ab7ed72bb"), 0, "desk", new Guid("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd") },
                    { new Guid("17b73954-d941-4b04-8e66-181c5a38dc1f"), 0, "desk", new Guid("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd") },
                    { new Guid("185cedd3-7101-4373-88f1-1975ae0a4e53"), 10, "room", new Guid("51de8af8-a981-49d9-a276-8144384555bb") },
                    { new Guid("2a11af56-40b4-46f5-bcd9-237cf8b80b65"), 0, "desk", new Guid("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd") },
                    { new Guid("2d5808e7-5a50-46ce-81d7-6f00fdf7a36e"), 10, "room", new Guid("51de8af8-a981-49d9-a276-8144384555bb") },
                    { new Guid("37d8909d-0132-4646-9da7-699a1399114e"), 10, "room", new Guid("51de8af8-a981-49d9-a276-8144384555bb") },
                    { new Guid("3a0bc226-1e63-444a-acd8-2f8a7a516ba4"), 0, "desk", new Guid("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd") },
                    { new Guid("3c024e8c-9472-492a-9075-6184c3b190e6"), 5, "room", new Guid("b2844205-b49b-4cf1-8389-b7cdb0a54372") },
                    { new Guid("411b0df1-d4b9-4f98-94a2-91d0e20452fc"), 5, "room", new Guid("b2844205-b49b-4cf1-8389-b7cdb0a54372") },
                    { new Guid("490385ef-b660-451f-ad9b-3cb9bb95b6f5"), 1, "room", new Guid("b2844205-b49b-4cf1-8389-b7cdb0a54372") },
                    { new Guid("52f983d7-2abe-40f1-990a-39082d48b4a5"), 0, "desk", new Guid("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd") },
                    { new Guid("57d352b0-06a9-4bc8-930a-6a013c47f3fa"), 0, "desk", new Guid("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd") },
                    { new Guid("5fc3d91f-6adf-471a-b5d7-ec2e38919a67"), 0, "desk", new Guid("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd") },
                    { new Guid("63cee1d2-5475-42d3-97df-75945111160f"), 0, "desk", new Guid("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd") },
                    { new Guid("694c8584-bbb0-4c9e-a097-52ea60c1f0e8"), 1, "room", new Guid("b2844205-b49b-4cf1-8389-b7cdb0a54372") },
                    { new Guid("6ef6be20-20c4-47bb-8486-18be2393f02d"), 2, "room", new Guid("b2844205-b49b-4cf1-8389-b7cdb0a54372") },
                    { new Guid("793d278d-4fca-4afb-9f49-640129b30804"), 1, "room", new Guid("b2844205-b49b-4cf1-8389-b7cdb0a54372") },
                    { new Guid("7d116299-bec6-468a-94c9-dbcb49fddbd3"), 2, "room", new Guid("b2844205-b49b-4cf1-8389-b7cdb0a54372") },
                    { new Guid("818b7488-9f73-4159-88db-11504e21caef"), 10, "room", new Guid("b2844205-b49b-4cf1-8389-b7cdb0a54372") },
                    { new Guid("84adf783-4b7c-4c44-b7e3-982e5e659105"), 2, "room", new Guid("b2844205-b49b-4cf1-8389-b7cdb0a54372") },
                    { new Guid("91bbfdb5-5cbb-48ee-9f07-cf33ada7bbc4"), 1, "room", new Guid("b2844205-b49b-4cf1-8389-b7cdb0a54372") },
                    { new Guid("930b50a5-4ff3-4b57-b485-4779ffc7a8f9"), 0, "desk", new Guid("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd") },
                    { new Guid("95d4b90e-4986-493c-9398-c17961b537e0"), 1, "room", new Guid("b2844205-b49b-4cf1-8389-b7cdb0a54372") },
                    { new Guid("9717cd48-954e-4a32-99ad-02e50a2da90e"), 1, "room", new Guid("b2844205-b49b-4cf1-8389-b7cdb0a54372") },
                    { new Guid("97d00c25-1678-4ed8-a2f2-74c553df2e4d"), 0, "desk", new Guid("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd") },
                    { new Guid("97d33b28-05e2-469a-8d58-584281de25c2"), 0, "desk", new Guid("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd") },
                    { new Guid("9f5163fb-1e9a-4ea3-b5cf-f59d351c4f33"), 1, "room", new Guid("b2844205-b49b-4cf1-8389-b7cdb0a54372") },
                    { new Guid("a50182d1-83d9-43fd-9640-89ffb94a2975"), 2, "room", new Guid("b2844205-b49b-4cf1-8389-b7cdb0a54372") },
                    { new Guid("bf2309fd-28c8-49ea-8fc1-eb050a33f101"), 0, "desk", new Guid("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd") },
                    { new Guid("cdfcfab1-11fa-496a-82c3-810a95a35918"), 20, "room", new Guid("51de8af8-a981-49d9-a276-8144384555bb") },
                    { new Guid("d2a06741-4b55-4f2f-9469-73e64edcf1bf"), 0, "desk", new Guid("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd") },
                    { new Guid("d2c32153-10b1-42ad-8762-3c731517b00e"), 0, "desk", new Guid("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd") },
                    { new Guid("de873ec6-5095-4096-b4d7-5ffd8837ccea"), 0, "desk", new Guid("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd") },
                    { new Guid("e4b0439c-c603-438d-9c48-3e89944b7221"), 0, "desk", new Guid("f1d76f62-e4ff-4b68-82d7-9733fb57f3bd") },
                    { new Guid("e9a01b0c-4caf-4d98-bbe8-5f3171a974df"), 5, "room", new Guid("b2844205-b49b-4cf1-8389-b7cdb0a54372") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RoomId",
                table: "Bookings",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_WorkspaceId",
                table: "Bookings",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkspaceAvailabilityOptions_WorkspaceId",
                table: "WorkspaceAvailabilityOptions",
                column: "WorkspaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "WorkspaceAvailabilityOptions");

            migrationBuilder.DropTable(
                name: "Workspaces");
        }
    }
}
