using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BaseEvent
{

   public void StartEvent(GameObject player);//物品的触发事件，具体物品写具体的脚本继承
    public void EndEvent(GameObject player);
}
