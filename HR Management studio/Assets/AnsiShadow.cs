using Figgle;
using System.IO;

namespace HR_Management_studio.Assets;

public static class AnsiShadow
{
    private static readonly string _path = "../../../Assets/AnsiSHadowFont.flf";

    public static FiggleFont LoadAnsiShadow()
    {
        using (Stream stream = File.OpenRead(_path))
        {
            return FiggleFontParser.Parse(stream);
        }
    }
}
