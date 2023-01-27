using Microsoft.EntityFrameworkCore.SqlServer;
namespace scrap.Models;


class Programm
{
	static void Main(string[] args)
	{
		var context = new TrelloContext();

		Repository<Utilisateur> repoUtilisateur = new Repository<Utilisateur>(context);
		Repository<Projet> repoProjet = new Repository<Projet>(context);
		Repository<Liste> repoListe = new Repository<Liste>(context);
		Repository<Carte> repoCarte = new Repository<Carte>(context);
		Repository<Commentaire> repoCommentaire = new Repository<Commentaire>(context);
		Repository<Etiquette> repoEtiquette = new Repository<Etiquette>(context);

		var proj = new Projet("Trello", "Créer la BDD");
		repoProjet.AddEntity(proj);
		var user1 = new Utilisateur("christopher", "Christopher@gmail.com", "fd0001");
		var user2 = new Utilisateur("Sevylane", "sevylane@gmail.com", "an0011");
		repoUtilisateur.AddEntity(user1);
		repoUtilisateur.AddEntity(user2);

		proj.AddUtilisateur(user1);
		repoProjet.UpdateEntity();

		//Ajout projets
		// var proj = new Projet("Trello", "Créer la BDD", new DateTime(2023, 01, 17));
		// repoProjet.AddEntity(proj);

		//Ajout listes

		// proj = repoProjet.Find(1);
		var list = new Liste("Liste 1", proj);
		repoListe.AddEntity(list);

		var list1 = new Liste("Liste 2", proj);
		repoListe.AddEntity(list1);

		var list2 = new Liste("Liste 3", proj);
		repoListe.AddEntity(list2);

		// //Ajout cartes
		// repoListe.AddEntity(list);
		// list = repoListe.Find(1);
		Console.WriteLine("Cartes");
		var card = new Carte("A faire", "Travailler sur la bdd", list, new DateTime(2023, 01, 17));
		repoCarte.AddEntity(card);

		var card1 = new Carte("A faire", "Mettre à jour la bdd", list1, new DateTime(2023, 01, 26));
		repoCarte.AddEntity(card1);

		var card2 = new Carte("A faire", "Tester la bdd", list, new DateTime(2023, 01, 26));
		repoCarte.AddEntity(card2);

		list.AddCarte(card);
		list.AddCarte(card2);
		list1.AddCarte(card1);

		// //Ajout commentaires
		// var card = new Carte();
		// card = repoCarte.Find(1);
		// var user = new Utilisateur();
		// user = repoUtilisateur.Find(2);
		Console.WriteLine("Commentaires");
		var commentaire = new Commentaire("Cela avant doucement mais sûrement", card, user1);
		var commentaire1 = new Commentaire("Update en cours", card, user2);
		repoCommentaire.AddEntity(commentaire);
		repoCommentaire.AddEntity(commentaire1);

		// //Ajout etiquette
		// var carte = new Carte();
		// carte = repoCarte.Find(1);
		Console.WriteLine("Etiquettes");
		var etiquette = new Etiquette("Important", "Jaune", card1);
		var etiquette1 = new Etiquette("Important", "Jaune", card1);
		repoEtiquette.AddEntity(etiquette);
		repoEtiquette.AddEntity(etiquette1);

	}
}
