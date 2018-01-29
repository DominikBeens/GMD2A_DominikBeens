using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Spawning/Spawner")]
public class Spawn_Spawner : ScriptableObject
{

    public List<Spawn_Wave> spawns = new List<Spawn_Wave>();
}
