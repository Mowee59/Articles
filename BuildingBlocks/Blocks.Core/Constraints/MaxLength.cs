namespace Blocks.Core.Constraints;

/// <summary>
/// Centralized length constraints used across the domain and persistence configuration.
/// Prefer these constants over hard-coded numeric values.
/// </summary>
public static class MaxLength
{
    public const int C0 = 0;
    public const int C8 = 8;
    public const int C32 = 32;
    public const int C64 = 64;
    public const int C128 = 128;
    public const int C256 = 256;
    public const int C512=512;
    public const int C1024=1024;
    public const int C2048 = 2048;
}

// TODO - replace hard coded values in Entities configurations with these constants 