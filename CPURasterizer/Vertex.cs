using SFML.Graphics;

namespace RenderMatrices;

public readonly struct Vertex
{
    public readonly Vector3 Position;
    public readonly Color Color;
    public readonly Vector2 UV;

    public Vertex(Vector3 position, Color color, Vector2 uv)
    {
        Position = position;
        Color = color;
        UV = uv;
    }
}