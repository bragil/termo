
public static class Words
{
    static List<string> List = new()
    {
        "pasta", "dados", "teste", "limbo", "fruta", "jeito", "haste", "pinta", "frete", "droga", "besta",
        "drone", "marca", "brado", "regua", "lapis", "manco", "barco", "trava", "verde", "culpa", "bicho",
        "pente", "prata", "leste", "carga", "mundo", "anzol", "pesca", "galho", "radio", "longe", "perto"
    };

    /// <summary>
    /// Sorteia uma palavra da lista.
    /// </summary>
    /// <returns>Palavra secreta</returns>
    public static string GetRandom()
        => List.OrderBy(x => Guid.NewGuid()).Take(1).Single();
}