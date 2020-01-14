﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SCA.Service.Monitoring.Data;

namespace SCA.Service.Monitoring.Migrations
{
    [DbContext(typeof(MonitoramentoContext))]
    [Migration("20200113132009_AlertasCorrecao2")]
    partial class AlertasCorrecao2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SCA.Shared.Entities.Alert.Cadastro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("RegiaoId")
                        .HasColumnType("int");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("varchar(20) CHARACTER SET utf8mb4")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("RegiaoId");

                    b.ToTable("Cadastro");
                });

            modelBuilder.Entity("SCA.Shared.Entities.Monitoring.Barragem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("RegiaoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RegiaoId");

                    b.ToTable("Barragem");
                });

            modelBuilder.Entity("SCA.Shared.Entities.Monitoring.Regiao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Cidade")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UF")
                        .HasColumnType("varchar(2) CHARACTER SET utf8mb4")
                        .HasMaxLength(2);

                    b.HasKey("Id");

                    b.ToTable("Regiao");
                });

            modelBuilder.Entity("SCA.Shared.Entities.Monitoring.Sensor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BarragemId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("BarragemId");

                    b.ToTable("Sensor");
                });

            modelBuilder.Entity("SCA.Shared.Entities.Monitoring.SensorHistorico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("SensorId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SensorId");

                    b.ToTable("SensorHistorico");
                });

            modelBuilder.Entity("SCA.Shared.Entities.Alert.Cadastro", b =>
                {
                    b.HasOne("SCA.Shared.Entities.Monitoring.Regiao", "Regiao")
                        .WithMany("Alerts")
                        .HasForeignKey("RegiaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SCA.Shared.Entities.Monitoring.Barragem", b =>
                {
                    b.HasOne("SCA.Shared.Entities.Monitoring.Regiao", "Regiao")
                        .WithMany("Barragens")
                        .HasForeignKey("RegiaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SCA.Shared.Entities.Monitoring.Sensor", b =>
                {
                    b.HasOne("SCA.Shared.Entities.Monitoring.Barragem", "Barragem")
                        .WithMany("Sensores")
                        .HasForeignKey("BarragemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SCA.Shared.Entities.Monitoring.SensorHistorico", b =>
                {
                    b.HasOne("SCA.Shared.Entities.Monitoring.Sensor", "Sensor")
                        .WithMany("Historico")
                        .HasForeignKey("SensorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}