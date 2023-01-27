using System;
using System.Collections.Generic;

namespace scrap.Models;

public partial class Etiquette
{
	public int Id { get; set; }

	public string Nom { get; set; } = null!;

	public string Couleur { get; set; } = null!;

	public int IdCarte { get; set; }

	public virtual Carte IdCarteNavigation { get; set; } = null!;

	public Etiquette()
	{
	}

	public Etiquette(string nom, string couleur, Carte cartenavigation)
	{
		Nom = nom;
		Couleur = couleur;
		IdCarteNavigation = cartenavigation;
		IdCarte = cartenavigation.Id;
	}

	public ICollection<Carte> GetCartes()
	{
		return new List<Carte> { IdCarteNavigation };
	}
}
