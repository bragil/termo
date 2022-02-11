namespace TermoLib;

/// <summary>
/// Status do caractere digitado pelo usuário.
/// AllOk: caractere e posição estão corretos.
/// OnlyCharOk: somente o caractere digitado está correto, a posição não.
/// NotOk: não existe o caractere digitado.
/// </summary>
public enum CharStatus { AllOk = 1, OnlyCharOk = 2, NotOk = 3 }

/// <summary>
/// Feedback do caractere digitado.
/// </summary>
/// <param name="CharStatus">Status do caractere</param>
/// <param name="Char">Caractere</param>
public record CharFeedback(CharStatus CharStatus, char Char);

/// <summary>
/// Feedback da palavra digitada
/// </summary>
public record Feedback
{
    public List<CharFeedback> Chars { get; set; }

    public Feedback()
        => Chars = new List<CharFeedback>();

    public void AddChar(CharFeedback charFeedback)
        => Chars.Add(charFeedback);

    public bool IsWinner()
        => Chars.All(c => c.CharStatus == CharStatus.AllOk);
}

/// <summary>
/// Resultado do game.
/// </summary>
public record GameResult
{
    public bool EndGame { get; set; }
    public bool HasError { get; set; }
    public string Message { get; set; } = "";

    public static GameResult Continue() => new GameResult() { EndGame = false };
    public static GameResult YouWin() => new GameResult() { EndGame = true, Message = "PARABÉNS!!!", HasError = false };
    public static GameResult YouLoose() => new GameResult() { EndGame = true, Message = "Não foi desta vez...", HasError = false };
}