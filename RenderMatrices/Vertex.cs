using SFML.Graphics;

namespace RenderMatrices;

public readonly struct Vertex
{
    public readonly Vector3 Position;
    public readonly Color Color;

    public Vertex(Vector3 position, Color color)
    {
        Position = position;
        Color = color;
    }
}