methods = {
    'CanUpgrade',
	'UpgradeRequire',
    'Upgrade',
}

local ExpCost = { 200, 1000, 5000 }

local GoldCost = { 20, 100, 500 }

function CanUpgrade(follower)
	return follower.CurLevel >= 30
end

function UpgradeRequire(follower)
	pack = Package()
	pack.Exp = ExpCost[follower.CurStar + 1]
	pack.Money = GoldCost[follower.CurStar + 1]
    return pack
end

function Upgrade(follower)
	follower.CurStar = follower.CurStar + 1
	follower.CurLevel = follower.CurLevel - 30
    return true
end
