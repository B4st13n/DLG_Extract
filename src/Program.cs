using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DlgExtract.Model;

namespace DlgExtract
{
    partial class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                ShowUsage();
                return;
            }

            //if (!File.Exists(args[0]))
            //{
            //    Console.WriteLine($"The tlk file at {args[0]} does not exist");
            //    return;
            //}

            //if (!File.Exists(args[1]))
            //{
            //    Console.WriteLine($"The dlg file at {args[0]} does not exist");
            //    return;
            //}

            #region Read TLK files
            string[] tlkPaths = Directory.GetFiles(args[0], "*.TLK", SearchOption.AllDirectories);
            var tlkReader = new TlkFileBinaryReader();
            var tlkFiles = new List<TlkFile>();
            foreach (var path in tlkPaths)
            {
                var tlkFile = tlkReader.Read(path);
                tlkFiles.Add(tlkFile);
                Console.WriteLine($"Added {tlkFile.Strings.Count} entries");
            }
            #endregion

            #region Read DLG files
            string[] dlgPaths = Directory.GetFiles(args[0], "*.DLG", SearchOption.AllDirectories);
            var dlgReader = new DlgFileBinaryReader();
            var dlgFiles = new List<DlgFile>();
            foreach (var path in dlgPaths)
            {
                var dlgFile = dlgReader.Read(path);
                dlgFiles.Add(dlgFile);
                Console.WriteLine($"Added {dlgFile.CharacterName} {dlgFile.States.Count} entries");
            }
            #endregion

            #region Combine both files in dialogs
            var dialogs = new List<Dialog>();
            foreach (var tlkFile in tlkFiles)
            {
                foreach (var dlgFile in dlgFiles)
                {
                    foreach (var state in dlgFile.States)
                    {
                        var dialog = new Dialog()
                        {
                            Name = dlgFile.CharacterName,
                            Strref = state.Strref,
                            Text = tlkFile.Strings.Where(y => y.Strref == state.Strref).SingleOrDefault()?.Text,
                        };
                        dialogs.Add(dialog);
                    }
                }
            }

            foreach (var dialog in dialogs)
            {
                try
                {
                    // I must save the text file of the dialogue here
                    var min = Math.Min(dialog.Text.Length, 20);
                    var fileName = dialog.Text.Substring(0, min).Trim();
                    fileName = Regex.Replace(fileName, @"[^0-9a-zA-Z .,!&]+", "");
                    fileName = $@"{dialog.Name}_{dialog.Strref}_{fileName}.txt";
                    fileName = $@"{args[0]}\TXT\{dialog.Name}\{fileName}";
                    Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                    File.WriteAllText(fileName, dialog.Text);
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"Error on {dialog.Name} {exception.Message}");
                }
            }
            #endregion


        }

        static void ShowUsage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("  TlkToSql dialog.tlk character.dlg");
        }
    }
}