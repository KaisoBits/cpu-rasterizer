namespace RenderMatrices;

public readonly struct Vector2 : IEquatable<Vector2>
{
    public static Vector2 Zero { get; } = new Vector2();
    public static Vector2 Up { get; } = new Vector2(0, -1);
    public static Vector2 Down { get; } = new Vector2(0, 1);
    public static Vector2 Left { get; } = new Vector2(-1, 0);
    public static Vector2 Right { get; } = new Vector2(1, 0);

    public readonly float X, Y;

    public Vector2(float x, float y)
    {
        X = x;
        Y = y;
    }

    public static Vector2 operator +(Vector2 a, Vector2 b) => new(a.X + b.X, a.Y + b.Y);
    public static Vector2 operator -(Vector2 a, Vector2 b) => new(a.X - b.X, a.Y - b.Y);

    public static Vector2 operator *(Vector2 a, float scalar) => new(a.X * scalar, a.Y * scalar);

    public static Vector2 operator *(float scalar, Vector2 a) => new(a.X * scalar, a.Y * scalar);

    public static Vector2 operator /(Vector2 a, float scalar) => new(a.X / scalar, a.Y / scalar);

    public static bool operator ==(Vector2 a, Vector2 b) => a.X == b.X && a.Y == b.Y;

    public static bool operator !=(Vector2 a, Vector2 b) => !(a == b);

    public Vector2 Abs() => new(Math.Abs(X), Math.Abs(Y));

    public float SimpleLen() => Math.Abs(X) + Math.Abs(Y);

    public float Length() => MathF.Sqrt(SimpleLen());

    public Vector2 Normalized() => this / Length();

    public bool Equals(Vector2 other) => this == other;
    public override bool Equals(object? obj) => obj is Vector2 other && this == other;

    public override int GetHashCode() => HashCode.Combine(X, Y);

    public override string ToString() => $"({X}, {Y})";

    public static float Dot(Vector2 v1, Vector2 v2) => v1.X * v2.X + v1.Y * v2.Y;

    public static float Cross(Vector2 v1, Vector2 v2) =>
        v1.X * v2.Y - v1.Y * v2.X;

    public static explicit operator Vector3(Vector2 v) => new(v.X, v.Y, 0);
    public static explicit operator Vector4(Vector2 v) => new(v.X, v.Y, 0, 1);
}
