using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace administracionScoutsCR.Migrations
{
    /// <inheritdoc />
    public partial class CorregirNombreRutaImagen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactoEmergencia",
                columns: table => new
                {
                    IdContactoEmergencia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Parentesco = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    NumeroContacto = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Contacto__3A12A7662C4BB57D", x => x.IdContactoEmergencia);
                });

            migrationBuilder.CreateTable(
                name: "Etapa",
                columns: table => new
                {
                    IdEtapa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    Seccion = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    Estado = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Etapa__E4B65D8E02E46CDA", x => x.IdEtapa);
                });

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    IdEvento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    Lugar = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Encargado = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ContactoEncargado = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    RutaImagen = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Eventos__034EFC043758CED9", x => x.IdEvento);
                });

            migrationBuilder.CreateTable(
                name: "Insignias",
                columns: table => new
                {
                    IdInsignia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    Seccion = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    Tipo = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    Estado = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    ImagenRuta = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Insignia__90F5F482014A20D0", x => x.IdInsignia);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seccion",
                columns: table => new
                {
                    IdSeccion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    EdadMinima = table.Column<int>(type: "int", nullable: false),
                    EdadMaxima = table.Column<int>(type: "int", nullable: false),
                    JefeSeccion = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Seccion__CD2B049F800113F0", x => x.IdSeccion);
                });

            migrationBuilder.CreateTable(
                name: "ReqInsignia",
                columns: table => new
                {
                    IdReqInsignia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    IdInsignias = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ReqInsig__63856F5906174F79", x => x.IdReqInsignia);
                    table.ForeignKey(
                        name: "FK_ReqInsignia_Insignias",
                        column: x => x.IdInsignias,
                        principalTable: "Insignias",
                        principalColumn: "IdInsignia",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Apellido1 = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Apellido2 = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    FechaNacimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    TipoUsuario = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IdSeccion = table.Column<int>(type: "int", nullable: false),
                    Direccion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Correo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    NumeroTelefono = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Contrasena = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    IdRole = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuario__5B65BF979FB41479", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuario_Roles",
                        column: x => x.IdRole,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuario_Seccion",
                        column: x => x.IdSeccion,
                        principalTable: "Seccion",
                        principalColumn: "IdSeccion");
                });

            migrationBuilder.CreateTable(
                name: "ConfirmacionEvento",
                columns: table => new
                {
                    IdConfirmacionEvento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Asistencia = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    IdEvento = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    ImagenRuta = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Confirma__F2F2F9E04E542794", x => x.IdConfirmacionEvento);
                    table.ForeignKey(
                        name: "FK_ConfirmacionEvento_Eventos",
                        column: x => x.IdEvento,
                        principalTable: "Eventos",
                        principalColumn: "IdEvento");
                    table.ForeignKey(
                        name: "FK_ConfirmacionEvento_Usuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "UsuarioxContactoEmergencia",
                columns: table => new
                {
                    IdUsuarioxContactoEmergencia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: true),
                    IdContactoEmergencia = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuariox__EBAD584FA589C63B", x => x.IdUsuarioxContactoEmergencia);
                    table.ForeignKey(
                        name: "FK_UsuarioxContactoEmergencia_ContactoEmergencia",
                        column: x => x.IdContactoEmergencia,
                        principalTable: "ContactoEmergencia",
                        principalColumn: "IdContactoEmergencia");
                    table.ForeignKey(
                        name: "FK_UsuarioxContactoEmergencia_Usuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "UsuarioxEtapa",
                columns: table => new
                {
                    IdUsuarioEtapa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdEtapa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuariox__865ED3013973B362", x => x.IdUsuarioEtapa);
                    table.ForeignKey(
                        name: "FK_UsuarioxEtapa_Etapa",
                        column: x => x.IdEtapa,
                        principalTable: "Etapa",
                        principalColumn: "IdEtapa");
                    table.ForeignKey(
                        name: "FK_UsuarioxEtapa_Usuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "UsuarioxInsignia",
                columns: table => new
                {
                    IdUsuarioxInsignias = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdInsignia = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ImagenRuta = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuariox__C499478003211EB8", x => x.IdUsuarioxInsignias);
                    table.ForeignKey(
                        name: "FK_UsuarioxInsignia_Insignias",
                        column: x => x.IdInsignia,
                        principalTable: "Insignias",
                        principalColumn: "IdInsignia");
                    table.ForeignKey(
                        name: "FK_UsuarioxInsignia_Usuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmacionEvento_IdEvento",
                table: "ConfirmacionEvento",
                column: "IdEvento");

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmacionEvento_IdUsuario",
                table: "ConfirmacionEvento",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_ReqInsignia_IdInsignias",
                table: "ReqInsignia",
                column: "IdInsignias");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdRole",
                table: "Usuario",
                column: "IdRole");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdSeccion",
                table: "Usuario",
                column: "IdSeccion");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioxContactoEmergencia_IdContactoEmergencia",
                table: "UsuarioxContactoEmergencia",
                column: "IdContactoEmergencia");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioxContactoEmergencia_IdUsuario",
                table: "UsuarioxContactoEmergencia",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioxEtapa_IdEtapa",
                table: "UsuarioxEtapa",
                column: "IdEtapa");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioxEtapa_IdUsuario",
                table: "UsuarioxEtapa",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioxInsignia_IdInsignia",
                table: "UsuarioxInsignia",
                column: "IdInsignia");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioxInsignia_IdUsuario",
                table: "UsuarioxInsignia",
                column: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfirmacionEvento");

            migrationBuilder.DropTable(
                name: "ReqInsignia");

            migrationBuilder.DropTable(
                name: "UsuarioxContactoEmergencia");

            migrationBuilder.DropTable(
                name: "UsuarioxEtapa");

            migrationBuilder.DropTable(
                name: "UsuarioxInsignia");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "ContactoEmergencia");

            migrationBuilder.DropTable(
                name: "Etapa");

            migrationBuilder.DropTable(
                name: "Insignias");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Seccion");
        }
    }
}
