using System.Collections.Generic;
using UnityEngine;

public class DroneCommunication : MonoBehaviour
{
    private LinkedList<Drone> drones = new LinkedList<Drone>();

    public void AddDrone(Drone drone)
    {
        drones.AddLast(drone);
    }

    public Drone SearchDrone(int id)
    {
        foreach (var drone in drones)
        {
            if (drone.DroneId == id)
            {
                UnityEngine.Debug.Log($"Drone {id} found at position: {drone.transform.position}");
                return drone;
            }
        }
        UnityEngine.Debug.Log($"Drone {id} not found.");
        return null;
    }

    public void SelfDestructDrone(int id)
    {
        var droneNode = drones.First;
        while (droneNode != null)
        {
            if (droneNode.Value.DroneId == id)
            {
                droneNode.Value.SelfDestruct();
                UnityEngine.Debug.Log($"Drone {id} has self-destructed.");
                drones.Remove(droneNode);
                return;
            }
            droneNode = droneNode.Next;
        }
        UnityEngine.Debug.Log($"Drone {id} not found for self-destruction.");
    }
}
