methods = {
    'GetGrowAttack',
	'GetAttack',
}

FollowerModule = {}

FollowerModule.baseAttack = { 0, 300, 1000, 2500, 5000, 9000 }
FollowerModule.growAttack = { 10, 20, 30, 50, 100, 150 }

function FollowerModule.GetGrowAttack(star, level)
    return FollowerModule.baseAttack[star + 1] + FollowerModule.growAttack[star + 1] * level
end

function FollowerModule.GetAttack(follower)
	return follower.BasicAttack + FollowerModule.GetGrowAttack(follower.CurStar, follower.CurLevel) * follower.GrowRatio
end
