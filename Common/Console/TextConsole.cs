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

    public static async Task WrapPrintTimeExecution(
        string actionName, Func<Task> action)
    {
        var sw = new System.Diagnostics.Stopwatch();
        sw.Start();
        await action();
        sw.Stop();
        WriteLine($"{actionName} took {sw.ElapsedMilliseconds}ms");
    }

    public static async Task WrapPrintTimeExecution(IEnumerable<(string name, Func<Task> action)> tasks)
    {
        async Task<string> EvaluateAction(string actionName, Func<Task> task)
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            await task();
            sw.Stop();
            return $"{actionName} took {sw.ElapsedMilliseconds}ms";
        }

        IList<string> results = [];
        foreach (var task in tasks)
        {
            WriteLine("Evaluating " + task.name);
            results.Add(await EvaluateAction(task.name, task.action));
            Thread.Sleep(500);
            AnsiConsole.Clear();
        }
        foreach (var result in results)
            WriteLine(result);
    }
}
