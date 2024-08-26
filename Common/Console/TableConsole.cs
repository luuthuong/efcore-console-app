namespace practice_app.Common;

public record TableData<T>(string[] Columns, IEnumerable<T> Items, Func<T, IEnumerable<string>> GetValues);

public static class TableConsole
{
    public static void Write<T>(TableData<T> data) where T : class
    {
        var table = RenderTable(data);
        AnsiConsole.Write(table);
    }

    public static void Write<T>(
        string title,
        TableData<T> data
        ) where T : class
    {
        var table = RenderTable(data);
        AnsiConsole.Write(
            new Panel(table)
            .Header(title)
            .RoundedBorder()
            .BorderColor(Color.Green)
        );
    }

    private static Table RenderTable<T>(TableData<T> data)
    {
        var table = new Table();
        table.MarkdownBorder();
        table.AddColumns(data.Columns.Select(x => new TableColumn(x)).ToArray());
        table.HeavyHeadBorder();

        foreach (var item in data.Items)
        {
            table.AddRow(data.GetValues(item).ToArray());
        }
        return table;
    }
}
