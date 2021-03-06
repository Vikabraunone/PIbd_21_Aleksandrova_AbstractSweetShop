﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AbstractSweetShopDatabaseImplement.Migrations
{
    public partial class AddMessageInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessageInfoes",
                columns: table => new
                {
                    MessageId = table.Column<string>(nullable: false),
                    ClientId = table.Column<int>(nullable: true),
                    SenderName = table.Column<string>(nullable: true),
                    DateDelivery = table.Column<DateTime>(nullable: false),
                    Subject = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageInfoes", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_MessageInfoes_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageInfoes_ClientId",
                table: "MessageInfoes",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageInfoes");
        }
    }
}
