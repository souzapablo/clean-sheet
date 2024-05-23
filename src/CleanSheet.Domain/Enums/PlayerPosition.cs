namespace CleanSheet.Domain.Enums
{
    [Flags]
    public enum PlayerPosition
    {
        None = 0,
        Gk = 1,
        Lb = 2,
        Cb = 4,
        Rb = 8,
        Cdm = 16,
        Lwb = 32,
        Lm = 64,
        Rwb = 128,
        Cm = 256,
        Rm = 512,
        Cam = 1024,
        Lw = 2048,
        Cf = 4096,
        Rw = 8192,
        St = 16384
    }
}