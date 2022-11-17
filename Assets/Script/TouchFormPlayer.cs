using UnityEngine;

    public class TouchFormPlayer : MonoBehaviour
    {
        public bool isTouch;
        public  BoxCollider2D _boxCollider2D;
        public void Start()
        {
            _boxCollider2D = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            TouchUpdate();
        }

        void TouchUpdate()
        {
            isTouch = false;
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (_boxCollider2D.OverlapPoint(Input.touches[i].position))
                {
                    isTouch = true;
                    break;
                }
            }
        }
    }
