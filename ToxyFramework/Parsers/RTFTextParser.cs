﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Toxy;

namespace ToxyFramework.Parsers
{
    public class RTFTextParser : ITextParser
    {
        public RTFTextParser(ParserContext context)
        {
            this.Context = context;
        }
        public virtual ParserContext Context
        {
            get; set;
        }
        public string Parse()
        {
            if (!File.Exists(Context.Path))
                throw new FileNotFoundException("File " + Context.Path + " is not found");

            StreamReader sr = null;
            try
            {
                ReasonableRTF.RtfToTextConverter converter = new ReasonableRTF.RtfToTextConverter();
                ReasonableRTF.Models.RtfResult result = converter.Convert(File.ReadAllBytes(Context.Path));
                return result.Text;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }
    }
}