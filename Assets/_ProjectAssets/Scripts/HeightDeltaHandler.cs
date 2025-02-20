using System;
using UnityEngine;

public static class HeightDeltaHandler {
    public static void HandleSpotPosition(Transform spot, float heightDelta, bool isIncreasing) {
        Vector3 newAvaiableSpot = spot.position;
        newAvaiableSpot[1] += isIncreasing ? heightDelta : -heightDelta;
        spot.position = newAvaiableSpot;
    }
}