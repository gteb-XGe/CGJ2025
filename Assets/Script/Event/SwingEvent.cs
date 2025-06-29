using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SwingEvent1 : MonoBehaviour, BaseEvent
{  [SerializeField] Transform startPoint, endPoint;
    [SerializeField] Swing swing;
    public float speed = 5f;
    private bool fnished=false;
    private void Start()
    {

    }
    public void StartEvent(GameObject player)
    {
        //�ı��ת����Χ
        swing.maxAngle=90f-swing.maxAngle;
        if (!fnished)
        {
            StageLoop.instance.finish();
            fnished = true;
        }
    }

    public void EndEvent(GameObject player)
    {
        Vector3 AB = endPoint.position - startPoint.position;
        Vector2 AB_xy = new Vector2(AB.x, AB.y);
        //������߷���
        Vector2 perp_xy = new Vector2(-AB_xy.y, AB_xy.x).normalized;
        Vector3 perp3D = new Vector3(perp_xy.x, perp_xy.y, 0);

        // �����ٶ�

        Vector3 velocity = perp3D * speed;
        if (player.GetComponent<Rigidbody2D>() != null)
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x, velocity.y);
        }
      
    }


}
