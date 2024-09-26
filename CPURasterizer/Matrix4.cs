
namespace RenderMatrices;

public readonly struct Matrix4
{
    public static readonly Matrix4 Identity = new(1, 0, 0, 0,
                                                  0, 1, 0, 0,
                                                  0, 0, 1, 0,
                                                  0, 0, 0, 1);

    public Matrix4(
        float a11, float a12, float a13, float a14,
        float a21, float a22, float a23, float a24,
        float a31, float a32, float a33, float a34,
        float a41, float a42, float a43, float a44)
    {
        A11 = a11; A12 = a12; A13 = a13; A14 = a14;
        A21 = a21; A22 = a22; A23 = a23; A24 = a24;
        A31 = a31; A32 = a32; A33 = a33; A34 = a34;
        A41 = a41; A42 = a42; A43 = a43; A44 = a44;
    }

    public readonly float
        A11, A12, A13, A14,
        A21, A22, A23, A24,
        A31, A32, A33, A34,
        A41, A42, A43, A44;

    public static Matrix4 Transform(Vector3 position)
    {
        return new Matrix4(1, 0, 0, position.X,
                           0, 1, 0, position.Y,
                           0, 0, 1, position.Z,
                           0, 0, 0, 1);
    }

    public static Matrix4 Scale(Vector3 scale)
    {
        return new Matrix4(scale.X, 0,       0,       0,
                           0,       scale.Y, 0,       0,
                           0,       0,       scale.Z, 0,
                           0,       0,       0,       1);
    }

    public static Matrix4 RotateX(float angleInRadians)
    {
        float cos = MathF.Cos(angleInRadians);
        float sin = MathF.Sin(angleInRadians);

        return new Matrix4(1, 0, 0, 0,
                           0, cos, -sin, 0,
                           0, sin, cos, 0,
                           0, 0, 0, 1);
    }

    public static Matrix4 RotateY(float angleInRadians)
    {
        float cos = MathF.Cos(angleInRadians);
        float sin = MathF.Sin(angleInRadians);

        return new Matrix4(cos, 0, sin, 0,
                           0, 1, 0, 0,
                           -sin, 0, cos, 0,
                           0, 0, 0, 1);
    }

    public static Matrix4 RotateZ(float angleInRadians)
    {
        float cos = MathF.Cos(angleInRadians);
        float sin = MathF.Sin(angleInRadians);

        return new Matrix4(cos, -sin, 0, 0,
                           sin, cos, 0, 0,
                           0, 0, 1, 0,
                           0, 0, 0, 1);
    }

    public static Matrix4 operator *(in Matrix4 m1, in Matrix4 m2)
    {
        return new(
            a11: m1.A11 * m2.A11 + m1.A12 * m2.A21 + m1.A13 * m2.A31 + m1.A14 * m2.A41,
            a12: m1.A11 * m2.A12 + m1.A12 * m2.A22 + m1.A13 * m2.A32 + m1.A14 * m2.A42,
            a13: m1.A11 * m2.A13 + m1.A12 * m2.A23 + m1.A13 * m2.A33 + m1.A14 * m2.A43,
            a14: m1.A11 * m2.A14 + m1.A12 * m2.A24 + m1.A13 * m2.A34 + m1.A14 * m2.A44,
            a21: m1.A21 * m2.A11 + m1.A22 * m2.A21 + m1.A23 * m2.A31 + m1.A24 * m2.A41,
            a22: m1.A21 * m2.A12 + m1.A22 * m2.A22 + m1.A23 * m2.A32 + m1.A24 * m2.A42,
            a23: m1.A21 * m2.A13 + m1.A22 * m2.A23 + m1.A23 * m2.A33 + m1.A24 * m2.A43,
            a24: m1.A21 * m2.A14 + m1.A22 * m2.A24 + m1.A23 * m2.A34 + m1.A24 * m2.A44,
            a31: m1.A31 * m2.A11 + m1.A32 * m2.A21 + m1.A33 * m2.A31 + m1.A34 * m2.A41,
            a32: m1.A31 * m2.A12 + m1.A32 * m2.A22 + m1.A33 * m2.A32 + m1.A34 * m2.A42,
            a33: m1.A31 * m2.A13 + m1.A32 * m2.A23 + m1.A33 * m2.A33 + m1.A34 * m2.A43,
            a34: m1.A31 * m2.A14 + m1.A32 * m2.A24 + m1.A33 * m2.A34 + m1.A34 * m2.A44,
            a41: m1.A41 * m2.A11 + m1.A42 * m2.A21 + m1.A43 * m2.A31 + m1.A44 * m2.A41,
            a42: m1.A41 * m2.A12 + m1.A42 * m2.A22 + m1.A43 * m2.A32 + m1.A44 * m2.A42,
            a43: m1.A41 * m2.A13 + m1.A42 * m2.A23 + m1.A43 * m2.A33 + m1.A44 * m2.A43,
            a44: m1.A41 * m2.A14 + m1.A42 * m2.A24 + m1.A43 * m2.A34 + m1.A44 * m2.A44
        );
    }

    public static Vector4 operator *(in Matrix4 m, in Vector4 v)
    {
        return new(m.A11 * v.X + m.A12 * v.Y + m.A13 * v.Z + m.A14 * v.W,
                   m.A21 * v.X + m.A22 * v.Y + m.A23 * v.Z + m.A24 * v.W,
                   m.A31 * v.X + m.A32 * v.Y + m.A33 * v.Z + m.A34 * v.W,
                   m.A41 * v.X + m.A42 * v.Y + m.A43 * v.Z + m.A44 * v.W);
    }

    public override string ToString()
    {
        return $"""
            {A11:000000.00} | {A12:000000.00} | {A13:000000.00} | {A14:000000.00}
            ----------------------------------------------
            {A21:000000.00} | {A22:000000.00} | {A23:000000.00} | {A24:000000.00}
            ----------------------------------------------
            {A31:000000.00} | {A32:000000.00} | {A33:000000.00} | {A34:000000.00}
            ----------------------------------------------
            {A41:000000.00} | {A42:000000.00} | {A43:000000.00} | {A44:000000.00}
            """;
    }
}
