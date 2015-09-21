methods = {
	'GetAttack',
	'GetHp'
}

function GetAttack(fcollect)
	attack = 0

	for i = 0, fcollect.Followers.Count - 1, 1 do
		attack = attack + ModuleManager.Instance:GetModule('FollowerModule'):GetAttack(fcollect.Followers[i])
	end

	return attack
end

function GetHp (fcollect)
	return GetAttack(fcollect)
end
