using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakableGlass : MonoBehaviour, ITakeHits
{
    public bool Alive => true;
    private BreakableWindow window;

    public void TakeHit(IDamage hitBy)
    {
        window.breakWindow();
    }

    // Start is called before the first frame update
    void Start()
    {
        window = GetComponentInChildren<BreakableWindow>();
    }

}
