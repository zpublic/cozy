require('FollowerModule')

methods = {
	'GetAttack',
	'GetHp'
}

FollowerCollectModule = {}

function FollowerCollectModule.GetAttack(fcollect)
	attack = 0
	for i = 0, fcollect.Followers.Count - 1, 1 do
		attack = attack + FollowerModule.GetAttack(fcollect.Followers[i])
	end
	return attack
end

function FollowerCollectModule.GetHp (fcollect)
	return FollowerCollectModule.GetAttack(fcollect)
end
