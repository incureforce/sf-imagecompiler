namespace SFC.ImageCompiler
{
    public interface ICLIOptionWithParameter : ICLIOption
    {
        bool TryParse(string text);
    }
}
