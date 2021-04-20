# SoftBody-Physics
2d physics simulation based on the particle spring mass model. Includes solid polygons and collision.

Generate rectangular soft bodies using particles and springs arranged in a cross section. Based on the mass and velocity of the nodes and the damping and desired length of the springs simulate the deforming of the body upon colliding with a polygon.
Polygons can be concave, convex or even contain holes. The example scene contains a rectangular frame, some ramps and a soft body that will bounce.

Demonstration: [YouTube Video](https://youtu.be/xg5vSq7_cT4)

## Todo

- fix bouncing of particles when pressed against surfaces
- implement self-collision so the structure doesn't collapse in on itself 
- implement friction

## Sources

- [Inspiration and Theory](https://youtu.be/kyQP4t_wOGI)
- [Line Intersection](https://martin-thoma.com/how-to-check-if-two-line-segments-intersect/)
- [closest point to line](https://stackoverflow.com/questions/3120357/get-closest-point-to-a-line/9557244#9557244)
