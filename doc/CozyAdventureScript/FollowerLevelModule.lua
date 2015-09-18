methods = {
    'CanUpgrade',
	'UpgradeRequire',
    'Upgrade',
}

FollowerLevelModule = {}

FollowerLevelModule.exp = {
	{5,7.3,9.6,11.9,14.2,16.5,18.8,21.1,23.4,25.7,28,30.3,32.6,34.9,37.2,39.5,41.8,44.1,46.4,48.7,51,53.3,55.6,57.9,60.2,62.5,64.8,67.1,69.4,71.7,74,76.3,78.6,80.9,83.2,85.5,87.8,90.1,92.4,94.7,97,100},
	{106.7,113.4,120.1,126.8,133.5,140.2,146.9,153.6,160.3,167,173.7,180.4,187.1,193.8,200.5,207.2,213.9,220.6,227.3,234,240.7,247.4,254.1,260.8,267.5,274.2,280.9,287.6,294.3,300},
	{323.4,346.8,370.2,393.6,417,440.4,463.8,487.2,510.6,534,557.4,580.8,604.2,627.6,651,674.4,697.8,721.2,744.6,768,791.4,814.8,838.2,861.6,885,908.4,931.8,955.2,978.6, 1000},
}


function FollowerLevelModule.CanUpgrade(follower)
    return true
end

function FollowerLevelModule.UpgradeRequire(follower)
	pack = Package()
	pack.Exp = FollowerLevelModule.exp[follower.CurStar + 1][follower.CurLevel + 1]
	return pack
end

function FollowerLevelModule.Upgrade(follower)
    follower.CurLevel = follower.CurLevel + 1
	return true
end
