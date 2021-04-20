using UnityEngine;
using Ruoran.Roguelike.Dungeon;

public class Test : MonoBehaviour
{
    int one = 0;
    // Use this for initialization
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (one == 0)
        {
            one = 1;
            TestFunc();
        }
        else return;
    }

    void TestFunc()
    {
        DungeonGenerator.Build();
    }
}
