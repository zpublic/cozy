function GetFollowerPackage()
    fp = FollowerPackage()
    local i = 0

    i = i+1
    f1 = Follower()
    f1.Id = i
    f1.Name = "路人甲"
    f1.BasicAttack = 5
    f1.GrowRatio = 1.1
	f1.Avator = 'lurenjia'
    fp:AddFollower(f1)

    i = i+1
    f2 = Follower()
    f2.Id = i
    f2.Name = "路人乙"
    f2.BasicAttack = 1
    f2.GrowRatio = 0.8
	f1.Avator = 'D001fighter01'
    fp:AddFollower(f2)

	i = i+1
    f2 = Follower()
    f2.Id = i
    f2.Name = "路人A"
    f2.BasicAttack = 1
    f2.GrowRatio = 0.8
	f1.Avator = 'D001fighter02'
    fp:AddFollower(f2)

	i = i+1
    f2 = Follower()
    f2.Id = i
    f2.Name = "路人B"
    f2.BasicAttack = 1
    f2.GrowRatio = 0.8
	f1.Avator = 'D001fighter03'
    fp:AddFollower(f2)

	i = i+1
    f2 = Follower()
    f2.Id = i
    f2.Name = "路人C"
    f2.BasicAttack = 1
    f2.GrowRatio = 0.8
	f1.Avator = 'D001fighter04'
    fp:AddFollower(f2)

	i = i+1
    f2 = Follower()
    f2.Id = i
    f2.Name = "路人D"
    f2.BasicAttack = 1
    f2.GrowRatio = 0.8
	f1.Avator = 'D001fighter05'
    fp:AddFollower(f2)

	i = i+1
    f2 = Follower()
    f2.Id = i
    f2.Name = "路人E"
    f2.BasicAttack = 1
    f2.GrowRatio = 0.8
	f1.Avator = 'D001fighter06'
    fp:AddFollower(f2)

	i = i+1
    f2 = Follower()
    f2.Id = i
    f2.Name = "路人F"
    f2.BasicAttack = 1
    f2.GrowRatio = 0.8
	f1.Avator = 'D001fighter07'
    fp:AddFollower(f2)

	i = i+1
    f2 = Follower()
    f2.Id = i
    f2.Name = "路人G"
    f2.BasicAttack = 1
    f2.GrowRatio = 0.8
	f1.Avator = 'D001fighter08'
    fp:AddFollower(f2)

	i = i+1
    f2 = Follower()
    f2.Id = i
    f2.Name = "路人H"
    f2.BasicAttack = 1
    f2.GrowRatio = 0.8
	f1.Avator = 'D001fighter09'
    fp:AddFollower(f2)

	i = i+1
    f2 = Follower()
    f2.Id = i
    f2.Name = "路人I"
    f2.BasicAttack = 1
    f2.GrowRatio = 0.8
	f1.Avator = 'D001fighter10'
    fp:AddFollower(f2)

	i = i+1
    f2 = Follower()
    f2.Id = i
    f2.Name = "路人J"
    f2.BasicAttack = 1
    f2.GrowRatio = 0.8
	f1.Avator = 'D001fighter11'
    fp:AddFollower(f2)

    return fp
end

