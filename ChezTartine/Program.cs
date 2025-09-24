using ChezTartine;
using System;
using System.Text;

// Permet d'ajouter des recettes de base à la boutique
static void CreerRecettesInitiales()
{
    List<Ingredient> ingredientsTartePommes = new List<Ingredient>();

    ingredientsTartePommes.Add(new Ingredient("Cassonade", 125, UniteIngredient.mL));
    ingredientsTartePommes.Add(new Ingredient("Fécule de maïs", 45, UniteIngredient.mL));
    ingredientsTartePommes.Add(new Ingredient("Cannelle moulue", 2.5, UniteIngredient.mL));
    ingredientsTartePommes.Add(new Ingredient("Pomme", 6, UniteIngredient.aucune));
    ingredientsTartePommes.Add(new Ingredient("Beurre", 30, UniteIngredient.mL));
    ingredientsTartePommes.Add(new Ingredient("Abaisse de pâte", 2, UniteIngredient.aucune));

    RecetteTarte recettePommes = new RecetteTarte("Tarte aux pommes de Ricardo", TypeTarte.Pommes, ingredientsTartePommes, 6.50m);

    List<Ingredient> ingredientsTarteSucre = new List<Ingredient>();

    ingredientsTarteSucre.Add(new Ingredient("Cassonade", 375, UniteIngredient.mL));
    ingredientsTarteSucre.Add(new Ingredient("Farine", 15, UniteIngredient.mL));
    ingredientsTarteSucre.Add(new Ingredient("Lait", 250, UniteIngredient.mL));
    ingredientsTarteSucre.Add(new Ingredient("Oeuf", 1, UniteIngredient.aucune));


    RecetteTarte recetteSucre = new RecetteTarte("Recette secrète de tarte au sucre de Cécile", TypeTarte.Sucre, ingredientsTarteSucre, 6.00m);
}


// Affiche les recettes et retourne la recette choisie par l'utilisateur
static RecetteTarte GetRecetteChoisie()
{
    RecetteTarte.AfficherRecettes();
    int indexRecette = 0;
    do
    {
        Console.WriteLine("Entrer la recette choisie:");
        if (!int.TryParse(Console.ReadLine(), out indexRecette))
        {
            indexRecette = -1;
        }

    } while (indexRecette < 0 && indexRecette > RecetteTarte.Recettes.Count);
    return RecetteTarte.Recettes[indexRecette];
}

// Affiche les types de tartes et retourne le type choisi par l'utilisateur
static TypeTarte GetTypeTarteChoisie(Boutique boutique)
{
    boutique.AfficherTartes();
    int type = 0;
    do
    {
        Console.WriteLine("Entrer le type de tarte choisie:");
        if (!int.TryParse(Console.ReadLine(), out type))
        {
            type = -1;
        }

    } while (type < 0 && type > Enum.GetValues<TypeTarte>().Length);
    return (TypeTarte) type;
}

// Initialisation de la boutique
CreerRecettesInitiales();
System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("fr-CA");
DateOnly date = new DateOnly(2024, 09, 23);
Boutique boutique = new Boutique();

// Affichage du menu principal
Console.ForegroundColor = ConsoleColor.Green;
String? choix = "";
Console.WriteLine(@"       
                                   .----.
                                   |-.-.|
                 __________________|;-;-|__
               .'                  '----'  '.
              . """""""""""""""""""""""""""""""""""""""""""""""""""""""" .
             .   ."""""""""""".   .--""""""""""""""""""""-,   .
            . """""".       """"'  .--""""""""""--.. """""" .
           .""""""""""'-""""""""""-  .-'   |""|""|   .'"""""""""".
          .   .''.'.     .'      |""|""|    .      .
          '._( ()   \"""""".  _     _""""""  _   .____.'
            |.'.  ()'   ' --------------------.|
            ||  '--'""""""""""'          |         ||
            ||    '.------'     |""""|""""|""""|    ||
            ||     |.-.-.||-----|--|--|--|----||
            ||     || | |||     |__|_-""-_|    ||
            ||     ||_|_|||    .-""-"" ()  '.   ||
            || .--.| [-] ||   .' ()     () .  ||
            |.'    '.    || .'""""""""""""""""""""""""""'. ||
            |: ()   |    ||--\mga   .   .  /---|
            /    () \____||___\___________/____|
            '-------'");
do
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"{date:d} Bienvenue Chez Tartine ! Choisir une option:");
    Console.WriteLine("1. Voir l'inventaire des ingrédients");
    Console.WriteLine("2. Voir l'inventaire des tartes");
    Console.WriteLine("3. Voir les recettes disponibles");
    Console.WriteLine("4. Faire une tarte");
    Console.WriteLine("5. Enregistrer une vente");
    Console.WriteLine("6. Vérifier l'inventaire et fermer boutique pour la journée");
    Console.WriteLine("7. Quitter l'application");
    Console.ResetColor();
    try
    {
        choix = Console.ReadLine();
        switch (choix)
        {
            case "1":
                boutique.AfficherIngredients();
                break;
            case "2":
                boutique.AfficherTartes();
                break;
            case "3":
                RecetteTarte.AfficherRecettes();
                break;
            case "4":
                Console.WriteLine("4. Faire une tarte");
                RecetteTarte recette = GetRecetteChoisie();
                boutique.ProduireTarte(recette);
                Console.WriteLine($"Tarte ajoutée à l'inventaire.  Le solde de la boutique est {boutique.Solde:c}");
                break;
            case "5":
                Console.WriteLine("5. Enregistrer une vente");
                TypeTarte typeTarte = GetTypeTarteChoisie(boutique);
                boutique.VendreTarte(typeTarte);
                Console.WriteLine($"Tarte vendue. Le solde de la boutique est {boutique.Solde:c}");
                break;
            case "6":
                boutique.VerifierTartes(date.AddDays(1));
                break;
            case "7":
                break;
            default:
                Console.WriteLine("Entrer une option valide (1-7)");
                break;
        }
    }
    catch (Exception e)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(e.Message);
        Console.ResetColor();
    }

} while (choix != "7");
Console.WriteLine("Au revoir !");