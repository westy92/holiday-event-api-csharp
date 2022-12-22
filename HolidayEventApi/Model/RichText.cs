using Newtonsoft.Json;

/// <summary>
/// Formatted Text
/// </summary>
public class RichText
{
    /// <summary>
    /// Formatted as plain text
    /// </summary>
    public string? Text { get; set; }
    /// <summary>
    /// Formatted as HTML
    /// </summary>
    public string? Html { get; set; }
    /// <summary>
    /// Formatted as Markdown
    /// </summary>
    public string? Markdown { get; set; }
}
