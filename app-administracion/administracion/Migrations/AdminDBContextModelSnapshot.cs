﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using administracion.DataAccess.Database;

#nullable disable

namespace administracion.Migrations
{
    [DbContext(typeof(AdminDBContext))]
    partial class AdminDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("administracion.DataAccess.Entities.Asegurado", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("apellido")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Asegurados");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0c5c3262-d5ef-46c7-0001-000000000001"),
                            apellido = "Ramirez Gimenez",
                            nombre = "Luis Jose"
                        },
                        new
                        {
                            Id = new Guid("0c5c3262-d5ef-46c7-0001-000000000002"),
                            apellido = "Banderas Lopez",
                            nombre = "Manuel Diego"
                        },
                        new
                        {
                            Id = new Guid("0c5c3262-d5ef-46c7-0001-000000000003"),
                            apellido = "Gimenez",
                            nombre = "Daniel"
                        },
                        new
                        {
                            Id = new Guid("0c5c3262-d5ef-46c7-0001-000000000004"),
                            apellido = "Salaguchi",
                            nombre = "Maria Jose"
                        });
                });

            modelBuilder.Entity("administracion.DataAccess.Entities.Incidente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("estadoIncidente")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("fechaFinalizado")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("fechaRegistrado")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("polizaId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("polizaId");

                    b.ToTable("Incidentes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0c5c3262-d5ef-46c7-0004-000000000001"),
                            estadoIncidente = 0,
                            fechaRegistrado = new DateTime(2010, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            polizaId = new Guid("0c5c3262-d5ef-46c7-0003-000000000001")
                        },
                        new
                        {
                            Id = new Guid("0c5c3262-d5ef-46c7-0004-000000000002"),
                            estadoIncidente = 0,
                            fechaRegistrado = new DateTime(2018, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            polizaId = new Guid("0c5c3262-d5ef-46c7-0003-000000000002")
                        },
                        new
                        {
                            Id = new Guid("0c5c3262-d5ef-46c7-0004-000000000003"),
                            estadoIncidente = 0,
                            fechaRegistrado = new DateTime(2021, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            polizaId = new Guid("0c5c3262-d5ef-46c7-0003-000000000003")
                        });
                });

            modelBuilder.Entity("administracion.DataAccess.Entities.MarcaProveedor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("manejaTodas")
                        .HasColumnType("boolean");

                    b.Property<int?>("marca")
                        .HasColumnType("integer");

                    b.Property<Guid>("proveedorId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("tallerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("tallerId");

                    b.ToTable("MarcasProveedor");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000001-d5ef-46c7-0006-000000000001"),
                            manejaTodas = false,
                            marca = 8,
                            proveedorId = new Guid("0c5c3262-d5ef-46c7-0006-000000000001")
                        },
                        new
                        {
                            Id = new Guid("00000002-d5ef-46c7-0006-000000000001"),
                            manejaTodas = false,
                            marca = 5,
                            proveedorId = new Guid("0c5c3262-d5ef-46c7-0006-000000000001")
                        },
                        new
                        {
                            Id = new Guid("00000003-d5ef-46c7-0006-000000000001"),
                            manejaTodas = false,
                            marca = 7,
                            proveedorId = new Guid("0c5c3262-d5ef-46c7-0006-000000000001")
                        },
                        new
                        {
                            Id = new Guid("00000004-d5ef-46c7-0006-000000000001"),
                            manejaTodas = false,
                            marca = 0,
                            proveedorId = new Guid("0c5c3262-d5ef-46c7-0006-000000000001")
                        },
                        new
                        {
                            Id = new Guid("00000001-d5ef-46c7-0006-000000000002"),
                            manejaTodas = true,
                            proveedorId = new Guid("0c5c3262-d5ef-46c7-0006-000000000002")
                        });
                });

            modelBuilder.Entity("administracion.DataAccess.Entities.MarcaTaller", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("manejaTodas")
                        .HasColumnType("boolean");

                    b.Property<int?>("marca")
                        .HasColumnType("integer");

                    b.Property<Guid>("tallerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("tallerId");

                    b.ToTable("MarcasTaller");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000001-d5ef-46c7-0005-000000000001"),
                            manejaTodas = false,
                            marca = 8,
                            tallerId = new Guid("0c5c3262-d5ef-46c7-0005-000000000001")
                        },
                        new
                        {
                            Id = new Guid("00000002-d5ef-46c7-0005-000000000001"),
                            manejaTodas = false,
                            marca = 4,
                            tallerId = new Guid("0c5c3262-d5ef-46c7-0005-000000000001")
                        },
                        new
                        {
                            Id = new Guid("00000003-d5ef-46c7-0005-000000000001"),
                            manejaTodas = false,
                            marca = 7,
                            tallerId = new Guid("0c5c3262-d5ef-46c7-0005-000000000001")
                        },
                        new
                        {
                            Id = new Guid("00000001-d5ef-46c7-0005-000000000002"),
                            manejaTodas = true,
                            tallerId = new Guid("0c5c3262-d5ef-46c7-0005-000000000002")
                        });
                });

            modelBuilder.Entity("administracion.DataAccess.Entities.Poliza", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("fechaRegistro")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("fechaVencimiento")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("tipoPoliza")
                        .HasColumnType("integer");

                    b.Property<Guid>("vehiculoId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("vehiculoId");

                    b.ToTable("Polizas");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0c5c3262-d5ef-46c7-0003-000000000001"),
                            fechaRegistro = new DateTime(2009, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            fechaVencimiento = new DateTime(2014, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            tipoPoliza = 0,
                            vehiculoId = new Guid("0c5c3262-d5ef-46c7-0002-000000000001")
                        },
                        new
                        {
                            Id = new Guid("0c5c3262-d5ef-46c7-0003-000000000002"),
                            fechaRegistro = new DateTime(2016, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            fechaVencimiento = new DateTime(2020, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            tipoPoliza = 1,
                            vehiculoId = new Guid("0c5c3262-d5ef-46c7-0002-000000000002")
                        },
                        new
                        {
                            Id = new Guid("0c5c3262-d5ef-46c7-0003-000000000003"),
                            fechaRegistro = new DateTime(2020, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            fechaVencimiento = new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            tipoPoliza = 0,
                            vehiculoId = new Guid("0c5c3262-d5ef-46c7-0002-000000000003")
                        });
                });

            modelBuilder.Entity("administracion.DataAccess.Entities.Proveedor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("nombreLocal")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Proveedores");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0c5c3262-d5ef-46c7-0006-000000000001"),
                            nombreLocal = "Todo en partes 3000"
                        },
                        new
                        {
                            Id = new Guid("0c5c3262-d5ef-46c7-0006-000000000002"),
                            nombreLocal = "Tu Carro, tu repuesto"
                        });
                });

            modelBuilder.Entity("administracion.DataAccess.Entities.Taller", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("nombreLocal")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Talleres");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0c5c3262-d5ef-46c7-0005-000000000001"),
                            nombreLocal = "Gas Monkey"
                        },
                        new
                        {
                            Id = new Guid("0c5c3262-d5ef-46c7-0005-000000000002"),
                            nombreLocal = "Taller de Luis"
                        });
                });

            modelBuilder.Entity("administracion.DataAccess.Entities.Vehiculo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("anioModelo")
                        .HasColumnType("integer");

                    b.Property<Guid?>("aseguradoId")
                        .HasColumnType("uuid");

                    b.Property<int>("color")
                        .HasColumnType("integer");

                    b.Property<DateTime>("fechaCompra")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("marca")
                        .HasColumnType("integer");

                    b.Property<string>("placa")
                        .HasMaxLength(7)
                        .HasColumnType("character varying(7)");

                    b.HasKey("Id");

                    b.HasIndex("aseguradoId");

                    b.ToTable("Vehiculos");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0c5c3262-d5ef-46c7-0002-000000000001"),
                            anioModelo = 2007,
                            aseguradoId = new Guid("0c5c3262-d5ef-46c7-0001-000000000001"),
                            color = 1,
                            fechaCompra = new DateTime(2007, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            marca = 0,
                            placa = "AB320AM"
                        },
                        new
                        {
                            Id = new Guid("0c5c3262-d5ef-46c7-0002-000000000002"),
                            anioModelo = 2006,
                            aseguradoId = new Guid("0c5c3262-d5ef-46c7-0001-000000000002"),
                            color = 6,
                            fechaCompra = new DateTime(2014, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            marca = 7,
                            placa = "AB322AM"
                        },
                        new
                        {
                            Id = new Guid("0c5c3262-d5ef-46c7-0002-000000000003"),
                            anioModelo = 2016,
                            aseguradoId = new Guid("0c5c3262-d5ef-46c7-0001-000000000003"),
                            color = 5,
                            fechaCompra = new DateTime(2017, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            marca = 8,
                            placa = "BB322AC"
                        },
                        new
                        {
                            Id = new Guid("0c5c3262-d5ef-46c7-0002-000000000004"),
                            anioModelo = 2020,
                            aseguradoId = new Guid("0c5c3262-d5ef-46c7-0001-000000000003"),
                            color = 9,
                            fechaCompra = new DateTime(2020, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            marca = 4,
                            placa = "BB329AC"
                        });
                });

            modelBuilder.Entity("administracion.DataAccess.Entities.Incidente", b =>
                {
                    b.HasOne("administracion.DataAccess.Entities.Poliza", "poliza")
                        .WithMany("incidente")
                        .HasForeignKey("polizaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("poliza");
                });

            modelBuilder.Entity("administracion.DataAccess.Entities.MarcaProveedor", b =>
                {
                    b.HasOne("administracion.DataAccess.Entities.Proveedor", "proveedor")
                        .WithMany("marcas")
                        .HasForeignKey("tallerId");

                    b.Navigation("proveedor");
                });

            modelBuilder.Entity("administracion.DataAccess.Entities.MarcaTaller", b =>
                {
                    b.HasOne("administracion.DataAccess.Entities.Taller", "taller")
                        .WithMany("marcas")
                        .HasForeignKey("tallerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("taller");
                });

            modelBuilder.Entity("administracion.DataAccess.Entities.Poliza", b =>
                {
                    b.HasOne("administracion.DataAccess.Entities.Vehiculo", "vehiculo")
                        .WithMany("polizas")
                        .HasForeignKey("vehiculoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("vehiculo");
                });

            modelBuilder.Entity("administracion.DataAccess.Entities.Vehiculo", b =>
                {
                    b.HasOne("administracion.DataAccess.Entities.Asegurado", "asegurado")
                        .WithMany("vehiculos")
                        .HasForeignKey("aseguradoId");

                    b.Navigation("asegurado");
                });

            modelBuilder.Entity("administracion.DataAccess.Entities.Asegurado", b =>
                {
                    b.Navigation("vehiculos");
                });

            modelBuilder.Entity("administracion.DataAccess.Entities.Poliza", b =>
                {
                    b.Navigation("incidente");
                });

            modelBuilder.Entity("administracion.DataAccess.Entities.Proveedor", b =>
                {
                    b.Navigation("marcas");
                });

            modelBuilder.Entity("administracion.DataAccess.Entities.Taller", b =>
                {
                    b.Navigation("marcas");
                });

            modelBuilder.Entity("administracion.DataAccess.Entities.Vehiculo", b =>
                {
                    b.Navigation("polizas");
                });
#pragma warning restore 612, 618
        }
    }
}
