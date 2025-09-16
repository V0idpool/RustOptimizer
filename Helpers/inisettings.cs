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

        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileIntA", CharSet = CharSet.Ansi)]
        private static extern int GetPrivateProfileInt(string lpApplicationName, string lpKeyName, int nDefault, string lpFileName);

        private string strFilename;

        public string Path;
        private void LogMessage(string message, string logFilePath)
        {
            try
            {
                // Append the message to the log file
                File.AppendAllText(logFilePath, $"{DateTime.Now}: {message}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogError(ex);
                // Handle any exceptions that may occur while writing to the log file
                System.Diagnostics.Debug.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }
        public string ReadValue(string Section, string Key, string DefaultValue = "", int BufferSize = 1024)
        {
            if (string.IsNullOrEmpty(Path))
            {
                MessageBox.Show("No path given" + Environment.NewLine + "Could not read Value", "No path given", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Error";
            }

            if (!File.Exists(Path))
            {
                MessageBox.Show("File does not exist" + Environment.NewLine + "Could not read Value", "File does not exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Error";
            }

            var sb = new System.Text.StringBuilder(BufferSize);
            int length = GetPrivateProfileString(Section, Key, DefaultValue, sb, sb.Capacity, Path);
            return sb.ToString(0, length);
        }

        public bool GetBoolean(string Section, string Key, bool Default)
        {
            return GetPrivateProfileInt(Section, Key, Conversions.ToInteger(Default), strFilename) == 1;
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