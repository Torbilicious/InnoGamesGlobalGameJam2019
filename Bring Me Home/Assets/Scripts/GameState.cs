using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameState {
    public static int nexLevel = 1;
    public static bool isDead = false;
    
    public static void Reset()
    {
        nexLevel = 1;
        isDead = false;
    }
}
