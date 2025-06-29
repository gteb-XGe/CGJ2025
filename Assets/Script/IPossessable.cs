using UnityEngine;


public interface IPossessable
{
    public void Possess();
    public void Unpossess();
    public void ChangeState(int dir);
}