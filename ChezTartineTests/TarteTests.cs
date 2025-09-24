using ChezTartine;

namespace ChezTartineTests;

public class TarteTests
{
    private DateOnly dateProduction;
    private RecetteTarte recette;

    public TarteTests()
    {
        dateProduction = new DateOnly(2024, 09, 23);
        recette = new RecetteTarte("Pommes", TypeTarte.Autre, new List<Ingredient>() { new Ingredient("Pomme", 6, UniteIngredient.aucune) }, 6.50m);
    }

    [Fact]
    public void Date_meilleure_avant_est_2_jours_apres_date_production()
    {   
        Tarte tarte = new Tarte(recette, dateProduction);
        Assert.Equal(dateProduction.AddDays(2), tarte.MeilleureAvant);
    }

    [Fact]
    public void DevraitEtreJetee_retourne_vrai_si_dateVerification_superieure_a_meilleure_avant()
    {
        DateOnly dateVerification = dateProduction.AddDays(3);
        Tarte tarte = new Tarte(recette, dateProduction);

        Assert.True(dateVerification > tarte.MeilleureAvant);
        Assert.True(tarte.DevraitEtreJetee(dateVerification));
    }

    [Fact]
    public void DevraitEtreJetee_retourne_faux_si_dateVerification_egale_a_meilleure_avant()
    {
        DateOnly dateVerification = dateProduction.AddDays(3);
        Tarte tarte = new Tarte(recette, dateProduction);

        Assert.False(tarte.DevraitEtreJetee(dateVerification));
    }
}
