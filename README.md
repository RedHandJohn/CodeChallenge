# CodeChallenge

This Unity project serves as a solution to the challenge described in the Code Challenge Unity.pdf file.

The first sentence in the Code Challenge pdf confused me a little bit. It reads "Using Unity and C# create a new 2D project and draw a hexagon of any color".
Now did this mean I was supposed to draw a sprite of a hexagon and import it in Unity or was I supposed to generate a hexagon inside Unity?
I wasn't sure so I did both. Each implementation is in a different scene and the "Switch Scene" button can be used to switch between them.

<h2>Builds</h2>

The builds are found in the Builds directory. I do not own a Mac so the only builds I put out are:
- One Windows Desktop build
- One Android build

<h2>General stuff</h2>

The ShapeController is responsible for rendering the shape, animating it, registering taps on it and it also plays a couple of beautiful sound effects.
All the UI is placed under the Canvas object.
The Manager is responsible for handling UI input.

<h2>Sprite implementation</h2>

I considered this the default implementation so the Unity project is structured around it.
Open the scene in Assets/Scenes/MainScene and enter Play mode to run it. It's also the default scene opened when you run either of the builds.

I am perfectly aware that I could have done this entire thing on the UI but I haven't worked with sprites in world space in a while and I missed it.

Each shape has its own sprite and its own polygon collider for precise input. 
The shapes are packed in prefabs and are toggled on/off based on the user's interaction with the left hand side buttons.

<h2>Generated Mesh implementation</h2>

Open the scene in Assets/GeneratedShapes/Scenes and enter Play mode to run it. It can also be accessed by clicking the "Switch Scene" button.

I made this one rather hastily to be perfectly honest.
As such I ended up duplicating the original scene, the Manager script and the ShapeController script and changed them where necessary.
Is this a good practice? Definitely not, but I was in a hurry. I did my best to isolate this implementation in it's own separate directory Assets/GeneratedShapes.
I also placed all the code belonging to it in a separate namespace to prevent conflict and/or ambiguity.

The shapes are stored as arrays of vertices inside instances of a custom scriptable object.
I keep references to scriptable object instances in the GeneratedShapes.ShapeController and I generate meshes at runtime based on them.
A mesh collider is then matched to the newly generated mesh and we can register raycasts to the shape with great precision.

I left two scriptable object instances named CustomShape_1 and CustomShape_2 if you want to play around with it and make your own shapes.
CustomShape_1 currently holds the vertices for a cross shape.
CustomShape_2 is empty.
After you set your custom vertices in the scriptable object all you have to do is hit Play and click the button corresponding to the shape you edited.
