namespace RefactoringLab.Part02.Reports;

/// <summary>
/// Template Method: the export steps and their order live here, and only here.
/// Subclasses supply the one step that differs between report types: Format.
/// </summary>
public abstract class ReportExporter
{
    // The template method. Not virtual, so no subclass can change the order of the steps.
    public void Export(string path)
    {
        var rows = Load();
        if (!Validate(rows))
            throw new InvalidOperationException("Invalid data");
        var content = Format(rows);
        Save(path, content);
    }

    // Steps that are identical for every report (shared default implementations).
    protected virtual List<string[]> Load() =>
    [
        ["Id", "Name"],
        ["1", "Keyboard"],
        ["2", "Mouse"]
    ];

    protected virtual bool Validate(List<string[]> rows) =>
        rows.Count > 1 && rows[0].Length > 0;

    protected virtual void Save(string path, string content) =>
        File.WriteAllText(path, content);

    // The step that differs: every concrete exporter must provide it.
    protected abstract string Format(List<string[]> rows);
}
