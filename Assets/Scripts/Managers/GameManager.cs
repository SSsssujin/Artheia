using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    private readonly ResourceManager _resource = new();

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            CombatEntity ce = new((int)Random.Range(80,150));
        }
    }

    public static ResourceManager Resource => Instance._resource;
}
