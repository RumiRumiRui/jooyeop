using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveAgent : MonoBehaviour
{
    public List<Transform> wayPoints;
    public int nextIdx;

    private NavMeshAgent agent;
    private Transform monsterTr;
    private readonly float patrolSpeed = 1.5f;
    private readonly float traceSpeed = 4.0f;
    private float damping = 1.0f;

    private bool _patrolling;
    public bool patrolling
    {
        get { return _patrolling; }
        set
        {
            _patrolling = value;
            if (_patrolling)
            {
                agent.speed = patrolSpeed;
                damping = 1.0f;
                MoveWayPoint();
            }
        }
    }

    private Vector2 _traceTarget;
    public Vector2 traceTarget
    {
        get { return _traceTarget; }
        set
        {
            _traceTarget = value;
            agent.speed = traceSpeed;
            damping = 10.0f;
            TraceTarget(_traceTarget);
        }
    }

    public float speed
    {
        get { return agent.velocity.magnitude; }
    }

    private void OnEnable()
    {
        var group = GameObject.Find("WayPointGroup");
        if (group != null)
        {
            group.GetComponentsInChildren<Transform>(wayPoints);
            wayPoints.RemoveAt(0);
            nextIdx = Random.Range(0, wayPoints.Count);
        }
    }

    void Start()
    {
        monsterTr = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        agent.updateRotation = false;
        this.patrolling = true;
    }

    void MoveWayPoint()
    {
        if (agent.isPathStale) return;
        agent.destination = wayPoints[nextIdx].position;
        agent.isStopped = false;
    }

    void TraceTarget(Vector2 pos)
    {
        if (agent.isPathStale) return;
        agent.destination = pos;
        agent.isStopped = false;
    }

    public void Stop()
    {
        agent.isStopped = true;
        agent.velocity = Vector2.zero;
        patrolling = false;
    }

    void Update()
    {
        if (agent.isStopped == false)
        {
            Quaternion rot = Quaternion.LookRotation(agent.desiredVelocity);
            monsterTr.rotation = Quaternion.Slerp(monsterTr.rotation, rot, Time.deltaTime * damping);
        }
        if (!_patrolling) return;
        if (agent.velocity.sqrMagnitude >= 0.2F * 0.2 && agent.remainingDistance <= 0.5f)
        {
            nextIdx = Random.Range(0, wayPoints.Count);
            MoveWayPoint();
        }
    }
}
