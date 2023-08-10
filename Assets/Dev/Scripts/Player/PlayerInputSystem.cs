using Leopotam.Ecs;
using UnityEngine;

public class PlayerInputSystem : IEcsRunSystem
{
	private EcsFilter<PlayerInputData> _inputFilter;

	public void Run()
    {
        foreach (int dataIndex in _inputFilter)
        {
            ref var input = ref _inputFilter.Get1(dataIndex);

            input.MouseXAxis = Input.GetAxis("Mouse X");
            input.MouseYAxis = Input.GetAxis("Mouse Y");

            input.MoveHorizontalAxis = Input.GetAxisRaw("Horizontal");
            input.MoveVerticalAxis = Input.GetAxisRaw("Vertical");

            if (Input.GetKey(KeyCode.Space))
            {
                if (!input.IsTryingToJump)
                    input.IsTryingToJump = true;
            }
            else if (input.IsTryingToJump)
            {
                input.IsTryingToJump = false;
            }

            input.IsTryingToRun = (Input.GetKey(KeyCode.LeftShift)) ? true : false;
        }
    }
}