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
                name: "clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ci = table.Column<int>(type: "integer", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Eliminado = table.Column<bool>(type: "boolean", nullable: false),
                    FechaEliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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
                    VecesAplicada = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Eliminado = table.Column<bool>(type: "boolean", nullable: false),
                    FechaEliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promociones_festivas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "casilleros",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Numero = table.Column<int>(type: "integer", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    Ubicacion = table.Column<string>(type: "text", nullable: true),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    AsignadoAClienteId = table.Column<Guid>(type: "uuid", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Eliminado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    FechaEliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_casilleros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_casilleros_clientes_AsignadoAClienteId",
                        column: x => x.AsignadoAClienteId,
                        principalTable: "clientes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Suscripcions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uuid", nullable: false),
                    Tipo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Precio = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "ACTIVA"),
                    CasilleroFijoId = table.Column<Guid>(type: "uuid", nullable: true),
                    PromocionId = table.Column<Guid>(type: "uuid", nullable: true),
                    DescuentoAplicado = table.Column<decimal>(type: "numeric(5,2)", nullable: false, defaultValue: 0.00m),
                    IngresosTotalesUsados = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Eliminado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    FechaEliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suscripcions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suscripcions_casilleros_CasilleroFijoId",
                        column: x => x.CasilleroFijoId,
                        principalTable: "casilleros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Suscripcions_clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Suscripcions_promociones_festivas_PromocionId",
                        column: x => x.PromocionId,
                        principalTable: "promociones_festivas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ingresos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uuid", nullable: false),
                    SuscripcionId = table.Column<Guid>(type: "uuid", nullable: true),
                    FechaIngreso = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HoraIngreso = table.Column<TimeSpan>(type: "interval", nullable: false),
                    HoraSalida = table.Column<TimeSpan>(type: "interval", nullable: true),
                    SalidaRegistrada = table.Column<bool>(type: "boolean", nullable: false),
                    DuracionMinutos = table.Column<int>(type: "integer", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Eliminado = table.Column<bool>(type: "boolean", nullable: false),
                    FechaEliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingresos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ingresos_Suscripcions_SuscripcionId",
                        column: x => x.SuscripcionId,
                        principalTable: "Suscripcions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ingresos_clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "prestamos_casilleros",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IngresoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CasilleroId = table.Column<Guid>(type: "uuid", nullable: false),
                    NumeroTicket = table.Column<string>(type: "text", nullable: true),
                    CiDepositado = table.Column<int>(type: "integer", nullable: true),
                    NumeroLlave = table.Column<string>(type: "text", nullable: true),
                    FechaPrestamo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HoraPrestamo = table.Column<TimeSpan>(type: "interval", nullable: false),
                    FechaDevolucion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    HoraDevolucion = table.Column<TimeSpan>(type: "interval", nullable: true),
                    Devuelto = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Eliminado = table.Column<bool>(type: "boolean", nullable: false),
                    FechaEliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ClienteId = table.Column<Guid>(type: "uuid", nullable: true)
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
                        name: "FK_prestamos_casilleros_clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "clientes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_prestamos_casilleros_ingresos_IngresoId",
                        column: x => x.IngresoId,
                        principalTable: "ingresos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_casilleros_AsignadoAClienteId",
                table: "casilleros",
                column: "AsignadoAClienteId");

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
                name: "IX_ingresos_ClienteId_FechaIngreso",
                table: "ingresos",
                columns: new[] { "ClienteId", "FechaIngreso" });

            migrationBuilder.CreateIndex(
                name: "IX_ingresos_SuscripcionId",
                table: "ingresos",
                column: "SuscripcionId");

            migrationBuilder.CreateIndex(
                name: "IX_prestamos_casilleros_CasilleroId_FechaPrestamo",
                table: "prestamos_casilleros",
                columns: new[] { "CasilleroId", "FechaPrestamo" });

            migrationBuilder.CreateIndex(
                name: "IX_prestamos_casilleros_ClienteId",
                table: "prestamos_casilleros",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_prestamos_casilleros_IngresoId",
                table: "prestamos_casilleros",
                column: "IngresoId");

            migrationBuilder.CreateIndex(
                name: "IX_promociones_festivas_FechaInicio_FechaFin",
                table: "promociones_festivas",
                columns: new[] { "FechaInicio", "FechaFin" });

            migrationBuilder.CreateIndex(
                name: "IX_Suscripcions_CasilleroFijoId",
                table: "Suscripcions",
                column: "CasilleroFijoId");

            migrationBuilder.CreateIndex(
                name: "IX_Suscripcions_ClienteId",
                table: "Suscripcions",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Suscripcions_ClienteId_Estado",
                table: "Suscripcions",
                columns: new[] { "ClienteId", "Estado" });

            migrationBuilder.CreateIndex(
                name: "IX_Suscripcions_Estado",
                table: "Suscripcions",
                column: "Estado");

            migrationBuilder.CreateIndex(
                name: "IX_Suscripcions_PromocionId",
                table: "Suscripcions",
                column: "PromocionId");

            migrationBuilder.CreateIndex(
                name: "IX_Suscripcions_Tipo",
                table: "Suscripcions",
                column: "Tipo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "prestamos_casilleros");

            migrationBuilder.DropTable(
                name: "ingresos");

            migrationBuilder.DropTable(
                name: "Suscripcions");

            migrationBuilder.DropTable(
                name: "casilleros");

            migrationBuilder.DropTable(
                name: "promociones_festivas");

            migrationBuilder.DropTable(
                name: "clientes");
        }
    }
}
