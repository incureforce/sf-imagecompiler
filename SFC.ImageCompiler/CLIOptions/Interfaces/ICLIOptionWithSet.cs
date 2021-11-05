namespace SFC.ImageCompiler
{
    public interface ICLIOptionWithSet : ICLIOption
    {
        bool TrySet();
    }
}
