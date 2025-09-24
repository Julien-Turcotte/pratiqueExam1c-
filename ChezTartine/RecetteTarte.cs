using System.Text;

namespace ChezTartine;

public class RecetteTarte
{
    /// <summary>
    /// Liste de toutes les recettes qui sont dans l'application
    /// </summary>
    public static List<RecetteTarte> Recettes { get; set; } = new List<RecetteTarte>();
    /// <summary>
    /// Liste des ingrédients nécessaires pour réaliser la recette
    /// </summary>
    public List<Ingredient> Ingredients { get; set; }
    /// <summary>
    /// Le type de tarte associé à cette recette
    /// </summary>
    public TypeTarte TypeTarte { get; set; }
    /// <summary>
    /// Nom de la recette
    /// </summary>
    public string Nom { get; set; }

    /// <summary>
    /// Coût de production pour cette recette de tarte
    /// </summary>
    public decimal CoutProduction { get; }

    /// <summary>
    /// Prix de vente suggéré
    /// </summary>
    public decimal PrixDetail { get; }

    /// <summary>
    /// Permet d'afficher toutes les recettes de l'application
    /// </summary>
    public static void AfficherRecettes()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Recettes disponibles :\n");
        for (int i=0; i< Recettes.Count; i++)
        {
            RecetteTarte recette = Recettes[i];
            sb.Append($"{i} - {recette.Nom}; Coût production {recette.CoutProduction:c}; Prix suggéré {recette.PrixDetail:c};\n");
        }
        Console.WriteLine(sb.ToString());
    }

    /// <remarks>Quand la recette est créée elle est ajoutée à la liste de recettes de l'application</remarks>
    /// <param name="nom">Nom de la recette</param>
    /// <param name="type">Type de tarte</param>
    /// <param name="ingredients">Liste d'ingrédients (comprend quantité de chacun)</param>
    /// <param name="coutProduction">Le coût de production pour faire la tarte (sera déduit du solde de la boutique lors de la fabrication)</param>
    public RecetteTarte(string nom, TypeTarte type, List<Ingredient> ingredients, decimal coutProduction = 0)
    {
        Nom = nom;
        TypeTarte = type;
        Ingredients = ingredients;
        Recettes.Add(this);
        CoutProduction = coutProduction;
        PrixDetail = coutProduction + (0.2m * coutProduction);
    }
}

