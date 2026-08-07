// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aguacongas.FreeTheIdServer.MySql.Migrations.ConfigurationDb
{
    public partial class AddRelyingPartyDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RelyingParties",
                type: "longtext",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "RelyingParties");
        }
    }
}
