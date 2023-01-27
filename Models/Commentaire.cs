using System;
using System.Collections.Generic;

namespace scrap.Models;

public partial class Commentaire
{
	public int Id { get; set; }

	public string Contenu { get; set; } = null!;

	public DateTime DateCreation { get; set; }

	public int IdUtilisateur { get; set; }

	public int IdCarte { get; set; }

	public virtual Carte IdCarteNavigation { get; set; } = null!;

	public virtual Utilisateur IdUtilisateurNavigation { get; set; } = null!;

	public Commentaire()
	{
	}

	public Commentaire(string contenu, Carte carteNavigation, Utilisateur utilisateurNavigation, DateTime dateCreation = default)
	{
		Contenu = contenu;
		DateCreation = dateCreation;
		IdCarteNavigation = carteNavigation;
		IdCarte = carteNavigation.Id;
		IdUtilisateurNavigation = utilisateurNavigation;
		IdUtilisateur = utilisateurNavigation.Id;
	}

	public Carte GetCarte()
	{
		return IdCarteNavigation;
	}

	public Utilisateur GetUtilisateur()
	{
		return IdUtilisateurNavigation;
	}
}
