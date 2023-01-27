using System;
using System.Collections.Generic;

namespace scrap.Models;

public partial class Carte
{
	public int Id { get; set; }

	public string Titre { get; set; } = null!;

	public string? Description { get; set; }

	public DateTime DateCreation { get; set; }

	public int IdListe { get; set; }

	public virtual List<Commentaire> Commentaires { get; } = new List<Commentaire>();

	public virtual List<Etiquette> Etiquettes { get; } = new List<Etiquette>();

	public virtual Liste IdListeNavigation { get; set; } = null!;

	public Carte()
	{
	}

	public Carte(string titre, string? description, DateTime dateCreation = default)
	{
		Titre = titre;
		Description = description;
		DateCreation = dateCreation;
	}

	public Carte(string titre, string? description, Liste newListe, DateTime dateCreation = default)
	{
		Titre = titre;
		Description = description;
		DateCreation = dateCreation;
		IdListeNavigation = newListe;
		IdListe = newListe.Id;
		IdListeNavigation.Cartes.Add(this);
	}

	public void changePosition(Liste newListe)
	{
		IdListeNavigation.Cartes.Remove(this);
		IdListeNavigation = newListe;
		IdListe = newListe.Id;
		IdListeNavigation.Cartes.Add(this);
	}

	public void addCommentaire(Commentaire newCommentaire)
	{
		Commentaires.Add(newCommentaire);
	}

	public void removeCommentaire(Commentaire oldCommentaire)
	{
		Commentaires.Remove(oldCommentaire);
	}

	public void addEtiquette(Etiquette newEtiquette)
	{
		Etiquettes.Add(newEtiquette);
	}

	public void removeEtiquette(Etiquette oldEtiquette)
	{
		Etiquettes.Remove(oldEtiquette);
	}

	public void changeListe(Liste newListe)
	{
		IdListeNavigation.Cartes.Remove(this);
		IdListeNavigation = newListe;
		IdListe = newListe.Id;
		IdListeNavigation.Cartes.Add(this);
	}

	public void changeTitre(string newTitre)
	{
		Titre = newTitre;
	}

	public void changeDescription(string newDescription)
	{
		Description = newDescription;
	}

	public List<Commentaire> GetCommentaires()
	{
		return Commentaires;
	}

	public List<Etiquette> GetEtiquettes()
	{
		return Etiquettes;
	}
	public List<Carte> SortByDate(List<Carte> cartes)
	{
		cartes.Sort((x, y) => x.DateCreation.CompareTo(y.DateCreation));
		return cartes;
	}
}

