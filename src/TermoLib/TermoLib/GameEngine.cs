namespace TermoLib;

/// <summary>
/// Engine do game Termo
/// </summary>
public class GameEngine
{
    private string term = "";
    private int maxLength = 0;

    public int MaxNumberOfChances { get; init; }

    public GameEngine(string term, int maxLength = 5, int maxNumberOfChances = 6)
    {
        this.term = term;
        this.maxLength = maxLength;
        MaxNumberOfChances = maxNumberOfChances;
    }
    
    /// <summary>
    /// Loop do jogo.
    /// </summary>
    /// <param name="function">Funcionalidade do loop</param>
    public GameResult Loop(Func<int, GameResult> function)
    {
        for (int i = 0; i < MaxNumberOfChances; i++)
        {
            var result = function(i);
            if (result.EndGame)
                return result;
        }
        return GameResult.YouLoose();
    }

    /// <summary>
    /// Verifica a palavra digitada pelo jogador 
    /// e retorna o feedback com os acertos e erros.
    /// </summary>
    /// <param name="typed">Palavra digitada pelo jogador</param>
    /// <returns>Feedback</returns>
    public Feedback Verify(string typed)
        => typed.ToCharArray()
                .Select(GetCharFeedback)  // map
                .Aggregate(new Feedback(), GenerateFeedback); // reduce

    
    /// <summary>
    /// Retorna true se a palavra digitada for inválida 
    /// (nulo, string vazia, comprimento diferente de maxLength ou caracteres não alfabéticos).
    /// </summary>
    /// <param name="typed">Palavra digitada</param>
    /// <returns>true ou false</returns>
    public bool InvalidWord(string typed)
        => string.IsNullOrWhiteSpace(typed)
            || typed.Length != maxLength
            || typed.ToCharArray().Any(c => !char.IsLetter(c));

    /// <summary>
    /// Retorna o feedback para um caractere da palavra digitada.
    /// </summary>
    /// <param name="character">Caractere da palavra digitada</param>
    /// <param name="index">Índice do caractere</param>
    /// <returns>CharFeedback</returns>
    private CharFeedback GetCharFeedback(char character, int index)
        => (character == term[index])
            ? new CharFeedback(CharStatus.AllOk, character)
            : term.IndexOf(character) switch
            {
                int idx when idx == -1 => new CharFeedback(CharStatus.NotOk, character),
                int idx when idx == index => new CharFeedback(CharStatus.AllOk, character),
                int idx when idx != index => new CharFeedback(CharStatus.OnlyCharOk, character),
                _ => new CharFeedback(CharStatus.NotOk, character)
            };

    /// <summary>
    /// Monta o objeto de feedback da palavra digitada.
    /// </summary>
    /// <param name="feedback">Instância do objeto Feedback</param>
    /// <param name="charFeedback">Feedback de um caractere da palavra</param>
    /// <returns>Objeto Feedback</returns>
    private Feedback GenerateFeedback(Feedback feedback, CharFeedback charFeedback)
    {
        feedback.AddChar(charFeedback);
        return feedback;
    }
}