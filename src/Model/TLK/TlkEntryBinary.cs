﻿using System;
using System.Runtime.InteropServices;

namespace DlgExtract.Model
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct TlkEntryBinary
    {
        public short Flags;
        public Array8 Sound;
        public Int32 VolumeVariance;
        public Int32 PitchVariance;
        public Int32 StringIndex;
        public Int32 StringLength;
    }
}