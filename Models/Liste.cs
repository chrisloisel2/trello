using System;
using System.Collections.Generic;

namespace scrap.Models;

public partial class Liste
{
	public int Id { get; set; }

	public string Nom { get; set; } = null!;

	public int IdProjet { get; set; }

	public virtual List<Carte> Cartes { get; } = new List<Carte>();

	public virtual Projet IdProjetNavigation { get; set; } = new Projet();

	public Liste()
	{
	}

	public Liste(string nom, Projet ProjetNavigation)
	{
		Nom = nom;
		ProjetNavigation.Listes.Add(this);
		IdProjetNavigation = ProjetNavigation;
		IdProjet = ProjetNavigation.Id;
	}

	public void AddCarte(Carte carte)
	{
		Cartes.Add(carte);
		carte.IdListeNavigation = this;
		carte.IdListe = Id;
	}

	public void RemoveCarte(Carte carte)
	{
		Cartes.Remove(carte);
		carte.IdListeNavigation = null;
		carte.IdListe = 0;
	}

	public void SetProjet(Projet projet)
	{
		IdProjetNavigation = projet;
		IdProjet = projet.Id;
	}

	public void RemoveProjet()
	{
		IdProjetNavigation = null;
		IdProjet = 0;
	}

	public List<Carte> GetCartes()
	{
		return Cartes;
	}


	public void insertCarte(Carte carte, int index)
	{
		Cartes.Insert(index, carte);
		carte.IdListeNavigation = this;
		carte.IdListe = Id;
	}
}
