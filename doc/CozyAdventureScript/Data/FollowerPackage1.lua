function GetFollowerPackage()
    fp = FollowerPackage()
    local i = 0

    i = i+1
    f1 = Follower()
    f1.Id = i
    f1.Name = "路人甲"
    f1.BasicAttack = 5
    f1.GrowRatio = 1.1
	f1.Avator = 'D001fighter01'
    fp:AddFollower(f1)

    i = i+1
    f2 = Follower()
    f2.Id = i
    f2.Name = "路人乙"
    f2.BasicAttack = 1
    f2.GrowRatio = 0.8
    fp:AddFollower(f2)

    return fp
end

