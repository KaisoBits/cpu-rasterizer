namespace RenderMatrices;

public readonly struct VertexTriangle
{
    public readonly Vertex A, B, C;

    public VertexTriangle(Vertex a, Vertex b, Vertex c)
    {
        A = a; B = b; C = c;
    }
}
