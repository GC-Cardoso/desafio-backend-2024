﻿// <auto-generated />
using System;
using Desafio.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Desafio.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241226033503_v0.3")]
    partial class v03
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Desafio.Core.Models.Conta", b =>
                {
                    b.Property<int>("contaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("contaId"));

                    b.Property<string>("agencia")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("cnpj")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("imagemDocumento")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasMaxLength(125)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("numeroConta")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("VARCHAR");

                    b.Property<decimal>("saldo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DECIMAL(10, 2)")
                        .HasDefaultValue(0m);

                    b.HasKey("contaId");

                    b.ToTable("Contas", (string)null);
                });

            modelBuilder.Entity("Desafio.Core.Models.Transacao", b =>
                {
                    b.Property<int>("transacaoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("transacaoId"));

                    b.Property<int?>("contaAlvoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("contaId")
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("tipoMovimento")
                        .HasColumnType("BIT");

                    b.Property<decimal>("valor")
                        .HasColumnType("DECIMAL(10, 2)");

                    b.HasKey("transacaoId");

                    b.ToTable("Transacoes", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}