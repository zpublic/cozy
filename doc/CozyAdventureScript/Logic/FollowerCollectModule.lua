methods = {
	'GetAttack',
	'GetHp'
}

function GetAttack(fcollect)
	getattack = ModuleManager.Instance.GetModule('FollowerModule').GetTableFunction('GetAttack')
	attack = 0
	for i = 0, fcollect.Followers.Count - 1, 1 do
		attack = attack + getattack.Call(fcollect.Followers[i])
	end
	return attack
end

function GetHp (fcollect)
	return GetAttack(fcollect)
end
