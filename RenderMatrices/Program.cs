using RenderMatrices;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

Mesh cube = new();
// Front face
cube.PushVertexTriangle(new VertexTriangle(
    new(new(-10, 10, -10), Color.Red),
    new(new(10, 10, -10), Color.Red),
    new(new(10, -10, -10), Color.Red)
));
cube.PushVertexTriangle(new VertexTriangle(
    new(new(-10, 10, -10), Color.Blue),
    new(new(10, -10, -10), Color.Blue),
    new(new(-10, -10, -10), Color.Blue)
));

// Back face
cube.PushVertexTriangle(new VertexTriangle(
    new(new(-10, 10, 10), Color.Cyan),
    new(new(10, -10, 10), Color.Cyan),
    new(new(10, 10, 10), Color.Cyan)
));
cube.PushVertexTriangle(new VertexTriangle(
    new(new(-10, 10, 10), Color.Magenta),
    new(new(-10, -10, 10), Color.Magenta),
    new(new(10, -10, 10), Color.Magenta)
));

// Right face
cube.PushVertexTriangle(new VertexTriangle(
    new(new(10, 10, -10), Color.Green),
    new(new(10, 10, 10), Color.Green),
    new(new(10, -10, 10), Color.Green)
));
cube.PushVertexTriangle(new VertexTriangle(
    new(new(10, 10, -10), Color.White),
    new(new(10, -10, 10), Color.White),
    new(new(10, -10, -10), Color.White)
));

// Left face
cube.PushVertexTriangle(new VertexTriangle(
    new(new(-10, 10, 10), Color.Yellow),
    new(new(-10, 10, -10), Color.Yellow),
    new(new(-10, -10, -10), Color.Yellow)
));
cube.PushVertexTriangle(new VertexTriangle(
    new(new(-10, 10, 10), Color.Red),
    new(new(-10, -10, -10), Color.Red),
    new(new(-10, -10, 10), Color.Red)
));

// Top face
cube.PushVertexTriangle(new VertexTriangle(
    new(new(-10, 10, 10), Color.Red),
    new(new(10, 10, 10), Color.Red),
    new(new(10, 10, -10), Color.Red)
));
cube.PushVertexTriangle(new VertexTriangle(
    new(new(-10, 10, 10), Color.Red),
    new(new(10, 10, -10), Color.Red),
    new(new(-10, 10, -10), Color.Red)
));

// Bottom face
cube.PushVertexTriangle(new VertexTriangle(
    new(new(-10, -10, -10), Color.Red),
    new(new(10, -10, -10), Color.Red),
    new(new(10, -10, 10), Color.Red)
));
cube.PushVertexTriangle(new VertexTriangle(
    new(new(-10, -10, -10), Color.Red),
    new(new(10, -10, 10), Color.Red),
    new(new(-10, -10, 10), Color.Red)
));
TriangleRasterizer rasterizer = new(800, 600);

Clock clock = new();

RenderWindow window = new(new VideoMode(800, 600), "CPU Rasterizer");

window.Closed += (s, e) => window.Close();

while (window.IsOpen)
{
    Matrix4 model = Matrix4.Transform(new(100, 100, 0)) * Matrix4.RotateX(clock.ElapsedTime.AsSeconds() / 4) * Matrix4.RotateY(clock.ElapsedTime.AsSeconds()) * Matrix4.Scale(new(5, 5, 5));

    window.DispatchEvents();
    window.Clear();

    rasterizer.Clear(Color.Black);
    rasterizer.Rasterize(cube, in model);

    window.Draw(rasterizer);

    window.Display();

    Thread.Sleep(1);
}
