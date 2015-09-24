function GetFollowerPackage()
    fp = FollowerPackage()
    local i = 0

    i = i+1
    f1 = Follower()
    f1.Id = i
    f1.Name = "路人甲"
	f1.BasicAttack = 5
    f1.GrowRatio = 1.1
	f1.Avatar = 'lurenjia'
    fp:AddFollower(f1)

    i = i+1
    f2 = Follower()
    f2.Id = i
    f2.Name = "路人乙"
	f2.BasicAttack = 1
    f2.GrowRatio = 0.8
	f2.Avatar = 'D001fighter01'
    fp:AddFollower(f2)

	i = i+1
    f3 = Follower()
    f3.Id = i
    f3.Name = "路人丙"
	f3.BasicAttack = 1
    f3.GrowRatio = 0.8
	f3.Avatar = 'D001fighter02'
    fp:AddFollower(f3)

	i = i+1
    f4 = Follower()
    f4.Id = i
    f4.Name = "路人丁"
    f4.BasicAttack = 1
    f4.GrowRatio = 0.8
	f4.Avatar = 'D001fighter03'
    fp:AddFollower(f4)

	i = i+1
    f5 = Follower()
    f5.Id = i
    f5.Name = "路人戊"
    f5.BasicAttack = 1
    f5.GrowRatio = 0.8
	f5.Avatar = 'D001fighter04'
    fp:AddFollower(f5)

	i = i+1
    f6 = Follower()
    f6.Id = i
    f6.Name = "路人己"
    f6.BasicAttack = 1
    f6.GrowRatio = 0.8
	f6.Avatar = 'D001fighter05'
    fp:AddFollower(f6)

	i = i+1
    f7 = Follower()
    f7.Id = i
    f7.Name = "路人庚"
    f7.BasicAttack = 1
    f7.GrowRatio = 0.8
	f7.Avatar = 'D001fighter06'
    fp:AddFollower(f7)

	i = i+1
    f8 = Follower()
    f8.Id = i
    f8.Name = "路人辛"
    f8.BasicAttack = 1
    f8.GrowRatio = 0.8
	f8.Avatar = 'D001fighter07'
    fp:AddFollower(f8)

	i = i+1
    f9 = Follower()
    f9.Id = i
    f9.Name = "路人壬"
    f9.BasicAttack = 1
    f9.GrowRatio = 0.8
	f9.Avatar = 'D001fighter08'
    fp:AddFollower(f9)

	i = i+1
    f10 = Follower()
    f10.Id = i
    f10.Name = "路人癸"
    f10.BasicAttack = 1
    f10.GrowRatio = 0.8
	f10.Avatar = 'D009lancer01'
    fp:AddFollower(f10)

	i = i+1
    f11 = Follower()
    f11.Id = i
    f11.Name = "李文超"
    f11.BasicAttack = 65535
    f11.GrowRatio = 1.0
	f11.Desc = '大家叫他愤怒的泡面。给Xamarin提交过代码。'
	f11.Avatar = 'D010lancer02'
	f11.CurStar = 0
	f11.MaxStar = 3
	f11.CurLevel = 11

    fp:AddFollower(f11)

    return fp
end

