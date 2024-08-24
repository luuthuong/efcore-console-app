using System;
using Spectre.Console;

namespace practice_app.Common;

public static class TableConsole
{
    public static void Print<T>(string[] columns, IEnumerable<T> items, Func<T, IEnumerable<string>> getValues) where T : class
    {
        var table = new Table();
        table.MarkdownBorder();
        table.AddColumns(columns.Select(x => new TableColumn(x)).ToArray());

        foreach (var item in items)
        {
            table.AddRow(getValues(item).ToArray());
        }
        AnsiConsole.Write(table);
    }
}
