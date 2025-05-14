using System;
using UnityEngine;

public static class HeightDeltaHandler {
    public static void IncreaseSpotPosition(Transform spot, float heightDelta) {
        Vector3 newAvaiableSpot = spot.position;
        newAvaiableSpot[1] += heightDelta;
        spot.position = newAvaiableSpot;
    }
    
    public static void DecreaseSpotPosition(Transform spot, float heightDelta) {
        Vector3 newAvaiableSpot = spot.position;
        newAvaiableSpot[1] -= heightDelta;
        spot.position = newAvaiableSpot;
    }
}