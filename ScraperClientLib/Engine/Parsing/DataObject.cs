﻿namespace ScraperClientLib.Engine.Parsing
{
    public abstract class DataObject
    {
        public string ParserType { get; internal set; }

        public abstract override string ToString();
    }
}