methods = {
    'CanUpgrade',
	'UpgradeRequire',
    'Upgrade',
}

local ExpCost = { 200, 1000, 5000 }

local GoldCost = { 20, 100, 500 }

function CanUpgrade(follower)
	if follower.CurLevel < 30 then return false end
	if follower.MaxStar == follower.CurStar then return false end
	if PlayerObject.Instance.Self.Exp < ExpCost[follower.CurStar + 1] then return false end
	if PlayerObject.Instance.Self.Money < GoldCost[follower.CurStar + 1] then return false end
	return true
end

function UpgradeRequire(follower)
	pack = Package()
	pack.Exp = ExpCost[follower.CurStar + 1]
	pack.Money = GoldCost[follower.CurStar + 1]
    return pack
end

function Upgrade(follower)
	pack = UpgradeRequire(follower)
	PlayerObject.Instance.Self.Exp 		= PlayerObject.Instance.Self.Exp  - pack.Exp
	PlayerObject.Instance.Self.Money 	= PlayerObject.Instance.Self.Money - pack.Money
	follower.CurStar = follower.CurStar + 1
	follower.CurLevel = follower.CurLevel - 30

    return true
end
