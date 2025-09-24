using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChezTartine;

public class Ingredient
{
    /// <summary>
    /// Nom de l'ingrédient
    /// </summary>
    public string Nom { get; set; }

    /// <summary>
    /// Quantité (nécessaire ou en stock)
    /// </summary>
    public double Quantite { get; set; }

    /// <summary>
    /// Comment est mesuré l'ingrédient
    /// </summary>
    public UniteIngredient UniteIngredient { get; set; }

    /// <param name="nom">Nom de l'ingrédient</param>
    /// <param name="qte">Quantité à utiliser ou en stock</param>
    /// <param name="unite">Unité de mesure pour la quantité. "Aucune" est utilisée pour des ingrédirents tels que des pommes entières</param>
    public Ingredient(string nom = "", double qte = 1, UniteIngredient unite = UniteIngredient.aucune)
    {
        Nom = nom;
        Quantite = qte;
        UniteIngredient = unite;
    }
}