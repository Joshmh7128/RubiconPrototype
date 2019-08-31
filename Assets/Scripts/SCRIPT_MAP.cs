using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRIPT_MAP : MonoBehaviour
{
    /* 
     * 
     CAMERA CONTROL - attached to player camera
        mouselook functionality
        lerps camera to player
        adjustable offset

    CUBE ACTION - attached to pivot manager
        controls arena rubik's cube movement
        randomly selects a face and rotates that face (9 rooms) plus or minus 90 degrees
        adjustable rotation speed

    CUBE ROOM PLAYER TRANSPORT - attached to each room
        parents player to room upon entering room, keeping the player oriented during room rotations

    CURSOR CONTROL - attached to player camera
        just turns off cursor for now... really just setup for of we want to switch the cursor back on to click UI buttons

    DESTROY AFTER TIME - attached to spark effects
        destroys impact particles after 1 second to avoid clutter

    GUN SCRIPT - attached to player camera
        does the shootin
        raycast based hit detection
        also spawns particle effects and muzzle flashes - not sure if that's best done through here, but that's the way it is rn
        does ammo management and reloading too
        could I have done separate scripts for ammo and particles? yeah. but here we are

    HEALTH TRACKER - attached to player
        tracks health and manages damage animations

    PLAYER MOVE - attached to player object
        enables 3-axis movement for the player
        adjustable speed
     
     */
}
