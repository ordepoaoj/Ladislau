using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web20.Migrations.AppDb
{
    public partial class _17022021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "__MigrationHistory",
                columns: table => new
                {
                    MigrationId = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ContextKey = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Model = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ProductVersion = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.__MigrationHistory", x => new { x.MigrationId, x.ContextKey });
                });

            migrationBuilder.CreateTable(
                name: "Aquisicao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo_Aquisicao = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aquisicao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Continente_Editor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome_Continente = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Continente_Editor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Periodicidade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Tipo_Periodicidade = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periodicidade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Revista_Museu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Aleph = table.Column<int>(type: "int", nullable: true),
                    IBICT = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ISSN = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Titulo = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    cd_Periodicidade = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Revista_Museu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pais_Editor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cod_Continente = table.Column<int>(type: "int", nullable: true),
                    Nome_Pais = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais_Editor", x => x.Id);
                    table.ForeignKey(
                        name: "fk_cpm_Editor",
                        column: x => x.Cod_Continente,
                        principalTable: "Continente_Editor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Editor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome_Editor = table.Column<string>(type: "varchar(700)", unicode: false, maxLength: 700, nullable: false),
                    Endereco = table.Column<string>(type: "varchar(750)", unicode: false, maxLength: 750, nullable: false),
                    Cod_Pais = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Telefone = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editor", x => x.Id);
                    table.ForeignKey(
                        name: "fk_pais_Editor",
                        column: x => x.Cod_Pais,
                        principalTable: "Pais_Editor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Museu_Editor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Id_Editor = table.Column<int>(type: "int", nullable: false),
                    Id_Museu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Museu_Editor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Editor_Editor_Museu ",
                        column: x => x.Id_Museu,
                        principalTable: "Revista_Museu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Editor_Museu_Editor ",
                        column: x => x.Id_Editor,
                        principalTable: "Editor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Preferencia_Editor",
                columns: table => new
                {
                    Cod_publicacao = table.Column<int>(type: "int", nullable: false),
                    Cod_Editor = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Preferen__7E9492BB1D286EF4", x => new { x.Cod_publicacao, x.Cod_Editor });
                    table.ForeignKey(
                        name: "fk_cod_Editor",
                        column: x => x.Cod_Editor,
                        principalTable: "Editor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Revista",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Aleph = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Titulo = table.Column<string>(type: "varchar(750)", unicode: false, maxLength: 750, nullable: true),
                    IBICT = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ISSN = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Chegada = table.Column<DateTime>(type: "date", nullable: true),
                    cd_Aquisicao = table.Column<int>(type: "int", nullable: true),
                    cd_Editor = table.Column<int>(type: "int", nullable: true),
                    cd_Periodicidade = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Revista", x => x.Id);
                    table.ForeignKey(
                        name: "fk_cod_Aquisicao_Revista",
                        column: x => x.cd_Aquisicao,
                        principalTable: "Aquisicao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_cod_Editor_Revista",
                        column: x => x.cd_Editor,
                        principalTable: "Editor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_cod_Periodicidade_Revista",
                        column: x => x.cd_Periodicidade,
                        principalTable: "Periodicidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "([NormalizedName] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "([NormalizedUserName] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "AK_Editor",
                table: "Editor",
                column: "Nome_Editor",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Editor_Cod_Pais",
                table: "Editor",
                column: "Cod_Pais");

            migrationBuilder.CreateIndex(
                name: "IX_Museu_Editor_Id_Editor",
                table: "Museu_Editor",
                column: "Id_Editor");

            migrationBuilder.CreateIndex(
                name: "IX_Museu_Editor_Id_Museu",
                table: "Museu_Editor",
                column: "Id_Museu");

            migrationBuilder.CreateIndex(
                name: "IX_Pais_Editor_Cod_Continente",
                table: "Pais_Editor",
                column: "Cod_Continente");

            migrationBuilder.CreateIndex(
                name: "IX_Preferencia_Editor_Cod_Editor",
                table: "Preferencia_Editor",
                column: "Cod_Editor");

            migrationBuilder.CreateIndex(
                name: "IX_Revista_cd_Aquisicao",
                table: "Revista",
                column: "cd_Aquisicao");

            migrationBuilder.CreateIndex(
                name: "IX_Revista_cd_Editor",
                table: "Revista",
                column: "cd_Editor");

            migrationBuilder.CreateIndex(
                name: "IX_Revista_cd_Periodicidade",
                table: "Revista",
                column: "cd_Periodicidade");

            migrationBuilder.CreateIndex(
                name: "UQ__Revista__447D3E9665F5288E",
                table: "Revista",
                column: "ISSN",
                unique: true,
                filter: "[ISSN] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Revista__447D3E966BA65F1F",
                table: "Revista",
                column: "ISSN",
                unique: true,
                filter: "[ISSN] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Revista__77BBC3442CC80D46",
                table: "Revista",
                column: "Aleph",
                unique: true,
                filter: "[Aleph] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Revista__77BBC344F33880D5",
                table: "Revista",
                column: "Aleph",
                unique: true,
                filter: "[Aleph] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Revista__7B406B5625EF3194",
                table: "Revista",
                column: "Titulo",
                unique: true,
                filter: "[Titulo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Revista__8D98C9B9524AE8D9",
                table: "Revista",
                column: "IBICT",
                unique: true,
                filter: "[IBICT] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Revista__8D98C9B9BDB348D1",
                table: "Revista",
                column: "IBICT",
                unique: true,
                filter: "[IBICT] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "__MigrationHistory");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Museu_Editor");

            migrationBuilder.DropTable(
                name: "Preferencia_Editor");

            migrationBuilder.DropTable(
                name: "Revista");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Revista_Museu");

            migrationBuilder.DropTable(
                name: "Aquisicao");

            migrationBuilder.DropTable(
                name: "Editor");

            migrationBuilder.DropTable(
                name: "Periodicidade");

            migrationBuilder.DropTable(
                name: "Pais_Editor");

            migrationBuilder.DropTable(
                name: "Continente_Editor");
        }
    }
}
