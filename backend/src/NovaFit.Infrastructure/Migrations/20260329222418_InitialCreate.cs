using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovaFit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "casilleros",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Numero = table.Column<int>(type: "integer", nullable: false),
                    Disponible = table.Column<bool>(type: "boolean", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreadoEn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_casilleros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ci = table.Column<int>(type: "integer", nullable: false),
                    Nombres = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TipoCliente = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "nuevo"),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "promociones_festivas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    PorcentajeDescuento = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Activa = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreadoEn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promociones_festivas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "membresias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uuid", nullable: false),
                    TipoPlan = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Costo = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "activa"),
                    Observacion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreadoEn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ClienteId1 = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_membresias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_membresias_clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_membresias_clientes_ClienteId1",
                        column: x => x.ClienteId1,
                        principalTable: "clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "prestamos_casilleros",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CasilleroId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentoRetenido = table.Column<string>(type: "text", nullable: false),
                    FechaHoraPrestamo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaHoraDevolucion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Observacion = table.Column<string>(type: "text", nullable: true),
                    CreadoEn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CasilleroId1 = table.Column<Guid>(type: "uuid", nullable: false),
                    ClienteId1 = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prestamos_casilleros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_prestamos_casilleros_casilleros_CasilleroId",
                        column: x => x.CasilleroId,
                        principalTable: "casilleros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_prestamos_casilleros_casilleros_CasilleroId1",
                        column: x => x.CasilleroId1,
                        principalTable: "casilleros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_prestamos_casilleros_clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_prestamos_casilleros_clientes_ClienteId1",
                        column: x => x.ClienteId1,
                        principalTable: "clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ingresos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uuid", nullable: false),
                    FechaHoraIngreso = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Permitido = table.Column<bool>(type: "boolean", nullable: false),
                    MotivoAlerta = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    MembresiaId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreadoEn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ClienteId1 = table.Column<Guid>(type: "uuid", nullable: false),
                    MembresiaId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingresos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ingresos_clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ingresos_clientes_ClienteId1",
                        column: x => x.ClienteId1,
                        principalTable: "clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ingresos_membresias_MembresiaId",
                        column: x => x.MembresiaId,
                        principalTable: "membresias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ingresos_membresias_MembresiaId1",
                        column: x => x.MembresiaId1,
                        principalTable: "membresias",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_casilleros_Numero",
                table: "casilleros",
                column: "Numero",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_clientes_Ci",
                table: "clientes",
                column: "Ci",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ingresos_ClienteId_FechaHoraIngreso",
                table: "ingresos",
                columns: new[] { "ClienteId", "FechaHoraIngreso" });

            migrationBuilder.CreateIndex(
                name: "IX_ingresos_ClienteId1",
                table: "ingresos",
                column: "ClienteId1");

            migrationBuilder.CreateIndex(
                name: "IX_ingresos_MembresiaId",
                table: "ingresos",
                column: "MembresiaId");

            migrationBuilder.CreateIndex(
                name: "IX_ingresos_MembresiaId1",
                table: "ingresos",
                column: "MembresiaId1");

            migrationBuilder.CreateIndex(
                name: "IX_membresias_ClienteId",
                table: "membresias",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_membresias_ClienteId1",
                table: "membresias",
                column: "ClienteId1");

            migrationBuilder.CreateIndex(
                name: "IX_prestamos_casilleros_CasilleroId_FechaHoraPrestamo",
                table: "prestamos_casilleros",
                columns: new[] { "CasilleroId", "FechaHoraPrestamo" });

            migrationBuilder.CreateIndex(
                name: "IX_prestamos_casilleros_CasilleroId1",
                table: "prestamos_casilleros",
                column: "CasilleroId1");

            migrationBuilder.CreateIndex(
                name: "IX_prestamos_casilleros_ClienteId",
                table: "prestamos_casilleros",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_prestamos_casilleros_ClienteId1",
                table: "prestamos_casilleros",
                column: "ClienteId1");

            migrationBuilder.CreateIndex(
                name: "IX_promociones_festivas_FechaInicio_FechaFin",
                table: "promociones_festivas",
                columns: new[] { "FechaInicio", "FechaFin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ingresos");

            migrationBuilder.DropTable(
                name: "prestamos_casilleros");

            migrationBuilder.DropTable(
                name: "promociones_festivas");

            migrationBuilder.DropTable(
                name: "membresias");

            migrationBuilder.DropTable(
                name: "casilleros");

            migrationBuilder.DropTable(
                name: "clientes");
        }
    }
}
