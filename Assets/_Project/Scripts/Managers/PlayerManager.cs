using UnityEngine;

namespace Game.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { get; private set; }

        public Transform PlayerTransform { get; private set; }
        public Player.PlayerController PlayerController { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);

                var player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    PlayerTransform = player.transform;
                    PlayerController = player.GetComponent<Player.PlayerController>();
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}