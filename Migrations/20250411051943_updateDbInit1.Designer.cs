﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using webAPI.Data;

#nullable disable

namespace MyWebApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250411051943_updateDbInit1")]
    partial class updateDbInit1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MyWebApi.Model.CaLam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("GioBatDau")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("GioKetThuc")
                        .HasColumnType("datetime");

                    b.Property<string>("TenCa")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("tbl_CaLam", (string)null);
                });

            modelBuilder.Entity("MyWebApi.Model.ChucVu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("TenChucVu")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("tbl_ChucVu", (string)null);
                });

            modelBuilder.Entity("MyWebApi.Model.NhanVien", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CaLamId")
                        .HasColumnType("int");

                    b.Property<int>("ChucVuId")
                        .HasColumnType("int");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("LuongCoBan")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CaLamId");

                    b.HasIndex("ChucVuId");

                    b.ToTable("tbl_NhanVien", (string)null);
                });

            modelBuilder.Entity("MyWebApi.Model.TaiKhoan", b =>
                {
                    b.Property<string>("MaTK")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("HinhAnh")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<int>("LoaiTK")
                        .HasColumnType("int");

                    b.Property<string>("MatKhau")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("TenHienThi")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("TenTK")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("Xoa")
                        .HasColumnType("bit");

                    b.HasKey("MaTK");

                    b.HasIndex("LoaiTK");

                    b.ToTable("tbl_TaiKhoan", (string)null);
                });

            modelBuilder.Entity("MyWebApi.Model.VaiTro", b =>
                {
                    b.Property<int>("MaLoai")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaLoai"));

                    b.Property<string>("TenLoai")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("MaLoai");

                    b.ToTable("tbl_VaiTro", (string)null);
                });

            modelBuilder.Entity("MyWebApi.Model.NhanVien", b =>
                {
                    b.HasOne("MyWebApi.Model.CaLam", "CaLam")
                        .WithMany("NhanViens")
                        .HasForeignKey("CaLamId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MyWebApi.Model.ChucVu", "ChucVu")
                        .WithMany("NhanViens")
                        .HasForeignKey("ChucVuId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CaLam");

                    b.Navigation("ChucVu");
                });

            modelBuilder.Entity("MyWebApi.Model.TaiKhoan", b =>
                {
                    b.HasOne("MyWebApi.Model.VaiTro", "VaiTro")
                        .WithMany("TaiKhoans")
                        .HasForeignKey("LoaiTK")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("VaiTro");
                });

            modelBuilder.Entity("MyWebApi.Model.CaLam", b =>
                {
                    b.Navigation("NhanViens");
                });

            modelBuilder.Entity("MyWebApi.Model.ChucVu", b =>
                {
                    b.Navigation("NhanViens");
                });

            modelBuilder.Entity("MyWebApi.Model.VaiTro", b =>
                {
                    b.Navigation("TaiKhoans");
                });
#pragma warning restore 612, 618
        }
    }
}
