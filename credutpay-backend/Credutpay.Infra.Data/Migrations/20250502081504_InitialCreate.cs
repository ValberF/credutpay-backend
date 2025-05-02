using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Credutpay.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(36)", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    WalletId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wallet",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(36)", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    UserId = table.Column<string>(type: "character varying(36)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallet_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WalletTransaction",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(36)", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    WalletReceiverId = table.Column<string>(type: "character varying(36)", nullable: false),
                    WalletSenderId = table.Column<string>(type: "character varying(36)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WalletTransaction_Wallet_WalletReceiverId",
                        column: x => x.WalletReceiverId,
                        principalTable: "Wallet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WalletTransaction_Wallet_WalletSenderId",
                        column: x => x.WalletSenderId,
                        principalTable: "Wallet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedAt", "Email", "Name", "Password", "Type", "UpdatedAt", "WalletId" },
                values: new object[,]
                {
                    { "00000000-0000-0000-0000-000000000001", new DateTime(2025, 4, 30, 21, 0, 0, 0, DateTimeKind.Local), "admin@credutpay.com", "Administrador", "$2a$11$jOsKulVMar93/ay9GWM6COq.nC3DaEWHQ775T6SOfXj/C70AToWgK", "User", null, "00000000-0000-0000-0000-000000000101" },
                    { "00000000-0000-0000-0000-000000000002", new DateTime(2025, 4, 30, 21, 0, 0, 0, DateTimeKind.Local), "lojista@credutpay.com", "Lojista Silva", "$2a$11$jOsKulVMar93/ay9GWM6COq.nC3DaEWHQ775T6SOfXj/C70AToWgK", "User", null, "00000000-0000-0000-0000-000000000102" },
                    { "00000000-0000-0000-0000-000000000003", new DateTime(2025, 4, 30, 21, 0, 0, 0, DateTimeKind.Local), "joao@credutpay.com", "João Usuário", "$2a$11$jOsKulVMar93/ay9GWM6COq.nC3DaEWHQ775T6SOfXj/C70AToWgK", "User", null, "00000000-0000-0000-0000-000000000103" },
                    { "00000000-0000-0000-0000-000000000004", new DateTime(2025, 4, 30, 21, 0, 0, 0, DateTimeKind.Local), "maria@credutpay.com", "Maria Usuária", "$2a$11$jOsKulVMar93/ay9GWM6COq.nC3DaEWHQ775T6SOfXj/C70AToWgK", "User", null, "00000000-0000-0000-0000-000000000104" }
                });

            migrationBuilder.InsertData(
                table: "Wallet",
                columns: new[] { "Id", "Amount", "CreatedAt", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { "00000000-0000-0000-0000-000000000101", 5000.00m, new DateTime(2025, 4, 30, 21, 0, 0, 0, DateTimeKind.Local), null, "00000000-0000-0000-0000-000000000001" },
                    { "00000000-0000-0000-0000-000000000102", 10000.00m, new DateTime(2025, 4, 30, 21, 0, 0, 0, DateTimeKind.Local), null, "00000000-0000-0000-0000-000000000002" },
                    { "00000000-0000-0000-0000-000000000103", 1200.50m, new DateTime(2025, 4, 30, 21, 0, 0, 0, DateTimeKind.Local), null, "00000000-0000-0000-0000-000000000003" },
                    { "00000000-0000-0000-0000-000000000104", 850.75m, new DateTime(2025, 4, 30, 21, 0, 0, 0, DateTimeKind.Local), null, "00000000-0000-0000-0000-000000000004" }
                });

            migrationBuilder.InsertData(
                table: "WalletTransaction",
                columns: new[] { "Id", "Amount", "CreatedAt", "UpdatedAt", "WalletReceiverId", "WalletSenderId" },
                values: new object[,]
                {
                    { "00000000-0000-0000-0000-000000000201", 150.50m, new DateTime(2025, 4, 26, 21, 0, 0, 0, DateTimeKind.Local), null, "00000000-0000-0000-0000-000000000102", "00000000-0000-0000-0000-000000000103" },
                    { "00000000-0000-0000-0000-000000000202", 75.25m, new DateTime(2025, 4, 28, 21, 0, 0, 0, DateTimeKind.Local), null, "00000000-0000-0000-0000-000000000102", "00000000-0000-0000-0000-000000000104" },
                    { "00000000-0000-0000-0000-000000000203", 50.00m, new DateTime(2025, 4, 30, 21, 0, 0, 0, DateTimeKind.Local), null, "00000000-0000-0000-0000-000000000104", "00000000-0000-0000-0000-000000000103" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_UserId",
                table: "Wallet",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransaction_WalletReceiverId",
                table: "WalletTransaction",
                column: "WalletReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransaction_WalletSenderId",
                table: "WalletTransaction",
                column: "WalletSenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WalletTransaction");

            migrationBuilder.DropTable(
                name: "Wallet");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
