# SoftBody-Physics
2d physics simulation based on the particle spring mass model. Includes solid polygons and collision.

Generate rectangular soft bodies using particles and springs arranged in a cross section. Based on the mass and velocity of the nodes and the damping and desired length of the springs simulate the deforming of the body upon colliding with a polygon.
Polygons can be concave, convex or even contain holes. The example scene contains a rectangular frame, some ramps and a soft body that will bounce.

Demonstration: [YouTube Video](https://youtu.be/xg5vSq7_cT4)

## Todo

- fix bouncing of particles when pressed against surfaces
- implement self-collision so the structure doesn't collapse in on itself 
- implement friction
