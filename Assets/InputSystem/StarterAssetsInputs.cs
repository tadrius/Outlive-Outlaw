using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool attack;
        public bool autoAttack;
        public bool reload;
        public bool zoom;
		public float cycleEquipment;
		public bool toggleFlashlight;

        [Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
        public bool cursorInputForAttack = true;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if (cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

        public void OnAttack(InputValue value)
        {
			if (cursorInputForAttack)
			{
                AttackInput(value.isPressed);
            }
        }

        public void OnAutoAttack(InputValue value)
        {
            if (cursorInputForAttack)
            {
                AutoAttackInput(value.isPressed);
            }
        }

        public void OnReload(InputValue value)
        {
            ReloadInput(value.isPressed);
        }

        public void OnZoom(InputValue value)
        {
            if (cursorInputForLook)
            {
                ZoomInput(value.isPressed);
            }
        }

		public void OnCycleEquipment(InputValue value)
		{
			CycleEquipmentInput(value.Get<float>());
		}

        public void OnToggleFlashlight(InputValue value)
        {
            ToggleFlashlightInput(value.isPressed);
        }
#endif


        public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

        public void AttackInput(bool newAttackState)
        {
            attack = newAttackState;
        }

        public void AutoAttackInput(bool newAttackState)
        {
            autoAttack = newAttackState;
        }

		public void ReloadInput(bool newReloadState)
		{
			reload = newReloadState;
		}

		public void ZoomInput(bool newZoomState)
		{
			zoom = newZoomState;
		}

		public void CycleEquipmentInput(float newCycleEquipmentValue)
		{
			cycleEquipment = newCycleEquipmentValue;
		}

        public void ToggleFlashlightInput(bool newToggleFlashlightInput)
        {
            toggleFlashlight = newToggleFlashlightInput;
        }

        private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}