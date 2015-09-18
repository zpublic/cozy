methods = {
    'CanUpgrade',
	'UpgradeRequire',
    'Upgrade',
}

FollowerStarModule = {}

FollowerStarModule.ExpCost = { 200, 1000, 5000 }

FollowerStarModule.GoldCost = { 20, 100, 500 }

function FollowerStarModule.CanUpgrade(follower)
	return follower.CurLevel >= 30
end

function FollowerStarModule.UpgradeRequire(follower)
	pack = Package()
	pack.Exp = FollowerStarModule.ExpCost[follower.CurStar + 1]
	pack.Money = FollowerStarModule.GoldCost[follower.CurStar + 1]
    return pack
end

function FollowerStarModule.Upgrade(follower)
	follower.CurStar = follower.CurStar + 1
	follower.CurLevel = follower.CurLevel - 30
    return true
end
