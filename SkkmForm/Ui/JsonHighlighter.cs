using System;
using System.Text.Json;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace SkkmConnector.Ui
{
    /// <summary>
    /// Подсветка JSON
    /// </summary>
    public static class JsonHighlighter
    {
        private static readonly Brush KeyBrush = new SolidColorBrush(Color.FromRgb(0xFA, 0xD0, 0x00));      // жёлтый — имена свойств
        private static readonly Brush StringBrush = new SolidColorBrush(Color.FromRgb(0xA5, 0xE8, 0x44));   // зелёный — строки
        private static readonly Brush NumberBrush = new SolidColorBrush(Color.FromRgb(0x6C, 0xC7, 0xF6));   // голубой — числа
        private static readonly Brush KeywordBrush = new SolidColorBrush(Color.FromRgb(0xFF, 0x9D, 0x00));  // оранжевый — true/false/null
        private static readonly Brush PunctuationBrush = new SolidColorBrush(Color.FromRgb(0xE0, 0xE0, 0xE0)); // серый — скобки и запятые
        private static readonly Brush PlainBrush = new SolidColorBrush(Color.FromRgb(0xE0, 0xE0, 0xE0));

        static JsonHighlighter()
        {
            foreach (var brush in new[] { KeyBrush, StringBrush, NumberBrush, KeywordBrush, PunctuationBrush, PlainBrush })
                brush.Freeze();
        }

        /// <summary>
        /// Выводит текст в RichTextBox. Если текст — валидный JSON, форматирует с отступами и подсветкой,
        /// иначе выводит как есть.
        /// </summary>
        public static void Render(RichTextBox box, string? text)
        {
            var doc = new FlowDocument
            {
                PagePadding = new System.Windows.Thickness(4),
                FontFamily = new FontFamily("Consolas"),
                FontSize = 13
            };
            var paragraph = new Paragraph { Margin = new System.Windows.Thickness(0) };
            doc.Blocks.Add(paragraph);

            if (string.IsNullOrWhiteSpace(text))
            {
                box.Document = doc;
                return;
            }

            string pretty = text;
            bool isJson = false;
            try
            {
                using var parsed = JsonDocument.Parse(text);
                pretty = JsonSerializer.Serialize(parsed, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });
                isJson = true;
            }
            catch (JsonException)
            {
                // не JSON — выведем как обычный текст
            }

            if (!isJson)
            {
                paragraph.Inlines.Add(new Run(pretty) { Foreground = PlainBrush });
                box.Document = doc;
                return;
            }

            Tokenize(pretty, paragraph);
            box.Document = doc;
        }

        private static void Tokenize(string json, Paragraph paragraph)
        {
            int i = 0;
            while (i < json.Length)
            {
                char c = json[i];

                if (c == '"')
                {
                    int start = i;
                    i++; // открывающая кавычка
                    while (i < json.Length)
                    {
                        if (json[i] == '\\') { i += 2; continue; }
                        if (json[i] == '"') { i++; break; }
                        i++;
                    }
                    string token = json.Substring(start, i - start);

                    // ключ, если после пробелов идёт двоеточие
                    int j = i;
                    while (j < json.Length && (json[j] == ' ' || json[j] == '\t')) j++;
                    bool isKey = j < json.Length && json[j] == ':';

                    paragraph.Inlines.Add(new Run(token) { Foreground = isKey ? KeyBrush : StringBrush });
                }
                else if (char.IsDigit(c) || (c == '-' && i + 1 < json.Length && char.IsDigit(json[i + 1])))
                {
                    int start = i;
                    while (i < json.Length && (char.IsDigit(json[i]) || "+-.eE".IndexOf(json[i]) >= 0)) i++;
                    paragraph.Inlines.Add(new Run(json.Substring(start, i - start)) { Foreground = NumberBrush });
                }
                else if (c == 't' || c == 'f' || c == 'n')
                {
                    string?[] keywords = { "true", "false", "null" };
                    string? matched = null;
                    foreach (var kw in keywords)
                        if (kw != null && i + kw.Length <= json.Length && json.Substring(i, kw.Length) == kw)
                        {
                            matched = kw;
                            break;
                        }

                    if (matched != null)
                    {
                        paragraph.Inlines.Add(new Run(matched) { Foreground = KeywordBrush });
                        i += matched.Length;
                    }
                    else
                    {
                        paragraph.Inlines.Add(new Run(c.ToString()) { Foreground = PlainBrush });
                        i++;
                    }
                }
                else
                {
                    // пробелы, переводы строк и пунктуация — собираем подряд
                    int start = i;
                    while (i < json.Length)
                    {
                        char ch = json[i];
                        if (ch == '"' || char.IsDigit(ch) || ch == 't' || ch == 'f' || ch == 'n' ||
                            (ch == '-' && i + 1 < json.Length && char.IsDigit(json[i + 1])))
                            break;
                        i++;
                    }
                    paragraph.Inlines.Add(new Run(json.Substring(start, i - start)) { Foreground = PunctuationBrush });
                }
            }
        }
    }
}
