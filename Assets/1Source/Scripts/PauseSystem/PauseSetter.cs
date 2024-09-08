using UnityEngine;

namespace System.PauseSystem
{
    public class PauseSetter
    {
        public void Enable()
        {
            Time.timeScale = 0;
            AudioListener.pause = true;
        }

        public void Disable()
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
        }
    }
}