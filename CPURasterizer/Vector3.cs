namespace RenderMatrices;

public readonly struct Vector3 : IEquatable<Vector3>
{
    public static Vector3 Zero { get; } = new Vector3();
    public static Vector3 Up { get; } = new Vector3(0, -1, 0);
    public static Vector3 Down { get; } = new Vector3(0, 1, 0);
    public static Vector3 Left { get; } = new Vector3(-1, 0, 0);
    public static Vector3 Right { get; } = new Vector3(1, 0, 0);

    public readonly float X, Y, Z;

    public Vector3(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static Vector3 operator +(Vector3 a, Vector3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    public static Vector3 operator -(Vector3 a, Vector3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    public static Vector3 operator *(Vector3 a, float scalar) => new(a.X * scalar, a.Y * scalar, a.Z * scalar);

    public static Vector3 operator *(float scalar, Vector3 a) => new(a.X * scalar, a.Y * scalar, a.Z * scalar);

    public static Vector3 operator /(Vector3 a, float scalar) => new(a.X / scalar, a.Y / scalar, a.Z / scalar);

    public static bool operator ==(Vector3 a, Vector3 b) => a.X == b.X && a.Y == b.Y && a.Z == b.Z;

    public static bool operator !=(Vector3 a, Vector3 b) => !(a == b);

    public Vector3 Abs() => new(Math.Abs(X), Math.Abs(Y), Math.Abs(Z));

    public float SimpleLen() => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);

    public float Length() => MathF.Sqrt(SimpleLen());

    public Vector3 Normalized() => this / Length();

    public bool Equals(Vector3 other) => this == other;
    public override bool Equals(object? obj) => obj is Vector3 other && this == other;

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);

    public override string ToString() => $"({X}, {Y}, {Z})";

    public static float Dot(Vector3 v1, Vector3 v2) => v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;

    public static Vector3 Cross(Vector3 v1, Vector3 v2) => new(
        v1.Y * v2.Z - v1.Z * v2.Y,
        v1.Z * v2.X - v1.X * v2.Z,
        v1.X * v2.Y - v1.Y * v2.X);

    public static explicit operator Vector4(Vector3 v) => new(v.X, v.Y, v.Z, 1);
    public static explicit operator Vector2(Vector3 v) => new(v.X, v.Y);
}
