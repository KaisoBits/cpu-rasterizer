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

    public void Rasterize(Mesh mesh, in Matrix4 matrix, Image objectTexture)
    {
        foreach (VertexTriangle face in mesh.Faces)
            Rasterize(face, matrix, objectTexture);
    }

    public void Rasterize(VertexTriangle face, in Matrix4 matrix, Image objectTexture)
    {
        Vector4 a = matrix * new Vector4(face.A.Position, 1);
        Vector4 b = matrix * new Vector4(face.B.Position, 1);
        Vector4 c = matrix * new Vector4(face.C.Position, 1);

        Color aColor = face.A.Color;
        Color bColor = face.B.Color;
        Color cColor = face.C.Color;

        Vector2 aUV = face.A.UV;
        Vector2 bUV = face.B.UV;
        Vector2 cUV = face.C.UV;

        Vector2 ab = (Vector2)(b - a);
        Vector2 bc = (Vector2)(c - b);
        Vector2 ca = (Vector2)(a - c);

        float triangleArea = Vector2.Cross(ca, ab);

        float biasAB = IsTopLeft(ab) ? 0 : -0.0001f;
        float biasBC = IsTopLeft(bc) ? 0 : -0.0001f;
        float biasCA = IsTopLeft(ca) ? 0 : -0.0001f;

        float minX = MathF.Min(MathF.Min(a.X, b.X), c.X);
        float minY = MathF.Min(MathF.Min(a.Y, b.Y), c.Y);
        float maxX = MathF.Max(MathF.Max(a.X, b.X), c.X);
        float maxY = MathF.Max(MathF.Max(a.Y, b.Y), c.Y);

        for (uint y = (uint)MathF.Round(Math.Max(minY, 0)); y <= Math.Ceiling(maxY) && y < _image.Size.Y; y++)
        {
            bool inTriangle = false;
            for (uint x = (uint)MathF.Round(Math.Max(minX, 0)); x <= Math.Ceiling(maxX) && y < _image.Size.X; x++)
            {
                Vector2 position = new(x, y);

                float w0 = Vector2.Cross(ab, position - (Vector2)a) + biasAB;
                float w1 = Vector2.Cross(bc, position - (Vector2)b) + biasBC;
                float w2 = Vector2.Cross(ca, position - (Vector2)c) + biasCA;

                float w0Bias = w0 / triangleArea;
                float w1Bias = w1 / triangleArea;
                float w2Bias = w2 / triangleArea;

                if (w0 >= 0 && w1 >= 0 && w2 >= 0)
                {
                    Vector2 finalUV = aUV * w0Bias + bUV * w1Bias + cUV * w2Bias;

                    Color textureColor = objectTexture.GetPixel((uint)(finalUV.X * objectTexture.Size.X), (uint)(finalUV.Y * objectTexture.Size.Y));

                    _image.SetPixel(x, y, new Color(
                        (byte)(((aColor.R * w0Bias + bColor.R * w1Bias + cColor.R * w2Bias) / 255.0f) * textureColor.R),
                        (byte)(((aColor.G * w0Bias + bColor.G * w1Bias + cColor.G * w2Bias) / 255.0f) * textureColor.G),
                        (byte)(((aColor.B * w0Bias + bColor.B * w1Bias + cColor.B * w2Bias) / 255.0f) * textureColor.B)));
                    inTriangle = true;
                }
                else if (inTriangle)
                {
                    break;
                }
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

    private bool IsTopLeft(Vector2 vec)
    {
        bool isTop = vec.Y == 0 && vec.X > 0;
        bool isLeft = vec.Y < 0;

        return isTop || isLeft;
    }
}
