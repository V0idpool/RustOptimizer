using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
namespace RustOptimizer.Helpers
{
    public class inisettings
    {



        [DllImport("kernel32", EntryPoint = "GetPrivateProfileStringA", CharSet = CharSet.Ansi)]
        private static extern int GetPrivateProfileString(string lpApplicationName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        [DllImport("kernel32", EntryPoint = "WritePrivateProfileStringA", CharSet = CharSet.Ansi)]
        private static extern int WritePrivateProfileString(string lpApplicationName, string lpKeyName, string lpString, string lpFileName);

        [DllImport("kernel32", EntryPoint = "WritePrivateProfileStringA", CharSet = CharSet.Ansi)]
        private static extern int DeletePrivateProfileSection(string Section, int NoKey, int NoSetting, string FileName);




        public string Path;
        private void LogMessage(string message, string logFilePath)
        {
            try
            {
                File.AppendAllText(logFilePath, $"{DateTime.Now}: {message}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogError(ex);
                System.Diagnostics.Debug.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }
        public string ReadValue(string Section, string Key, string DefaultValue = "", int BufferSize = 1024)
        {
            if (string.IsNullOrEmpty(Path))
            {
                return DefaultValue;
            }
            // If the file doesn't exist on first launch, silently return the DefaultValue.
            if (!File.Exists(Path))
            {
                return DefaultValue;
            }

            var sb = new System.Text.StringBuilder(BufferSize);
            int length = GetPrivateProfileString(Section, Key, DefaultValue, sb, sb.Capacity, Path);
            return sb.ToString(0, length);
        }
        public bool GetBoolean(string Section, string Key, bool Default)
        {
            string strValue = ReadValue(Section, Key, Default.ToString());

            if (strValue.Equals("True", StringComparison.OrdinalIgnoreCase) ||
                strValue.Equals("Enabled", StringComparison.OrdinalIgnoreCase) ||
                strValue == "1")
            {
                return true;
            }

            if (strValue.Equals("False", StringComparison.OrdinalIgnoreCase) ||
                strValue.Equals("Disabled", StringComparison.OrdinalIgnoreCase) ||
                strValue == "0")
            {
                return false;
            }

            return Default;
        }
        public int GetInteger(string Section, string Key, int Default)
        {
            string strValue = ReadValue(Section, Key, Default.ToString());

            if (int.TryParse(strValue, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out int result))
            {
                return result > 0 ? result : Default;
            }

            return Default;
        }
        public string GetPath()
        {
            return Path;
        }

        public void WriteValue(string Section, string Key, string Value, string path)
        {

            if (string.IsNullOrEmpty(Path))
            {
                //do nothing
                return;
            }

            string Ordner;
            Ordner = System.IO.Path.GetDirectoryName(path);
            if (Directory.Exists(Ordner) == false)
            {
                //do nothing
                return;
            }

            WritePrivateProfileString(Section, Key, Value, Path);
        }

        public void DeleteKey(string Section, string Key)
        {

            if (string.IsNullOrEmpty(Path))
            {
                Interaction.MsgBox("No path given" + Constants.vbNewLine + "Could not delete Key", MsgBoxStyle.Critical, "No path given");
                return;
            }

            string Ordner;
            Ordner = System.IO.Path.GetDirectoryName(Path);
            if (Directory.Exists(Ordner) == false)
            {
                Interaction.MsgBox("File does not exist" + Constants.vbNewLine + "Could not delete Key", MsgBoxStyle.Critical, "File does not exist");
                return;
            }

            string arglpString = null;
            WritePrivateProfileString(Section, Key, arglpString, Path);
        }

        public void DeleteSection(string Section)
        {

            if (string.IsNullOrEmpty(Path))
            {
                Interaction.MsgBox("No path given" + Constants.vbNewLine + "Could not delete Section", MsgBoxStyle.Critical, "No path given");
                return;
            }

            if (File.Exists(Path) == false)
            {
                Interaction.MsgBox("File does not exist (anymore)" + Constants.vbNewLine + "Could not delete Section", MsgBoxStyle.Critical, "File does not exist");
                return;
            }

            DeletePrivateProfileSection(Section, 0, 0, Path);
        }
        //destructor
        ~inisettings()
        {

        }




    }
}