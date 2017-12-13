using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour{
    public List<Agent> squids = new List<Agent>();

    public float radiusY = 1f;
    public float radiusX = 1f;
    public Vector3 center;

    void Start() {
        Agent[] agents = gameObject.GetComponentsInChildren<Agent>();
        squids.AddRange(agents);
        foreach (Agent agent in agents)
        {
            agent.world = this;
        }
    }

    public bool IsInBounds(Vector2 position) {
        float x = position.x;
        float y = position.y;
        float h = center.x;
        float k = center.y;
        float xTerm = Mathf.Pow(x - h, 2) / Mathf.Pow(radiusX, 2);
        float yTerm = Mathf.Pow(y - k, 2) / Mathf.Pow(radiusY, 2);
        bool inBounds = (xTerm + yTerm <= 1);
        return inBounds;
    }


    public List<Agent> GetAgents(int id) {
        switch (id)
        {
            case 3:
                return squids;
            default:
                return null;
        }
    }


}
