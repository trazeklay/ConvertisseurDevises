namespace WSConvertisseur.Models
{
    public class Devise
    {
		private int id;
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
		public string? NomDevise
		{
			get { return nomDevise; }
			set { nomDevise = value; }
		}

		private double taux;

		public double Taux
		{
			get { return taux; }
			set { taux = value; }
		}

		public Devise()
		{

		}
	}
}
