using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField]
    private Timer m_timer = new Timer(3, 0);

    public Action OnTimerEnd;

    public void StartTimer()
    {
        m_timer.Create(EndTimer);
        OnScreenTimer.Instance.SetTimer(m_timer);

        StartCoroutine(DoTimePass());
    }

    private void EndTimer()
    {
        OnTimerEnd?.Invoke();
        GameManager.SwitchState(GameState.GameOver);
        LevelManager.Instance.EndLevel();
        Debug.Log("Time up!");
    }

    private IEnumerator DoTimePass()
    {
        while (true)
        {
            m_timer.Tick();

            if (m_timer.CurrentSeconds == 0)
                yield break;

            else
                yield return null;
        }
    }
}
