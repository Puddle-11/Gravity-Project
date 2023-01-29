# Gravity-Project


if you want to use the script without importing the entire package, apply the script to a gameobject with the following components

Sphere (mesh renderer)
Mesh renderer
sphere collider (not a trigger collider)
the gravity script
sphere collider(a trigger collier) radius = 30
rigidbody (mass = 1, drag = 0.005, angular drag = 0.05, use gravity = false, is kinematic = false)

instructions. apply the sphere trigger to the feild variable. and the rigid body to the RB variable; 
set mass to whatever you want > 0
set gravity volume = 1;
sim speed = 1;
volume = whatever you want > 0;
curve = linear curve increasing from the right to left;




