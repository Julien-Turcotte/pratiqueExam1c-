using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChezTartine;

public class Tarte
{
    /// <summary>
    /// Recette utilisée pour créer la tarte
    /// </summary>
    public RecetteTarte Recette { get; }

    /// <summary>
    /// Date après laquelle il vaudrait mieux jeter la tarte
    /// </summary>
    public DateOnly MeilleureAvant { get; }

    /// <summary>
    /// Prix de la tarte; par défaut celui suggéré par la recette
    /// </summary>
    public decimal Prix { get; set; }

    /// <param name="recette">Recette à partir de laquelle sera produite la tarte</param>
    /// <param name="dateProduction">Date à laquelle est produite (cuite) la tarte</param>
    public Tarte(RecetteTarte recette, DateOnly dateProduction = new DateOnly())
    {
        Recette = recette;
        MeilleureAvant = dateProduction.AddDays(2);
        Prix = recette.PrixDetail;
    }

    /// <summary>
    /// Compare la date passée en paramètre à la date "Meilleure avant". Si la date passée en paramètre est supérieure à la date "Meilleure Avant" la date devrait être jetée pour éviter toute contamination alimentaire.
    /// </summary>
    /// <param name="DateJour">Date à comparer avec "Meilleure avant"</param>
    /// <returns>Vrai si la date devrait être jetée (selon la date "Meilleure Avant"), Faux sinon</returns>
    public bool DevraitEtreJetee(DateOnly DateJour)
    {
        return DateJour <= MeilleureAvant;
    }
}
