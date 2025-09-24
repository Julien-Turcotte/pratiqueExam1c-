using ChezTartine;

namespace ChezTartineTests;
public class BoutiqueTests
{
    private Boutique boutique;
    private DateOnly date;
    private RecetteTarte recettePommes;
    private RecetteTarte recetteMuscadePommes;

    private void CreerRecettes()
    {
        // La muscade n'est pas dans les ingrédients de base des boutiques
        Ingredient muscade = new Ingredient("Muscade", 12, UniteIngredient.mL);
        Ingredient pommes = new Ingredient("Pomme", 6, UniteIngredient.aucune);
        recettePommes = new RecetteTarte("Pommes", TypeTarte.Autre, new List<Ingredient>() { pommes }, 6.50m);
        recetteMuscadePommes = new RecetteTarte("Pommes et muscade", TypeTarte.Autre, new List<Ingredient>() { muscade, pommes }, 7.50m);
    }

    public BoutiqueTests()
    {
        // Créer les recettes
        CreerRecettes();
        // Créer une boutique avec un stock d'ingrédients (pomme en fait partie, mais pas muscade)
        date = new DateOnly(2024, 09, 23);
        boutique = new Boutique();
    }

    [Fact]
    public void ProduireTarte_consomme_ingredients()
    {
        Ingredient pommes = boutique.InventaireIngredients.Find(i => i.Nom == "Pomme");
        double qtePommesAvant = pommes.Quantite;
        boutique.ProduireTarte(recettePommes);
        // La recette contient 6 pommes
        Assert.Equal(qtePommesAvant - 6, pommes.Quantite);
    }

    [Fact]
    public void ProduireTarte_deduit_du_solde_cout_de_production()
    {
        decimal soldeAvant = boutique.Solde;
        boutique.ProduireTarte(recettePommes);
        Assert.Equal(soldeAvant - 6.50m, boutique.Solde);
    }

    [Fact]
    public void ProduireTarte_retourne_exception_si_stock_insuffisant()
    {
        //La muscade ne fait pas partie du stock de départ de la boutique
        Action action = () => boutique.ProduireTarte(recetteMuscadePommes);
        StocksInsuffisantsException exception = Assert.Throws<StocksInsuffisantsException>(action);
        Assert.Equal("Il n'est pas possible de produire ce type de tarte pour le moment. Vérifier l'inventaire d'ingrédients.", exception.Message);
    }

    [Fact]
    public void VendreTarte_augmente_le_solde_du_prix_de_la_tarte()
    {
        boutique.ProduireTarte(recettePommes);
        decimal soldeAvant = boutique.Solde;
        boutique.VendreTarte(TypeTarte.Autre);
        Assert.Equal(soldeAvant + recettePommes.PrixDetail, boutique.Solde);
    }

    [Fact]
    public void VendreTarte_retourne_exception_si_stock_insuffisant()
    {
        Action action = () => boutique.VendreTarte(TypeTarte.Farlouche);
        StocksInsuffisantsException exception = Assert.Throws<StocksInsuffisantsException>(action);
        Assert.Equal("Il n'est pas possible de vendre ce type de tarte pour le moment. Vérifier l'inventaire de tartes.", exception.Message);
    }


}