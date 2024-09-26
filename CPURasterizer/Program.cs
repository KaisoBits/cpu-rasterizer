using RenderMatrices;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

Mesh cube = new();
// Front face
cube.PushVertexTriangle(new VertexTriangle(
    new(new(-10, 10, -10), new Color(200, 200, 200), new(1, 1)),
    new(new(10, 10, -10), new Color(200, 200, 200), new(0, 0)),
    new(new(10, -10, -10), new Color(200, 200, 200), new(1, 0))
));
cube.PushVertexTriangle(new VertexTriangle(
    new(new(-10, 10, -10), new Color(200, 200, 200), new(0, 1)),
    new(new(10, -10, -10), new Color(200, 200, 200), new(0, 0)),
    new(new(-10, -10, -10), new Color(200, 200, 200), new(1, 1))
));
// Back face
cube.PushVertexTriangle(new VertexTriangle(
    new(new(-10, 10, 10), new Color(200, 200, 200), new(1, 0)),
    new(new(10, -10, 10), new Color(200, 200, 200), new(0, 0)),
    new(new(10, 10, 10), new Color(200, 200, 200), new(1, 1))
));
cube.PushVertexTriangle(new VertexTriangle(
    new(new(-10, 10, 10), new Color(200, 200, 200), new(1, 1)),
    new(new(-10, -10, 10), new Color(200, 200, 200), new(0, 0)),
    new(new(10, -10, 10), new Color(200, 200, 200), new(0, 1))
));
// Left face
cube.PushVertexTriangle(new VertexTriangle(
    new(new(10, 10, -10), new Color(170, 170, 170), new(0, 1)),
    new(new(10, 10, 10), new Color(170, 170, 170), new(1, 0)),
    new(new(10, -10, 10), new Color(170, 170, 170), new(0, 0))
));
cube.PushVertexTriangle(new VertexTriangle(
    new(new(10, 10, -10), new Color(170, 170, 170), new(1, 1)),
    new(new(10, -10, 10), new Color(170, 170, 170), new(1, 0)),
    new(new(10, -10, -10), new Color(170, 170, 170), new(0, 1))
));
// Right face
cube.PushVertexTriangle(new VertexTriangle(
    new(new(-10, 10, 10), new Color(170, 170, 170), new(0, 1)),
    new(new(-10, 10, -10), new Color(170, 170, 170), new(1, 0)),
    new(new(-10, -10, -10), new Color(170, 170, 170), new(0, 0))
));
cube.PushVertexTriangle(new VertexTriangle(
    new(new(-10, 10, 10), new Color(170, 170, 170), new(1, 1)),
    new(new(-10, -10, -10), new Color(170, 170, 170), new(1, 0)),
    new(new(-10, -10, 10), new Color(170, 170, 170), new(0, 1))
));
// Bottom face
cube.PushVertexTriangle(new VertexTriangle(
    new(new(-10, 10, 10), new Color(120, 120, 120), new(1, 0)),
    new(new(10, 10, 10), new Color(120, 120, 120), new(0, 1)),
    new(new(10, 10, -10), new Color(120, 120, 120), new(1, 1))
));
cube.PushVertexTriangle(new VertexTriangle(
    new(new(-10, 10, 10), new Color(120, 120, 120), new(0, 0)),
    new(new(10, 10, -10), new Color(120, 120, 120), new(0, 1)),
    new(new(-10, 10, -10), new Color(120, 120, 120), new(1, 0))
));
// Top face
cube.PushVertexTriangle(new VertexTriangle(
    new(new(-10, -10, -10), new Color(240, 240, 240), new(1, 0)),
    new(new(10, -10, -10), new Color(240, 240, 240), new(0, 1)),
    new(new(10, -10, 10), new Color(240, 240, 240), new(1, 1))
));
cube.PushVertexTriangle(new VertexTriangle(
    new(new(-10, -10, -10), new Color(240, 240, 240), new(0, 0)),
    new(new(10, -10, 10), new Color(240, 240, 240), new(0, 1)),
    new(new(-10, -10, 10), new Color(240, 240, 240), new(1, 0))
));

float sensitivity = 0.01f;
Vector2i? lastMousePos = null;
Vector2f angle = new();

TriangleRasterizer rasterizer = new(800, 600);

Clock clock = new();

RenderWindow window = new(new VideoMode(800, 600), "CPU Rasterizer");

window.Closed += (s, e) => window.Close();
window.MouseMoved += (s, e) =>
{
    if (lastMousePos != null)
    {
        float xDiff = e.X - lastMousePos.Value.X;
        float yDiff = e.Y - lastMousePos.Value.Y;

        angle += new Vector2f(xDiff, -yDiff) * sensitivity;
    }

    lastMousePos = new Vector2i(e.X, e.Y);
};

window.Resized += (s, e) =>
{
    window.SetView(new View(new Vector2f(e.Width, e.Height) / 2.0f, new(e.Width, e.Height)));
    rasterizer.Resize(e.Width, e.Height);
};
window.MouseLeft += (s, e) => lastMousePos = null;

Image texture = new("Resources/bricks.png");

List<Vector3> cubePositions = [];
for (int x = 0; x < 3; x++)
{
    for (int y = 0; y < 3; y++)
    {
        for (int z = 0; z < 3; z++)
        {
            cubePositions.Add(new Vector3(x, y, z) * 150 - new Vector3(150, 150, 150));
        }
    }
}

while (window.IsOpen)
{
    Matrix4 cameraMatrix = Matrix4.Transform(new(window.Size.X / 2, window.Size.Y / 2, 200)) * Matrix4.RotateX(angle.Y) * Matrix4.RotateY(angle.X);

    window.Clear();

    rasterizer.Clear(Color.Black);

    foreach (Vector3 cubePos in cubePositions)
    {
        Matrix4 model = Matrix4.Transform(cubePos) * Matrix4.Scale(new(5, 5, 5));
        rasterizer.Rasterize(cube, cameraMatrix * model, texture);
    }

    window.Draw(rasterizer);

    window.DispatchEvents();

    window.Display();

    Thread.Sleep(1);
}
