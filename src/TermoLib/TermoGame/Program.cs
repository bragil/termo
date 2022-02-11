using TermoLib;

string typed = "";
Console.WriteLine("--------------------------------------------------------------");
Console.WriteLine("TERMO - 6 chances de acertar a palavra secreta de 5 caracteres");
Console.WriteLine("--------------------------------------------------------------");

var engine = new GameEngine(Words.GetRandom());

var result = engine.Loop(i =>
{
    do
        typed = Console.ReadLine() ?? "";
    while (engine.InvalidWord(typed));

    var feedback = engine.Verify(typed);
    ShowFeedback(feedback, i + 1);

    if (feedback.IsWinner())
        return GameResult.YouWin();

    return GameResult.Continue();
});
if (!string.IsNullOrWhiteSpace(result.Message))
    Console.WriteLine(result.Message);

/// <summary>
/// Exibe o feedback da palavra digitada
/// </summary>
static void ShowFeedback(Feedback feedback, int chance)
{
    Console.Write($"{chance}. ");
    feedback.Chars.ForEach(cf =>
    {
        if (cf.CharStatus == CharStatus.NotOk)
            FormatChar(cf, ConsoleColor.White);
        else if (cf.CharStatus == CharStatus.OnlyCharOk)
            FormatChar(cf, ConsoleColor.DarkYellow);
        else
            FormatChar(cf, ConsoleColor.Green);
    });
    Console.WriteLine();
}

/// <summary>
/// Formata cada caractere da palavra, de acordo com a cor passada (status):
/// verde: caractere e posição ok
/// amarelo: caractere ok e posição não ok
/// branco: não ok
/// </summary>
static void FormatChar(CharFeedback cf, ConsoleColor color)
{
    var defaultForecolor = Console.ForegroundColor;
    Console.ForegroundColor = color;
    Console.Write(cf.Char.ToString().ToUpper());
    Console.ForegroundColor = defaultForecolor;
    Console.Write(" ");
}