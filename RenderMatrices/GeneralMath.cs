namespace RenderMatrices;

public static class GeneralMath
{
    public static float Lerp(float min, float max, float t) => min + (max - min) * t;
}
