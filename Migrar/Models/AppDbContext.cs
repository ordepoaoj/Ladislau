using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace Web20.Models
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aquisicao> Aquisicaos { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<ContinenteEditor> ContinenteEditors { get; set; }
        public virtual DbSet<Editor> Editors { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistories { get; set; }
        public virtual DbSet<MuseuEditor> MuseuEditors { get; set; }
        public virtual DbSet<PaisEditor> PaisEditors { get; set; }
        public virtual DbSet<Periodicidade> Periodicidades { get; set; }
        public virtual DbSet<PreferenciaEditor> PreferenciaEditors { get; set; }
        public virtual DbSet<RevMuseu> RevMuseus { get; set; }
        public virtual DbSet<RevistaMuseu> RevistaMuseus { get; set; }
        public virtual DbSet<Revistum> Revista { get; set; }
        public virtual DbSet<View> Views { get; set; }

        public IConfiguration Configuration { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("Web20ContextConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Aquisicao>(entity =>
            {
                entity.ToTable("Aquisicao");

                entity.Property(e => e.TipoAquisicao)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Tipo_Aquisicao");
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<ContinenteEditor>(entity =>
            {
                entity.ToTable("Continente_Editor");

                entity.Property(e => e.NomeContinente)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Nome_Continente");
            });

            modelBuilder.Entity<Editor>(entity =>
            {
                entity.ToTable("Editor");

                entity.HasIndex(e => e.NomeEditor, "AK_Editor")
                    .IsUnique();

                entity.Property(e => e.CodPais).HasColumnName("Cod_Pais");

                entity.Property(e => e.CodPostal).HasMaxLength(30);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Endereco)
                    .HasMaxLength(750)
                    .IsUnicode(false);

                entity.Property(e => e.NomeEditor)
                    .IsRequired()
                    .HasMaxLength(700)
                    .IsUnicode(false)
                    .HasColumnName("Nome_Editor");

                entity.Property(e => e.Telefone).IsUnicode(false);

                entity.HasOne(d => d.CodPaisNavigation)
                    .WithMany(p => p.Editors)
                    .HasForeignKey(d => d.CodPais)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_pais_Editor");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<MuseuEditor>(entity =>
            {
                entity.ToTable("Museu_Editor");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.IdEditor).HasColumnName("Id_Editor");

                entity.Property(e => e.IdMuseu).HasColumnName("Id_Museu");

                entity.HasOne(d => d.IdEditorNavigation)
                    .WithMany(p => p.MuseuEditors)
                    .HasForeignKey(d => d.IdEditor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Editor_Museu_Editor ");

                entity.HasOne(d => d.IdMuseuNavigation)
                    .WithMany(p => p.MuseuEditors)
                    .HasForeignKey(d => d.IdMuseu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Editor_Editor_Museu ");
            });

            modelBuilder.Entity<PaisEditor>(entity =>
            {
                entity.ToTable("Pais_Editor");

                entity.Property(e => e.CodContinente).HasColumnName("Cod_Continente");

                entity.Property(e => e.NomePais)
                    .IsUnicode(false)
                    .HasColumnName("Nome_Pais");

                entity.HasOne(d => d.CodContinenteNavigation)
                    .WithMany(p => p.PaisEditors)
                    .HasForeignKey(d => d.CodContinente)
                    .HasConstraintName("fk_cpm_Editor");
            });

            modelBuilder.Entity<Periodicidade>(entity =>
            {
                entity.ToTable("Periodicidade");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.TipoPeriodicidade)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Tipo_Periodicidade");
            });

            modelBuilder.Entity<PreferenciaEditor>(entity =>
            {
                entity.HasKey(e => new { e.CodPublicacao, e.CodEditor })
                    .HasName("PK__Preferen__7E9492BB1D286EF4");

                entity.ToTable("Preferencia_Editor");

                entity.Property(e => e.CodPublicacao).HasColumnName("Cod_publicacao");

                entity.Property(e => e.CodEditor).HasColumnName("Cod_Editor");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.CodEditorNavigation)
                    .WithMany(p => p.PreferenciaEditors)
                    .HasForeignKey(d => d.CodEditor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cod_Editor");
            });

            modelBuilder.Entity<RevMuseu>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Rev_Museu");

                entity.Property(e => e.CdPeriodicidade).HasColumnName("cd_Periodicidade");

                entity.Property(e => e.Ibict)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("IBICT");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Issn)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ISSN");

                entity.Property(e => e.Titulo).IsUnicode(false);
            });

            modelBuilder.Entity<RevistaMuseu>(entity =>
            {
                entity.ToTable("Revista_Museu");

                entity.Property(e => e.CdPeriodicidade).HasColumnName("cd_Periodicidade");

                entity.Property(e => e.Ibict)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("IBICT");

                entity.Property(e => e.Issn)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ISSN");

                entity.Property(e => e.Titulo).IsUnicode(false);
            });

            modelBuilder.Entity<Revistum>(entity =>
            {
                entity.HasIndex(e => e.Issn, "UQ__Revista__447D3E9665F5288E")
                    .IsUnique();

                entity.HasIndex(e => e.Issn, "UQ__Revista__447D3E966BA65F1F")
                    .IsUnique();

                entity.HasIndex(e => e.Aleph, "UQ__Revista__77BBC3442CC80D46")
                    .IsUnique();

                entity.HasIndex(e => e.Aleph, "UQ__Revista__77BBC344F33880D5")
                    .IsUnique();

                entity.HasIndex(e => e.Titulo, "UQ__Revista__7B406B5625EF3194")
                    .IsUnique();

                entity.HasIndex(e => e.Ibict, "UQ__Revista__8D98C9B9524AE8D9")
                    .IsUnique();

                entity.HasIndex(e => e.Ibict, "UQ__Revista__8D98C9B9BDB348D1")
                    .IsUnique();

                entity.Property(e => e.Aleph)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CdAquisicao).HasColumnName("cd_Aquisicao");

                entity.Property(e => e.CdEditor).HasColumnName("cd_Editor");

                entity.Property(e => e.CdPeriodicidade).HasColumnName("cd_Periodicidade");

                entity.Property(e => e.Chegada).HasColumnType("date");

                entity.Property(e => e.Ibict)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("IBICT");

                entity.Property(e => e.Issn)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ISSN");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(750)
                    .IsUnicode(false);

                entity.HasOne(d => d.CdAquisicaoNavigation)
                    .WithMany(p => p.Revista)
                    .HasForeignKey(d => d.CdAquisicao)
                    .HasConstraintName("fk_cod_Aquisicao_Revista");

                entity.HasOne(d => d.CdEditorNavigation)
                    .WithMany(p => p.Revista)
                    .HasForeignKey(d => d.CdEditor)
                    .HasConstraintName("fk_cod_Editor_Revista");

                entity.HasOne(d => d.CdPeriodicidadeNavigation)
                    .WithMany(p => p.Revista)
                    .HasForeignKey(d => d.CdPeriodicidade)
                    .HasConstraintName("fk_cod_Periodicidade_Revista");
            });

            modelBuilder.Entity<View>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View");

                entity.Property(e => e.Aleph)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Chegada).HasColumnType("date");

                entity.Property(e => e.Ibict)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("IBICT");

                entity.Property(e => e.Issn)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ISSN");

                entity.Property(e => e.NomeEditor)
                    .IsRequired()
                    .HasMaxLength(700)
                    .IsUnicode(false)
                    .HasColumnName("Nome_Editor");

                entity.Property(e => e.TipoAquisicao)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Tipo_Aquisicao");

                entity.Property(e => e.TipoPeriodicidade)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Tipo_Periodicidade");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(750)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
