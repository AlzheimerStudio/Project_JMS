
public static class Extensions
{
    public static float MapRangeClamped(this float value, float inA, float outA, float inB, float outB)
    {
        return (value - inA) / (outA - inA) * (outB - inB) + inB;
    }

}
