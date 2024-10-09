using UnityEngine;
using UnityEngine.UI;

namespace Scripts.LevelSystem
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Color _completedColor;
        [SerializeField] private Color _bossColor;

        public void SetCompletedColor()
        {
            _image.color = _completedColor;
        }

        public void SetBossColor()
        {
            _image.color = _bossColor;
        }
    }
}