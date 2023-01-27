using System;
using System.Collections.Generic;

namespace scrap.Models;

public partial class Projet
{
	public int Id { get; set; }

	public string Nom { get; set; } = null!;

	public string? Description { get; set; }

	public DateTime DateCreation { get; set; }

	public virtual List<Liste> Listes { get; } = new List<Liste>();

	public virtual List<Utilisateur> IdUtilisateurs { get; } = new List<Utilisateur>();

	public Projet()
	{
	}

	public Projet(string nom, string? description, DateTime dateCreation = default)//methode Projetfirst
	{
		Nom = nom;
		Description = description;
		DateCreation = dateCreation;
	}

	public Projet(string nom, string? description, List<Utilisateur> utilisateurs, DateTime dateCreation = default)// methode Utilisateurfirst
	{
		Nom = nom;
		Description = description;
		DateCreation = dateCreation;
		IdUtilisateurs = utilisateurs;
		foreach (var utilisateur in utilisateurs)
		{
			utilisateur.IdProjet.Add(this);
		}
	}

	public void AddUtilisateur(Utilisateur utilisateur)
	{
		// ma relation many to many est dans la table UtilisateurProjet
		IdUtilisateurs.Add(utilisateur);
		utilisateur.IdProjet.Add(this);
	}

	public void AddListe(Liste liste)
	{
		Listes.Add(liste);
		liste.IdProjetNavigation = this;
	}

	public List<Utilisateur> GetUtilisateurs()
	{
		return IdUtilisateurs;
	}

	public List<Liste> GetListes()
	{
		return Listes;
	}

}
