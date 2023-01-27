using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace scrap.Models;

public partial class TrelloContext : DbContext
{
	public TrelloContext()
	{
		Database.EnsureDeleted();
		Database.EnsureCreated();
	}

	public TrelloContext(DbContextOptions<TrelloContext> options)
		: base(options)
	{
		Database.EnsureDeleted();
		Database.EnsureCreated();
	}

	public virtual DbSet<Carte> Cartes { get; set; }

	public virtual DbSet<Commentaire> Commentaires { get; set; }

	public virtual DbSet<Etiquette> Etiquettes { get; set; }

	public virtual DbSet<Liste> Listes { get; set; }

	public virtual DbSet<Projet> Tableaus { get; set; }

	public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseMySQL("Server=localhost;Port=3306;Database=trello;User=root;Password=0000;");
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Carte>(entity =>
		{
			entity.HasOne(d => d.IdListeNavigation).WithMany(p => p.Cartes)
				.HasForeignKey(d => d.IdListe)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("carte_ibfk_1");
		});

		modelBuilder.Entity<Commentaire>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PRIMARY");

			entity.ToTable("commentaire");

			entity.HasIndex(e => e.IdCarte, "id_carte");

			entity.HasIndex(e => e.IdUtilisateur, "id_utilisateur");

			entity.Property(e => e.Id)
				.HasColumnType("int(11)")
				.HasColumnName("id");
			entity.Property(e => e.Contenu)
				.HasColumnType("text")
				.HasColumnName("contenu");
			entity.Property(e => e.DateCreation)
				.HasColumnType("date")
				.HasColumnName("date_creation");
			entity.Property(e => e.IdCarte)
				.HasColumnType("int(11)")
				.HasColumnName("id_carte");
			entity.Property(e => e.IdUtilisateur)
				.HasColumnType("int(11)")
				.HasColumnName("id_utilisateur");

			entity.HasOne(d => d.IdCarteNavigation).WithMany(p => p.Commentaires)
				.HasForeignKey(d => d.IdCarte)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("commentaire_ibfk_2");

			entity.HasOne(d => d.IdUtilisateurNavigation).WithMany(p => p.Commentaires)//definir une relation
				.HasForeignKey(d => d.IdUtilisateur)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("commentaire_ibfk_1");
		});

		modelBuilder.Entity<Etiquette>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PRIMARY");

			entity.ToTable("etiquette");

			entity.HasIndex(e => e.IdCarte, "id_carte");

			entity.Property(e => e.Id)
				.HasColumnType("int(11)")
				.HasColumnName("id");
			entity.Property(e => e.Couleur)
				.HasMaxLength(255)
				.HasColumnName("couleur");
			entity.Property(e => e.IdCarte)
				.HasColumnType("int(11)")
				.HasColumnName("id_carte");
			entity.Property(e => e.Nom)
				.HasMaxLength(255)
				.HasColumnName("nom");

			entity.HasOne(d => d.IdCarteNavigation).WithMany(p => p.Etiquettes)
				.HasForeignKey(d => d.IdCarte)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("etiquette_ibfk_1");
		});

		modelBuilder.Entity<Liste>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PRIMARY");

			entity.ToTable("liste");

			entity.HasIndex(e => e.IdProjet, "id_tableau");

			entity.Property(e => e.Id)
				.HasColumnType("int(11)")
				.HasColumnName("id");
			entity.Property(e => e.IdProjet)
				.HasColumnType("int(11)")
				.HasColumnName("id_tableau");
			entity.Property(e => e.Nom)
				.HasMaxLength(255)
				.HasColumnName("nom");

			entity.HasOne(d => d.IdProjetNavigation).WithMany(p => p.Listes)
				.HasForeignKey(d => d.IdProjet)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("liste_ibfk_1");
		});

		modelBuilder.Entity<Projet>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PRIMARY");

			entity.ToTable("tableau");

			entity.Property(e => e.Id)
				.HasColumnType("int(11)")
				.HasColumnName("id");
			entity.Property(e => e.DateCreation)
				.HasColumnType("date")
				.HasColumnName("date_creation");
			entity.Property(e => e.Description)
				.HasDefaultValueSql("'NULL'")
				.HasColumnType("text")
				.HasColumnName("description");
			entity.Property(e => e.Nom)
				.HasMaxLength(255)
				.HasColumnName("nom");
		});

		modelBuilder.Entity<Utilisateur>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PRIMARY");

			entity.ToTable("utilisateur");

			entity.Property(e => e.Id)
				.HasColumnType("int(11)")
				.HasColumnName("id");
			entity.Property(e => e.DateInscription)
				.HasColumnType("date")
				.HasColumnName("date_inscription");
			entity.Property(e => e.Email)
				.HasMaxLength(255)
				.HasColumnName("email");
			entity.Property(e => e.MotDePasse)
				.HasMaxLength(255)
				.HasColumnName("mot_de_passe");
			entity.Property(e => e.Nom)
				.HasMaxLength(255)
				.HasColumnName("nom");

			entity.HasMany(d => d.IdProjet).WithMany(p => p.IdUtilisateurs)
				.UsingEntity<Dictionary<string, object>>(
					"UtilisateurTableau",
					r => r.HasOne<Projet>().WithMany()
						.HasForeignKey("IdProjet")
						.OnDelete(DeleteBehavior.Restrict)
						.HasConstraintName("utilisateur_tableau_ibfk_2"),
					l => l.HasOne<Utilisateur>().WithMany()
						.HasForeignKey("IdUtilisateur")
						.OnDelete(DeleteBehavior.Restrict)
						.HasConstraintName("utilisateur_tableau_ibfk_1"),
					j =>
					{
						j.HasKey("IdUtilisateur", "IdProjet").HasName("PRIMARY");
						j.ToTable("utilisateur_tableau");
						j.HasIndex(new[] { "IdProjet" }, "id_tableau");
					});
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
