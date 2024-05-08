using UnityEngine;
using UnityEngine.UI;

namespace slotgame
{
    public class SlotGameController : StateMachine
    {
        public static SlotGameController Instance;
        [SerializeField] Button spinBtn;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        void Start()
        {
           spinBtn.onClick.AddListener(SetSpinState);
            ChangeState<SpinState>();
        }

        public void SetSpinState()
        {
            spinBtn.interactable = false;
            ChangeState<SpinState>();
        }

        public void SetResultState()
        {
            ChangeState<ResultState>();
        }

        public void SetIdleState()
        {
            spinBtn.interactable = true;
            ChangeState<IdleState>();
        }

        private void OnDestroy()
        {
            spinBtn.onClick.RemoveAllListeners();
        }

       
    }
}