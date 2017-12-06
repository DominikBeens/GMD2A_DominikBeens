using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Enemy Spawning/Wave")]
public class Spawn_Wave : ScriptableObject
{

    public List<Spawn_Enemy> enemies = new List<Spawn_Enemy>();
    public float endOfWaveCountDown;
}
