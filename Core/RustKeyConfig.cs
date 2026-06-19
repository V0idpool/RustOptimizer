namespace RustOptimizer.Core
{
    public static class RustKeyConfig
    {
        public static void ApplyBindings(string filePath, IReadOnlyDictionary<string, string> bindings)
        {
            string? directory = Path.GetDirectoryName(filePath);
            if (string.IsNullOrEmpty(directory) || !Directory.Exists(directory))
            {
                throw new DirectoryNotFoundException($"Rust config directory was not found: {directory}");
            }

            List<string> lines = File.Exists(filePath)
                ? File.ReadAllLines(filePath).ToList()
                : new List<string>();

            Dictionary<string, int> bindingLines = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < lines.Count; i++)
            {
                if (TryGetBindingKey(lines[i], out string key))
                {
                    bindingLines[key] = i;
                }
            }

            foreach (var binding in bindings)
            {
                string line = $"bind {binding.Key.ToLowerInvariant()} {binding.Value}";
                if (bindingLines.TryGetValue(binding.Key, out int lineIndex))
                {
                    lines[lineIndex] = line;
                }
                else
                {
                    bindingLines[binding.Key] = lines.Count;
                    lines.Add(line);
                }
            }

            if (File.Exists(filePath))
            {
                File.Copy(filePath, filePath + ".bak", true);
            }

            File.WriteAllLines(filePath, lines);
        }

        private static bool TryGetBindingKey(string line, out string key)
        {
            key = string.Empty;
            string[] parts = line.Trim().Split(new char[] { ' ' }, 3, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 3 || !parts[0].Equals("bind", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            key = parts[1];
            return true;
        }
    }
}
