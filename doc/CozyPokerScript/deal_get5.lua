function deal(cc)
    ccRet = CardCollect()
    ccRet:Add(cc:Get())
    ccRet:Add(cc:Get())
    ccRet:Add(cc:Get())
    ccRet:Add(cc:Get())
    ccRet:Add(cc:Get())
    return ccRet
end