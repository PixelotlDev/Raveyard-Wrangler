using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    PlayerActions actions;

    // Start is called before the first frame update
    void Awake()
    {
        actions = new PlayerActions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        actions.gameplay.Enable();
    }

    private void OnDisable()
    {
        actions.gameplay.Disable();
    }
}
