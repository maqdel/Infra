using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Threading.Tasks;

namespace maqdel.Infra.Misc
{
    public interface IFizzBuzzTool
    {
        List<DivisorToken> ClassicTokens();

        List<string> Classic();

        List<string> CustomByRange(int Start, int End, List<DivisorToken> DivisorTokens);

        List<string> CustomSet(List<int> Numbers, List<DivisorToken> DivisorTokens);
    }
}
