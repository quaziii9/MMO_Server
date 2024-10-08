﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static Define;

public class PlayerController : CreatureController
{
	Coroutine _coSkill;

    protected override void Init()
    {
        base.Init();
    }

    protected override void UpdateController()
    {
		switch(State)
		{
			case CreatureState.Idle:
				GetDirInput();
				GetIdleInput();
				break;
			case CreatureState.Moving:
                GetDirInput();
                break;
		}
        GetDirInput();
        base.UpdateController();
    }

    private void LateUpdate()
    {
		Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, - 10);
    }

    // 키보드 입력
    void GetDirInput()
	{
		if (Input.GetKey(KeyCode.W))
		{
			Dir = MoveDir.Up;
		}
		else if (Input.GetKey(KeyCode.S))
		{
			Dir = MoveDir.Down;
		}
		else if (Input.GetKey(KeyCode.A))
		{
			Dir = MoveDir.Left;
		}
		else if (Input.GetKey(KeyCode.D))
		{
			Dir = MoveDir.Right;
		}
		else
		{
			Dir = MoveDir.None;
		}
	}

	void GetIdleInput()
	{
        if (Input.GetKey(KeyCode.Space))
		{
            State = CreatureState.Skil;
			_coSkill = StartCoroutine("CoStartPunch");
		}
    }

	IEnumerator CoStartPunch()
	{
		// 피격 판정
		GameObject go = Managers.Object.Find(GetFrontCellPos());
		if(go!= null)
		{
			Debug.Log(go.name);
		}
		// 대기 시간
		yield return new WaitForSeconds(0.5f);
		State = CreatureState.Idle;
		_coSkill = null;
	}
}
