using RenderMatrices;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

Mesh cube = new();
// Front face
cube.PushVertexTriangle(new VertexTriangle(
    new(new(-10, 10, -10), Color.White, new(0, 0)),
    new(new(10, 10, -10), Color.White, new(1, 0)),
    new(new(10, -10, -10), Color.White, new(1, 1))
));
cube.PushVertexTriangle(new VertexTriangle(
    new(new(-10, 10, -10), Color.White, new(0, 0)),
    new(new(10, -10, -10), Color.White, new(1, 1)),
    new(new(-10, -10, -10), Color.White, new(0, 1))
));
// Back face
cube.PushVertexTriangle(new VertexTriangle(
    new(new(-10, 10, 10), Color.White, new(1, 0)),
    new(new(10, -10, 10), Color.White, new(0, 1)),
    new(new(10, 10, 10), Color.White, new(0, 0))
));
cube.PushVertexTriangle(new VertexTriangle(
    new(new(-10, 10, 10), Color.White, new(1, 0)),
    new(new(-10, -10, 10), Color.White, new(1, 1)),
    new(new(10, -10, 10), Color.White, new(0, 1))
));
// Right face
cube.PushVertexTriangle(new VertexTriangle(
    new(new(10, 10, -10), Color.Yellow, new(0, 0)),
    new(new(10, 10, 10), Color.Red, new(1, 0)),
    new(new(10, -10, 10), Color.Magenta, new(1, 1))
));
cube.PushVertexTriangle(new VertexTriangle(
    new(new(10, 10, -10), Color.Yellow, new(0, 0)),
    new(new(10, -10, 10), Color.Red, new(1, 1)),
    new(new(10, -10, -10), Color.Magenta, new(0, 1))
));
// Left face
cube.PushVertexTriangle(new VertexTriangle(
    new(new(-10, 10, 10), Color.Yellow, new(0, 0)),
    new(new(-10, 10, -10), Color.Red, new(1, 0)),
    new(new(-10, -10, -10), Color.Magenta, new(1, 1))
));
cube.PushVertexTriangle(new VertexTriangle(
    new(new(-10, 10, 10), Color.Yellow, new(0, 0)),
    new(new(-10, -10, -10), Color.Red, new(1, 1)),
    new(new(-10, -10, 10), Color.Magenta, new(0, 1))
));
// Top face
cube.PushVertexTriangle(new VertexTriangle(
    new(new(-10, 10, 10), Color.Yellow, new(0, 0)),
    new(new(10, 10, 10), Color.Red, new(1, 0)),
    new(new(10, 10, -10), Color.Magenta, new(1, 1))
));
cube.PushVertexTriangle(new VertexTriangle(
    new(new(-10, 10, 10), Color.Yellow, new(0, 0)),
    new(new(10, 10, -10), Color.Red, new(1, 1)),
    new(new(-10, 10, -10), Color.Magenta, new(0, 1))
));
// Bottom face
cube.PushVertexTriangle(new VertexTriangle(
    new(new(-10, -10, -10), Color.Yellow, new(0, 0)),
    new(new(10, -10, -10), Color.Red, new(1, 0)),
    new(new(10, -10, 10), Color.Magenta, new(1, 1))
));
cube.PushVertexTriangle(new VertexTriangle(
    new(new(-10, -10, -10), Color.Yellow, new(0, 0)),
    new(new(10, -10, 10), Color.Red, new(1, 1)),
    new(new(-10, -10, 10), Color.Magenta, new(0, 1))
));

TriangleRasterizer rasterizer = new(800, 600);

Clock clock = new();

RenderWindow window = new(new VideoMode(800, 600), "CPU Rasterizer");

window.Closed += (s, e) => window.Close();
window.Resized += (s, e) => window.SetView(new View(new Vector2f(e.Width, e.Height) / 2.0f, new(e.Width, e.Height)));

Image texture = new("Resources/bricks.png");

while (window.IsOpen)
{
    Matrix4 model = Matrix4.Transform(new(100, 100, 0)) * Matrix4.RotateX(clock.ElapsedTime.AsSeconds() / 4) * Matrix4.RotateY(clock.ElapsedTime.AsSeconds()) * Matrix4.Scale(new(5, 5, 5));

    window.DispatchEvents();
    window.Clear();

    rasterizer.Clear(Color.Black);
    rasterizer.Rasterize(cube, in model, texture);

    window.Draw(rasterizer);

    window.Display();

    Thread.Sleep(1);
}
