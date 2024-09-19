namespace RenderMatrices;

public readonly struct Vector4 : IEquatable<Vector4>
{
    public static Vector4 Zero { get; } = new Vector4();
    public static Vector4 Up { get; } = new Vector4(0, -1, 0, 0);
    public static Vector4 Down { get; } = new Vector4(0, 1, 0, 0);
    public static Vector4 Left { get; } = new Vector4(-1, 0, 0, 0);
    public static Vector4 Right { get; } = new Vector4(1, 0, 0, 0);

    public readonly float X, Y, Z, W;

    public Vector4(float x, float y, float z, float w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public Vector4(Vector3 vec, float w)
    {
        X = vec.X;
        Y = vec.Y;
        Z = vec.Z;
        W = w;
    }

    public static Vector4 operator +(Vector4 a, Vector4 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
    public static Vector4 operator -(Vector4 a, Vector4 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);

    public static Vector4 operator *(Vector4 a, float scalar) => new(a.X * scalar, a.Y * scalar, a.Z * scalar, a.W * scalar);

    public static Vector4 operator *(float scalar, Vector4 a) => new(a.X * scalar, a.Y * scalar, a.Z * scalar, a.W * scalar);

    public static Vector4 operator /(Vector4 a, float scalar) => new(a.X / scalar, a.Y / scalar, a.Z / scalar, a.W / scalar);

    public static bool operator ==(Vector4 a, Vector4 b) => a.X == b.X && a.Y == b.Y && a.Z == b.Z && a.W == b.W;

    public static bool operator !=(Vector4 a, Vector4 b) => !(a == b);

    public Vector4 Abs() => new(Math.Abs(X), Math.Abs(Y), Math.Abs(Z), Math.Abs(W));

    public float SimpleLen() => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z) + Math.Abs(W);

    public float Length() => MathF.Sqrt(SimpleLen());

    public Vector4 Normalized() => this / Length();

    public bool Equals(Vector4 other) => this == other;
    public override bool Equals(object? obj) => obj is Vector4 other && this == other;

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);

    public override string ToString() => $"({X}, {Y}, {Z}, {W})";

    public static explicit operator Vector3(Vector4 v) => new(v.X, v.Y, v.Z);
    public static explicit operator Vector2(Vector4 v) => new(v.X, v.Y);
}
