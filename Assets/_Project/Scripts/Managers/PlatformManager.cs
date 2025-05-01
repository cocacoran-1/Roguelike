using UnityEngine;

namespace Game.Managers
{
    public class PlatformManager : MonoBehaviour
    {
        public static PlatformManager Instance { get; private set; }

        public bool IsMobile { get; private set; }

        [SerializeField] private bool forceMobileInEditor = false;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);

#if UNITY_ANDROID || UNITY_IOS
                IsMobile = true;
#else
                IsMobile = forceMobileInEditor;
#endif
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
