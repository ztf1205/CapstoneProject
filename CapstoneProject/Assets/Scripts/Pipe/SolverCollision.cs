using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;
using System;

public class SolverCollision : MonoBehaviour
{
    [SerializeField] int phase;

    private ObiSolver solver;
    [SerializeField] private ObiCollider fireCollider;
    [SerializeField] private Fire fire;

    [SerializeField] private ObiEmitter emitter;

    [SerializeField] private ObiEmitter emitter2;

    void Start()
    {
        solver = GetComponent<ObiSolver>();
        solver.OnCollision += Solver_OnCollision;
    }

    private void Solver_OnCollision(ObiSolver s, ObiSolver.ObiCollisionEventArgs e)
    {
        var world = ObiColliderWorld.GetInstance();

        try
        {
            foreach (Oni.Contact contact in e.contacts)
            {
                if (contact.distance < 0.01f)
                {
                    var col = world.colliderHandles[contact.bodyB].owner;
                    if (col == fireCollider)
                    {
                        if (fire.HP == 3000)
                        {
                            EventManager.TriggerEvent("FireExtingush");
                        }
                        fire.ReduceHP(1);
                        if (fire.HP <= 0)
                            OnFireDied();
                    }
                }
            }
        }
        catch (ArgumentException ex)
        {
            Debug.LogError(ex);
            return;
        }
    }

    private void OnFireDied()
    {
        fire.SetActiveFalse();

        if (phase == 1)
        {
            emitter.speed = 0;
            emitter2.speed = 3;
            EventManager.TriggerEvent("OpenDoor1");
            EventManager.TriggerEvent("DrawOutline");
        }
        else
        {
            emitter.speed = 0;
            EventManager.TriggerEvent("OpenDoor2");
        }
    }
}
