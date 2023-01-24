using System.ComponentModel.DataAnnotations;

namespace WSConvertisseur.Models
{
	/// <summary>
	/// Classe de l'objet devise
	/// </summary>
    public class Devise
    {
		private int id;

		/// <summary>
		/// Retourne l'identifiant de la devise
		/// </summary>
		public int ID
		{
			get { return id; }
			set {
				if (value <= 0)
					throw new ArgumentOutOfRangeException("L'id de la devise doit être strictement positif.");
				id = value; 
			}
		}

		private string? nomDevise;

		/// <summary>
		/// Retourne le nom de la devise
		/// </summary>
        [Required]
        public string? NomDevise
		{
			get { return nomDevise; }
			set { nomDevise = value; }
		}

		private double taux;

		/// <summary>
		/// Retourne le taux de la devise
		/// </summary>
		public double Taux
		{
			get { return taux; }
			set { taux = value; }
		}


		/// <summary>
		/// Constructeur de la classe Devise
		/// </summary>
		/// <param name="id">L'id de la devise</param>
		/// <param name="nomDevise">le nom de la devise</param>
		/// <param name="taux">le taux de la devise, en comparaison avec l'euro</param>
		public Devise(int id, string? nomDevise, double taux)
		{
			ID = id;
			NomDevise = nomDevise;
			Taux = taux;
		}
	}
}
