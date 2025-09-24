using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChezTartine;

public class Boutique
{
    /// <summary>
    /// Les tartes produites par la boutique qui sont en vente
    /// </summary>
    public static List<Tarte> InventaireTartes { get; set; } = new List<Tarte>();

    /// <summary>
    /// Les différents ingrédients qui en stock dans la boutique
    /// </summary>
    public List<Ingredient> InventaireIngredients { get; set; }

    /// <summary>
    /// Solde du compte de la Boutique
    /// </summary>
    public decimal Solde { get; private set; }
    

    public Boutique()
    {
        InventaireTartes = new List<Tarte>();
        InventaireIngredients = new List<Ingredient>();
        Solde = 0;

        InventaireIngredients.Add(new Ingredient("Cassonade", 1250, UniteIngredient.mL));
        InventaireIngredients.Add(new Ingredient("Sucre blanc", 3750, UniteIngredient.mL));
        InventaireIngredients.Add(new Ingredient("Fécule de maïs", 900, UniteIngredient.mL));
        InventaireIngredients.Add(new Ingredient("Cannelle moulue", 25, UniteIngredient.mL));
        InventaireIngredients.Add(new Ingredient("Vanille", 100, UniteIngredient.mL));
        InventaireIngredients.Add(new Ingredient("Pomme", 60, UniteIngredient.aucune));
        InventaireIngredients.Add(new Ingredient("Cerises", 15000, UniteIngredient.mL));
        InventaireIngredients.Add(new Ingredient("Beurre", 300, UniteIngredient.mL));
        InventaireIngredients.Add(new Ingredient("Abaisse de pâte", 50, UniteIngredient.aucune));
    }

    /// <summary>
    /// Crée une nouvelle tarte selon la recette passée en paramètre si les ingrédients suffisants sont en stock et l'ajoute à l'inventaire de la boutique.
    /// Le solde de la boutique est réduit du coût de production.
    /// </summary>
    /// <param name="recette">La recette qui sera utilisée pour préparer la tarte. Le stock d'ingrédients de la boutique sera modifié selon la recette passée en paramètre.</param>
    /// <param name="dateProduction">Date à laquelle est produite (cuite) la tarte</param>
    public void ProduireTarte(RecetteTarte recette, DateOnly dateProduction = new DateOnly())
    {
        if (VerifierStockIngredients(recette))
        {
            Ingredient? ingredient;
            try
            {
                foreach (Ingredient ingredientRecette in recette.Ingredients)
                {

                    ingredient = InventaireIngredients.Find(i => i.Nom.Equals(ingredientRecette.Nom, StringComparison.OrdinalIgnoreCase));
                    if (ingredient != null)
                    {
                        ingredient.Quantite -= ingredientRecette.Quantite;
                    }
                }
                // TODO Compléter (voir documentation)
            }
            catch (Exception)
            {
                throw;
            }
        }
        else
        {
            throw new StocksInsuffisantsException("Il n'est pas possible de produire ce type de tarte pour le moment. Vérifier l'inventaire d'ingrédients.");

        }
    }

    /// <summary>
    /// Enregistre une vente de tarte de la boutique : retire la tarte de l'inventaire et augmente le solde de la boutique.
    /// </summary>
    /// <param name="typeTarte">Type de tarte cherché dans l'inventaire pour la vente (la recette n'est pas considérée).</param>
    public void VendreTarte(TypeTarte typeTarte)
    {
        Tarte? tarte = InventaireTartes.FirstOrDefault(t => t.Recette.TypeTarte == typeTarte);
        if (!(tarte is Tarte))
        {
            Solde = tarte.Prix;
            InventaireTartes.Remove(tarte);
        }
        else
        {
            throw new StocksInsuffisantsException("Il n'est pas possible de vendre ce type de tarte pour le moment. Vérifier l'inventaire de tartes.");
        }
    }

    /// <summary>
    /// Pour une recette passée en paramètre, retourne si la boutique a en stock les ingrédients nécessaires à sa fabrication
    /// </summary>
    /// <param name="recette">Recette pour laquelle on vérifie le stock d'ingrédient</param>
    /// <returns>Vrai si la boutique a en stock tous les ingrédients, Faux sinon</returns>
    public bool VerifierStockIngredients(RecetteTarte recette)
    {
        bool ingredientsSuffisants = true;
        foreach (Ingredient ingredientRecette in recette.Ingredients)
        {
            ingredientsSuffisants = InventaireIngredients.Exists(i => i.Nom.ToLowerInvariant() == ingredientRecette.Nom.ToLowerInvariant() && i.Quantite >= ingredientRecette.Quantite);
            if (!ingredientsSuffisants)
            {
                break;
            }
        }
        return ingredientsSuffisants;
    }


    /// <summary>
    /// Parcoure l'inventaire des tartes et jette toutes les tartes pour lesquelles la date "Meilleure avant" est passée.
    /// </summary>
    /// <param name="dateJour">Date à laquelle on vérifie l'inventaire</param>
    public void VerifierTartes(DateOnly dateJour)
    {
        int retirees = InventaireTartes.RemoveAll(t => t.DevraitEtreJetee(dateJour) == true);
        Console.WriteLine($"{retirees} tartes jetée(s).");
    }

    /// <summary>
    /// Affiche la liste d'ingrédients en stock
    /// </summary>
    public void AfficherIngredients()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Ingrédients en stock :\n");
        foreach (Ingredient ingredient in InventaireIngredients)
        {
            sb.Append($"- {ingredient.Nom} ({ingredient.Quantite}{(ingredient.UniteIngredient == UniteIngredient.aucune ? "" : " "+ingredient.UniteIngredient)})");
            sb.Append("\n");
        }
        Console.WriteLine(sb.ToString());
    }

    /// <summary>
    /// Affiche la liste de tartes en stock
    /// </summary>
    public void AfficherTartes()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Types de tartes :\n");
        foreach (TypeTarte type in Enum.GetValues<TypeTarte>())
        {
            sb.Append($"{((int)type)} - {type} ({GetNombreTartes(type)} en stock)\n");
        }
        Console.WriteLine(sb.ToString());
    }

    /// <summary>
    /// Affiche le nombre de tartes en stock du type passé en paramètre
    /// </summary>
    /// <param name="type">Type de tarte cherché</param>
    /// <returns>Le nombre de tartes en stock dont le type est passé en paramètre</returns>
    public int GetNombreTartes(TypeTarte type)
    {
        return InventaireTartes.Where(t => t.Recette.TypeTarte == type).Count();
    }
}