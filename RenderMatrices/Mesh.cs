namespace RenderMatrices;

public sealed class Mesh
{
    public IReadOnlyList<VertexTriangle> Faces => _faces;
    public readonly List<VertexTriangle> _faces = [];

    public void PushVertexTriangle(VertexTriangle vertexTriangle)
    {
        _faces.Add(vertexTriangle);
    }
}
