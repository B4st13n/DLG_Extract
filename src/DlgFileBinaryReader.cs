﻿using System.Collections.Generic;
using System.IO;
using DlgExtract.Model;
using System;
using System.Reflection.PortableExecutable;

namespace DlgExtract
{
    partial class Program
    {
        public class DlgFileBinaryReader
        {
            public DlgFile Read(string filename) // C:\Users\Bastienv\OneDrive\Projects\Baldur's Gate Project\Assets
            {
                using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                var f = Read(fs);
                f.CharacterName = Path.GetFileNameWithoutExtension(filename);
                return f;
            }

            public DlgFile Read(Stream s)
            {
                using var br = new BinaryReader(s);
                var dlgFile = ParseFile(br);
                br.BaseStream.Seek(0, SeekOrigin.Begin);
                return dlgFile;
            }

            private DlgFile ParseFile(BinaryReader br)
            {
                var header = (DlgHeaderBinary)Common.ReadStruct(br, typeof(DlgHeaderBinary));

                var stateBinaries = new List<DlgStateBinary>();

                br.BaseStream.Seek(header.StateOffset, SeekOrigin.Begin);
                for (int i = 0; i < header.StateCount; i++)
                {
                    var stateBinary = (DlgStateBinary)Common.ReadStruct(br, typeof(DlgStateBinary));
                    stateBinaries.Add(stateBinary);
                }

                //br.BaseStream.Seek(header.StateOffset, SeekOrigin.Begin);
                //for (int i = 0; i < header.StateCount; i++)
                //{
                //    //br.BaseStream.Seek(stringLocation, SeekOrigin.Begin);
                //    //var stringEntry = br.ReadBytes(stringDataEntries[i].StringLength);
                //    //var s = System.Text.Encoding.UTF8.GetString(stringEntry);
                //    //stringLocation += stringDataEntries[i].StringLength;
                //    //stringEntries.Add(new string(s));
                //}

                var dlg = new DlgFile
                {
                    
                };

                int stringIndex = 0;
                foreach (var state in stateBinaries)
                {
                    var stateEntry = new StateEntry
                    {
                        Strref = state.actor,
                    };
                    dlg.States.Add(stateEntry);
                    stringIndex++;
                }

                return dlg;
            }
        }
    }
}