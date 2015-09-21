methods = {
    'GetGrowAttack',
	'GetAttack',
}


local baseAttack = { 0, 300, 1000, 2500, 5000, 9000 }
local growAttack = { 10, 20, 30, 50, 100, 150 }

function GetGrowAttack(star, level)
    return baseAttack[star + 1] + growAttack[star + 1] * level
end

function GetAttack(follower)
	return follower.BasicAttack + GetGrowAttack(follower.CurStar, follower.CurLevel) * follower.GrowRatio
end
