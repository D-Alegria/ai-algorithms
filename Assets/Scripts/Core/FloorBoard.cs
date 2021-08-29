using System;
using UnityEngine;

namespace Core
{
    public class FloorBoard : SessionMember, IChangeNotifier<int>
    {
        public int value;
        public Color neon;
        public Color purp;
        private Renderer _boardRenderer;
        private bool _isPaintable = true;

        void Awake()
        {
            _boardRenderer = gameObject.GetComponent<Renderer>();
        }

        // Start is called before the first frame update
        void Start()
        {
            SetColor();
        }

        protected override void ONSessionStateChanged(SessionState state)
        {
            _isPaintable = !(state is SessionState.Running);
        }

        private void SetColor()
        {
            switch (value)
            {
                case 0:
                    _boardRenderer.material.color = purp;
                    _boardRenderer.material.DisableKeyword("_EMISSION");
                    break;
                case 9:
                    _boardRenderer.material.color = neon;
                    _boardRenderer.material.EnableKeyword("_EMISSION");
                    break;
                default:
                    _boardRenderer.material.color = Color.white;
                    _boardRenderer.material.DisableKeyword("_EMISSION");
                    break;
            }
        }

        private void ToggleValue()
        {
            if(!_isPaintable) return;
            value = value switch
            {
                0 => 1,
                1 => 0,
                _ => value
            };

            SetColor();
            NotifyObservers();
        }

        private void OnMouseDown()
        {
            ToggleValue();
        }

        private void OnMouseEnter()
        {
            if (Input.GetMouseButton(0))
            {
                ToggleValue();
            }
        }

        public void NotifyObservers()
        {
            ONStateChanged?.Invoke(value);
        }

        public event Action<int> ONStateChanged;
    }
}