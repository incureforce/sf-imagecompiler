namespace SFC.ImageCompiler
{
    public enum CLIOptionKeyGeneration
    {
        Detect,

        Unix = 1,
        Windows = 2,

        Everything = Unix | Windows,
    }
}
