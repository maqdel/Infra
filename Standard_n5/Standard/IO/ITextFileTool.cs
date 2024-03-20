using System;
using System.Collections.Generic;
using System.IO;

using System.Net;
using System.Threading.Tasks;

namespace maqdel.Infra.IO
{
    public interface ITextFileTool
    {
        void CleanFile();
        void AddText(string Text);
        void AddText(string Text, int Length, string Filler, int Align);
        void AddTextLine(string TextLine);
        bool LoadFile();
        string GetFileText();
        int GetFileLength();
        int GetLinesCount();
        string[] GetLines();
        int Find(string Text);
        List<int> GetPositions(string source, string searchString);
        List<int> FindAll(string Text);
    }
}
