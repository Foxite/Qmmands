using System;
using System.Threading.Tasks;

namespace Qmmands
{
    internal interface ITypeParser
    {
        ValueTask<ITypeParserResult> ParseAsync(Parameter parameter, string value, CommandContext context);
    }
}
