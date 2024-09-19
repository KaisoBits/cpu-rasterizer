using SFML.Graphics;

namespace RenderMatrices;

public sealed class TriangleRasterizer : Drawable, IDisposable
{
    private readonly Image _image;
    private readonly Texture _texture;
    private readonly Sprite _sprite;

    private readonly float[,] _zBuffer;

    public TriangleRasterizer(uint width, uint height)
    {
        _image = new(width, height);
        _texture = new Texture(_image);
        _sprite = new Sprite(_texture);

        _zBuffer = new float[width, height];
    }

    public void Rasterize(Mesh mesh, in Matrix4 matrix)
    {
        foreach (VertexTriangle face in mesh.Faces)
            Rasterize(face, matrix);
    }

    public void Rasterize(VertexTriangle face, in Matrix4 matrix)
    {
        Vector4 a = matrix * new Vector4(face.A.Position, 1);
        Vector4 b = matrix * new Vector4(face.B.Position, 1);
        Vector4 c = matrix * new Vector4(face.C.Position, 1);

        Vector2 ab = (Vector2)(b - a);
        Vector2 bc = (Vector2)(c - b);
        Vector2 ca = (Vector2)(a - c);

        float minX = MathF.Min(MathF.Min(a.X, b.X), c.X);
        float minY = MathF.Min(MathF.Min(a.Y, b.Y), c.Y);
        float maxX = MathF.Max(MathF.Max(a.X, b.X), c.X);
        float maxY = MathF.Max(MathF.Max(a.Y, b.Y), c.Y);

        for (uint y = (uint)MathF.Round(Math.Max(minY, 0)); y <= maxY && y < _image.Size.Y; y++)
        {
            for (uint x = (uint)MathF.Round(Math.Max(minX, 0)); x <= maxX && y < _image.Size.X; x++)
            {
                Vector2 position = new(x, y);

                if (Vector2.Cross(ab, position - (Vector2)a) < 0 &&
                    Vector2.Cross(bc, position - (Vector2)b) < 0 &&
                    Vector2.Cross(ca, position - (Vector2)c) < 0)
                    _image.SetPixel(x, y, face.A.Color);
            }
        }
    }

    public void Clear(Color color)
    {
        for (uint y = 0; y < _image.Size.Y; y++)
        {
            for (uint x = 0; x < _image.Size.X; x++)
            {
                _image.SetPixel(x, y, color);
                _zBuffer[x, y] = 0;
            }
        }
    }

    public void Dispose()
    {
        _image.Dispose();
        _texture.Dispose();
        _sprite.Dispose();
    }

    public void Draw(RenderTarget target, RenderStates states)
    {
        // Slow but that's the price of CPU-GPU data transfer...
        _texture.Update(_image);

        target.Draw(_sprite, states);
    }
}
