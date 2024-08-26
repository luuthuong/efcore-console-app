namespace practice_app.Common.Console;

public class TextConsole
{
    public static void WriteLine(string message, Justify alignment = Justify.Left)
    {
        var text = new Text(
            message,
            new Style(Color.Green)
        );
        text.Justify(alignment);
        AnsiConsole.Write(text);
        AnsiConsole.WriteLine();
    }
}
