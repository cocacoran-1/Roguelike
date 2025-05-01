using UnityEngine;

namespace Game.Map
{
    public class InfiniteMapManager : MonoBehaviour
    {
        [Header("Å¸ÀÏ¸Ê ÇÁ¸®ÆÕ ¹× Å©±â")]
        [SerializeField] private GameObject tilemapPrefab;
        [SerializeField] private Vector2 tilemapSize = new Vector2(10f, 10f);
        [SerializeField] private Transform player;

        private Transform[,] tilemaps = new Transform[3, 3];
        private Vector2 centerPosition;
        private Vector2Int centerTileIndex;

        private void Start()
        {
            centerTileIndex = Vector2Int.zero;

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    Vector2 offset = new Vector2((x - 1) * tilemapSize.x, (y - 1) * tilemapSize.y);
                    Vector2 pos = centerPosition + offset;
                    GameObject tilemap = Instantiate(tilemapPrefab, pos, Quaternion.identity, transform);
                    tilemaps[x, y] = tilemap.transform;
                }
            }
        }

        private void Update()
        {
            Vector2 playerPos = player.position;
            Vector2 centerWorldPos = new Vector2(centerTileIndex.x * tilemapSize.x, centerTileIndex.y * tilemapSize.y);
            Vector2 delta = playerPos - centerWorldPos;

            int moveX = 0, moveY = 0;

            if (delta.x > tilemapSize.x / 2f)
                moveX = 1;
            else if (delta.x < -tilemapSize.x / 2f)
                moveX = -1;

            if (delta.y > tilemapSize.y / 2f)
                moveY = 1;
            else if (delta.y < -tilemapSize.y / 2f)
                moveY = -1;

            if (moveX != 0 || moveY != 0)
            {
                centerTileIndex += new Vector2Int(moveX, moveY);
                UpdateTilemaps(moveX, moveY);
            }
        }

        private void UpdateTilemaps(int moveX, int moveY)
        {
            if (moveX != 0)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (moveX == 1)
                    {
                        Transform left = tilemaps[0, y];
                        tilemaps[0, y] = tilemaps[1, y];
                        tilemaps[1, y] = tilemaps[2, y];
                        tilemaps[2, y] = left;

                        Vector3 pos = tilemaps[2, y].position;
                        pos.x += tilemapSize.x * 3f;
                        tilemaps[2, y].position = pos;
                    }
                    else if (moveX == -1)
                    {
                        Transform right = tilemaps[2, y];
                        tilemaps[2, y] = tilemaps[1, y];
                        tilemaps[1, y] = tilemaps[0, y];
                        tilemaps[0, y] = right;

                        Vector3 pos = tilemaps[0, y].position;
                        pos.x -= tilemapSize.x * 3f;
                        tilemaps[0, y].position = pos;
                    }
                }
            }

            if (moveY != 0)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (moveY == 1)
                    {
                        Transform bottom = tilemaps[x, 0];
                        tilemaps[x, 0] = tilemaps[x, 1];
                        tilemaps[x, 1] = tilemaps[x, 2];
                        tilemaps[x, 2] = bottom;

                        Vector3 pos = tilemaps[x, 2].position;
                        pos.y += tilemapSize.y * 3f;
                        tilemaps[x, 2].position = pos;
                    }
                    else if (moveY == -1)
                    {
                        Transform top = tilemaps[x, 2];
                        tilemaps[x, 2] = tilemaps[x, 1];
                        tilemaps[x, 1] = tilemaps[x, 0];
                        tilemaps[x, 0] = top;

                        Vector3 pos = tilemaps[x, 0].position;
                        pos.y -= tilemapSize.y * 3f;
                        tilemaps[x, 0].position = pos;
                    }
                }
            }
        }
    }
}
