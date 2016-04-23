Table (package)
{
    Table (message)
    {
        Table t_GameZoneProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table zone(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table game(name)
            Table 2(memberID)
        }
    }
    Table (message)
    {
        Table t_NewLoginSessionProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table wdLoginID(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table wdGatewayID(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table loginTempID(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table bytes(type)
            Table domain(name)
            Table 4(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table wdPort(name)
            Table 5(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table t_GameZoneProto(type)
            Table gameZone(name)
            Table 6(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table t_GameZoneProto(type)
            Table flatZone(name)
            Table 7(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint64(type)
            Table accid(name)
            Table 8(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table type(name)
            Table 9(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table state(name)
            Table 10(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table string(type)
            Table name(name)
            Table 11(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table bytes(type)
            Table passwd(name)
            Table 12(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table bytes(type)
            Table client_ip(name)
            Table 13(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table bytes(type)
            Table numpasswd(name)
            Table 14(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table bytes(type)
            Table passwd2(name)
            Table 15(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table accSafe(name)
            Table 16(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table wdNetType(name)
            Table 17(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table userType(name)
            Table 18(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table createTime(name)
            Table 19(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table viplevel(name)
            Table 20(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table memlevel(name)
            Table 21(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table annualFlag(name)
            Table 22(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table memberFlag(name)
            Table 23(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table vipFlag(name)
            Table 24(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table microclient(name)
            Table 25(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table qqmember(name)
            Table 26(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table qqyearmember(name)
            Table 27(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table qqlevel(name)
            Table 28(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table bytes(type)
            Table openid(name)
            Table 29(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table bytes(type)
            Table loginurl(name)
            Table 30(memberID)
        }
    }
    Table (message)
    {
        Table t_SelectUserProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table ret(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table taskTempid(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table t_GameZoneProto(type)
            Table zone(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint64(type)
            Table accid(name)
            Table 4(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table string(type)
            Table name(name)
            Table 5(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table level(name)
            Table 6(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table onlinetime(name)
            Table 7(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table sex(name)
            Table 8(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table job(name)
            Table 9(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table createtime(name)
            Table 10(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table charid(name)
            Table 11(memberID)
        }
    }
    Table (message)
    {
        Table t_MapUserSculptProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table dwHorseID(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table dwHeadID(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table dwBodyID(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table dwlegsID(name)
            Table 4(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table dwhandsID(name)
            Table 5(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table dwfeetsID(name)
            Table 6(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table dwWeaponID(name)
            Table 7(memberID)
        }
    }
    Table (message)
    {
        Table t_SelectUserInfoProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table id(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table string(type)
            Table name(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table type(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table level(name)
            Table 4(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table mapid(name)
            Table 5(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table string(type)
            Table mapName(name)
            Table 6(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table country(name)
            Table 7(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table group(name)
            Table 8(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table face(name)
            Table 9(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table hair(name)
            Table 10(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table string(type)
            Table countryName(name)
            Table 11(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table bitmask(name)
            Table 12(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table forbidTime(name)
            Table 13(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table zone_state(name)
            Table 14(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table target_zone(name)
            Table 15(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table acceptPK(name)
            Table 16(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table t_MapUserSculptProto(type)
            Table sculpt(name)
            Table 17(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table flatZoneID(name)
            Table 18(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table gameZoneID(name)
            Table 19(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table activestar(name)
            Table 20(memberID)
        }
    }
    Table (message)
    {
        Table t_stUserInfoUserCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table version(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table repeated(condtion)
            Table t_SelectUserInfoProto(type)
            Table userset(name)
            Table 2(memberID)
        }
    }
    Table (message)
    {
        Table t_stCreateSelectUserCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table string(type)
            Table strUserName(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table jobType(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table charType(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table country(name)
            Table 4(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table suitType(name)
            Table 5(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table string(type)
            Table strPromoterName(name)
            Table 6(memberID)
        }
    }
    Table (message)
    {
        Table t_stMobileChannelChatUserCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table dwType(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table dwSysInfoType(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table dwCharType(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table dwCountry(name)
            Table 4(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table dwVip(name)
            Table 5(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table dwFromUserTempID(name)
            Table 6(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table dwChatTime(name)
            Table 7(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table string(type)
            Table pstrName(name)
            Table 8(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table string(type)
            Table pstrChat(name)
            Table 9(memberID)
        }
    }
    Table (message)
    {
        Table MainAttrib(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table id(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table string(type)
            Table name(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table int32(type)
            Table lv(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table int32(type)
            Table heroLvLimit(name)
            Table 4(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table energy(name)
            Table 5(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table energyLimit(name)
            Table 6(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint64(type)
            Table exp(name)
            Table 7(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint64(type)
            Table totleExp(name)
            Table 8(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table int32(type)
            Table title(name)
            Table 9(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table int32(type)
            Table icon(name)
            Table 10(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table int32(type)
            Table iconEdage(name)
            Table 11(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table gold(name)
            Table 12(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table money(name)
            Table 13(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table exploit(name)
            Table 14(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table totalpkscore(name)
            Table 15(memberID)
        }
    }
    Table (message)
    {
        Table stObjectLocationProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table dwLocation(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table x(name)
            Table 2(memberID)
        }
    }
    Table (message)
    {
        Table t_ObjectProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table qwThisID(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table dwObjectID(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table string(type)
            Table strName(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table dwNum(name)
            Table 4(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table stObjectLocationProto(type)
            Table pos(name)
            Table 5(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table lv(name)
            Table 6(memberID)
        }
    }
    Table (message)
    {
        Table t_AddObjectInfoProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table byActionType(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table t_ObjectProto(type)
            Table obj(name)
            Table 2(memberID)
        }
    }
    Table (message)
    {
        Table t_stAddObjectListUnityUserCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table version(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table repeated(condtion)
            Table t_AddObjectInfoProto(type)
            Table userset(name)
            Table 2(memberID)
        }
    }
    Table (message)
    {
        Table t_DaySignRwdRecProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table id(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table state(name)
            Table 2(memberID)
        }
    }
    Table (message)
    {
        Table t_DaySignCmdProto(messageName)
        Table (member)
        {
            Table repeated(condtion)
            Table t_DaySignRwdRecProto(type)
            Table signs(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_MailOpenCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table mailid(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_MailAddListProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table id(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table state(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table string(type)
            Table fromname(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table string(type)
            Table title(name)
            Table 4(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table accessory(name)
            Table 5(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table createtime(name)
            Table 6(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table endtime(name)
            Table 7(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table type(name)
            Table 8(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table needmoney(name)
            Table 9(memberID)
        }
    }
    Table (message)
    {
        Table t_MailContentProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table id(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table string(type)
            Table title(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table accessory(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table string(type)
            Table text(name)
            Table 4(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table sendmoney(name)
            Table 5(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table recvmoney(name)
            Table 6(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table sendgold(name)
            Table 7(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table recvgold(name)
            Table 8(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table t_ObjectProto(type)
            Table item(name)
            Table 9(memberID)
        }
    }
    Table (message)
    {
        Table t_PetSoldierStructProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table type(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table level(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table star(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint64(type)
            Table exp(name)
            Table 4(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint64(type)
            Table nextexp(name)
            Table 5(memberID)
        }
    }
    Table (message)
    {
        Table t_SoldierCmdProto(messageName)
        Table (member)
        {
            Table repeated(condtion)
            Table t_PetSoldierStructProto(type)
            Table soldiers(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_PetGeneralSkillProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table id(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table level(name)
            Table 2(memberID)
        }
    }
    Table (message)
    {
        Table t_PetGeneralStructProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table id(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table level(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table star(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table deathstate(name)
            Table 4(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint64(type)
            Table exp(name)
            Table 5(memberID)
        }
        Table (member)
        {
            Table repeated(condtion)
            Table t_PetGeneralSkillProto(type)
            Table skillset(name)
            Table 6(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint64(type)
            Table nextexp(name)
            Table 7(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table pkscore(name)
            Table 8(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table souls(name)
            Table 9(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table armySoliderCount(name)
            Table 10(memberID)
        }
        Table (member)
        {
            Table repeated(condtion)
            Table uint32(type)
            Table armyEquips(name)
            Table 11(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table t_PetSoldierStructProto(type)
            Table armyInfo(name)
            Table 12(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table nextsouls(name)
            Table 13(memberID)
        }
    }
    Table (message)
    {
        Table t_GeneralCmdProto(messageName)
        Table (member)
        {
            Table repeated(condtion)
            Table t_PetGeneralStructProto(type)
            Table generals(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_FightSettingProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table adviser(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table repeated(condtion)
            Table uint32(type)
            Table generals(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table mainGeneral(name)
            Table 3(memberID)
        }
    }
    Table (message)
    {
        Table t_GeneralSelectSoldierProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table general(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table repeated(condtion)
            Table uint32(type)
            Table soldiers(name)
            Table 2(memberID)
        }
    }
    Table (message)
    {
        Table t_stRandNameCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table string(type)
            Table name(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_stGetRandNameCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table sex(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_StudyGeneralSkillProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table general(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table skillid(name)
            Table 2(memberID)
        }
    }
    Table (message)
    {
        Table t_OneDungeonProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table id(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table enterTimes(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table int32(type)
            Table leftTimes(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table occupy(name)
            Table 4(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table star(name)
            Table 5(memberID)
        }
    }
    Table (message)
    {
        Table t_AllDungonCmdProto(messageName)
        Table (member)
        {
            Table repeated(condtion)
            Table t_OneDungeonProto(type)
            Table dungeonset(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_CompleteQuestCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table id(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table result(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table star(name)
            Table 3(memberID)
        }
    }
    Table (message)
    {
        Table t_ObjProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table objid(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table objnum(name)
            Table 2(memberID)
        }
    }
    Table (message)
    {
        Table t_CompleteQuestRewardCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table id(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table money(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table repeated(condtion)
            Table t_ObjProto(type)
            Table itemset(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table exploit(name)
            Table 4(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table generalExp(name)
            Table 5(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table soldierExp(name)
            Table 6(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table captureid(name)
            Table 7(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table completecount(name)
            Table 8(memberID)
        }
    }
    Table (message)
    {
        Table t_CaptiveGeneralProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table id(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table goodwill(name)
            Table 2(memberID)
        }
    }
    Table (message)
    {
        Table t_ALLCaptiveGeneralCmdProto(messageName)
        Table (member)
        {
            Table repeated(condtion)
            Table t_CaptiveGeneralProto(type)
            Table captiveset(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_GetOneCaptiveCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table id(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_UserItemCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table qwthisid(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table num(name)
            Table 2(memberID)
        }
    }
    Table (message)
    {
        Table t_DelObjCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table qwthisid(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_RefreshObjCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table qwthisid(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table num(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table type(name)
            Table 3(memberID)
        }
    }
    Table (message)
    {
        Table t_MoveObjCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table qwthisid(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table stObjectLocationProto(type)
            Table dst(name)
            Table 2(memberID)
        }
    }
    Table (message)
    {
        Table t_SplitObjCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table qwthisid(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table qwnewthisid(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table dwnum(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table stObjectLocationProto(type)
            Table dst(name)
            Table 4(memberID)
        }
    }
    Table (message)
    {
        Table t_TavernInfoProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table id(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table lastfreetime(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table needbuynum(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table todayfreetime(name)
            Table 4(memberID)
        }
    }
    Table (message)
    {
        Table t_TavernMainInfoCmdProto(messageName)
        Table (member)
        {
            Table repeated(condtion)
            Table t_TavernInfoProto(type)
            Table objs(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_RewardObjsProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table id(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table num(name)
            Table 2(memberID)
        }
    }
    Table (message)
    {
        Table t_TavernBuyObjectCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table id(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table repeated(condtion)
            Table t_RewardObjsProto(type)
            Table objs(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table needbuynum(name)
            Table 3(memberID)
        }
    }
    Table (message)
    {
        Table t_TavernStartBuyCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table id(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table isbuyten(name)
            Table 2(memberID)
        }
    }
    Table (message)
    {
        Table t_CombindObjCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table id(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table generalid(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table qwthisid(name)
            Table 3(memberID)
        }
    }
    Table (message)
    {
        Table t_CombineObjReturnCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table id(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table generalid(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table qwthisid(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table t_ObjectProto(type)
            Table oldobj(name)
            Table 4(memberID)
        }
    }
    Table (message)
    {
        Table t_PalaceGetRewardCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table todayget(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table returnresult(name)
            Table 2(memberID)
        }
    }
    Table (message)
    {
        Table t_PlayValueUpdayteCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table id(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table value(name)
            Table 2(memberID)
        }
    }
    Table (message)
    {
        Table t_IndivSortItemProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table oldplace(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table string(type)
            Table name(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table charid(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table sex(name)
            Table 4(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table job(name)
            Table 5(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table string(type)
            Table septname(name)
            Table 6(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table group(name)
            Table 7(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table vip(name)
            Table 8(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table level(name)
            Table 9(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table value(name)
            Table 10(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table value1(name)
            Table 11(memberID)
        }
    }
    Table (message)
    {
        Table t_SortListUserCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table type(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table page(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table totalpage(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table pkscore(name)
            Table 4(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table place(name)
            Table 5(memberID)
        }
        Table (member)
        {
            Table repeated(condtion)
            Table t_IndivSortItemProto(type)
            Table items(name)
            Table 6(memberID)
        }
    }
    Table (message)
    {
        Table t_RequestSortListUserCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table type(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table page(name)
            Table 2(memberID)
        }
    }
    Table (message)
    {
        Table t_SoldierUpStarCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table type(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table index(name)
            Table 2(memberID)
        }
    }
    Table (message)
    {
        Table t_GeneralAllEquipsCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table id(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table t_ObjectProto(type)
            Table equipone(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table t_ObjectProto(type)
            Table equiptwo(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table t_ObjectProto(type)
            Table equipthree(name)
            Table 4(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table t_ObjectProto(type)
            Table equipfour(name)
            Table 5(memberID)
        }
    }
    Table (message)
    {
        Table t_GeneralWearEquipCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table id(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table qwthisid(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table action(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table oldthisid(name)
            Table 4(memberID)
        }
    }
    Table (message)
    {
        Table t_rqGeneralUseExpBook(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table itemID(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table count(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table generalID(name)
            Table 3(memberID)
        }
    }
    Table (message)
    {
        Table t_rtGeneralUseExpBook(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table itemID(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table generalID(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table value(name)
            Table 3(memberID)
        }
    }
    Table (message)
    {
        Table t_rqEquipArmy(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table position(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table generalID(name)
            Table 2(memberID)
        }
    }
    Table (message)
    {
        Table t_rqGeneralUpStar(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table generalID(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_GenralArmyUpdegreeProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table generalID(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_PrimaryQuestStateProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table type(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table questid(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table string(type)
            Table content(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table count(name)
            Table 4(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table maxcount(name)
            Table 5(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table rewardgold(name)
            Table 6(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table rewardmoney(name)
            Table 7(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table t_RewardObjsProto(type)
            Table rewarditem(name)
            Table 8(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table state(name)
            Table 9(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table string(type)
            Table nametype(name)
            Table 10(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table icon(name)
            Table 11(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table toDoID(name)
            Table 12(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table toDoChildID(name)
            Table 13(memberID)
        }
    }
    Table (message)
    {
        Table t_PrimaryQuestInfoCmdProto(messageName)
        Table (member)
        {
            Table repeated(condtion)
            Table t_PrimaryQuestStateProto(type)
            Table questset(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_PrimaryQuestGetRewardCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table type(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table questid(name)
            Table 2(memberID)
        }
    }
    Table (message)
    {
        Table t_DailyQuestStateProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table questid(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table string(type)
            Table content(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table count(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table maxcount(name)
            Table 4(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table rewardhonor(name)
            Table 5(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table rewardmoney(name)
            Table 6(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table state(name)
            Table 7(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table string(type)
            Table nametype(name)
            Table 8(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table icon(name)
            Table 9(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table toDoID(name)
            Table 10(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table toDoChildID(name)
            Table 11(memberID)
        }
    }
    Table (message)
    {
        Table t_DailyQuestInfoCmdProto(messageName)
        Table (member)
        {
            Table repeated(condtion)
            Table t_DailyQuestStateProto(type)
            Table questset(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_DailyQuestGetRewardCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table questid(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_SoldierRenameCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table type(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table index(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table string(type)
            Table name(name)
            Table 3(memberID)
        }
    }
    Table (message)
    {
        Table t_CommonRewardCmdProto(messageName)
        Table (member)
        {
            Table repeated(condtion)
            Table t_RewardObjsProto(type)
            Table itemset(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_GeneralGetCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table generalid(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_SoldierGetCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table type(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table num(name)
            Table 2(memberID)
        }
    }
    Table (message)
    {
        Table t_DungeonChapterStateProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table id(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table percentstate(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table completecount(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table totalstar(name)
            Table 4(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table starstate(name)
            Table 5(memberID)
        }
    }
    Table (message)
    {
        Table t_DungeonChapterRewardCmdProto(messageName)
        Table (member)
        {
            Table repeated(condtion)
            Table t_DungeonChapterStateProto(type)
            Table chapterdata(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_DungeonGetChapterRwdCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table id(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table iselite(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table rewardindex(name)
            Table 3(memberID)
        }
    }
    Table (message)
    {
        Table t_GuideStepCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table id(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table canInterrupt(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table nextID(name)
            Table 3(memberID)
        }
    }
    Table (message)
    {
        Table t_GuideCompletedCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table id(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_DungeonStartAutoQuestProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table questid(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table autoquesttime(name)
            Table 2(memberID)
        }
    }
    Table (message)
    {
        Table t_OneAutoQuestRewardCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table time(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table money(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table repeated(condtion)
            Table t_ObjProto(type)
            Table itemset(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table exploit(name)
            Table 4(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table generalExp(name)
            Table 5(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table soldierExp(name)
            Table 6(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table captureid(name)
            Table 7(memberID)
        }
    }
    Table (message)
    {
        Table t_DungeonAutoQuestRewardProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table questid(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table repeated(condtion)
            Table t_OneAutoQuestRewardCmdProto(type)
            Table rewardset(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table repeated(condtion)
            Table t_ObjProto(type)
            Table extralitemset(name)
            Table 3(memberID)
        }
    }
    Table (message)
    {
        Table t_rqPassChapterReward(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table chapterID(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_rtPassChapterReward(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table chapterID(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table costEnergy(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table exp(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table repeated(condtion)
            Table t_ObjProto(type)
            Table items(name)
            Table 4(memberID)
        }
    }
    Table (message)
    {
        Table t_rqChapterRetreat(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table chapterID(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table reason(name)
            Table 2(memberID)
        }
    }
    Table (message)
    {
        Table t_rtChapterTotalReward(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table chapterID(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table exp(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table repeated(condtion)
            Table t_ObjProto(type)
            Table items(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table first(name)
            Table 4(memberID)
        }
    }
    Table (message)
    {
        Table t_rtFirstChargeReward(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table firstcharge(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_CommonReward(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table id(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table state(name)
            Table 2(memberID)
        }
    }
    Table (message)
    {
        Table t_rtConsumptionReward(messageName)
        Table (member)
        {
            Table repeated(condtion)
            Table t_CommonReward(type)
            Table data(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table totalgold(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table starttime(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table endtime(name)
            Table 4(memberID)
        }
    }
    Table (message)
    {
        Table t_rqGetConsumptionReward(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table id(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_CommonCountReward(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table id(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table gettimes(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table maxtimes(name)
            Table 3(memberID)
        }
    }
    Table (message)
    {
        Table t_rtSingleChargeReward(messageName)
        Table (member)
        {
            Table repeated(condtion)
            Table t_CommonCountReward(type)
            Table data(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_rqGetSingleChargeReward(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table id(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_rtAccuChargeReward(messageName)
        Table (member)
        {
            Table repeated(condtion)
            Table t_CommonReward(type)
            Table data(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table totalcharge(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table starttime(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table endtime(name)
            Table 4(memberID)
        }
    }
    Table (message)
    {
        Table t_rqGetAccuChargeReward(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table id(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_rtSevenDayChargeReward(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table state(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table totalcharge(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table day(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table starttime(name)
            Table 4(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table endtime(name)
            Table 5(memberID)
        }
    }
    Table (message)
    {
        Table t_rtSetTimeReward(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table state(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_SecertCourtRecord(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table index(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table itemid(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table itemnum(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table curprice(name)
            Table 4(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table oldprice(name)
            Table 5(memberID)
        }
    }
    Table (message)
    {
        Table t_HeroShopBatchCmdProto(messageName)
        Table (member)
        {
            Table repeated(condtion)
            Table t_SecertCourtRecord(type)
            Table data(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table needgold(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table buytimes(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table moneytype(name)
            Table 4(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table maxbuytimes(name)
            Table 5(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table leftrefreshtimes(name)
            Table 6(memberID)
        }
    }
    Table (message)
    {
        Table t_GroceryShopBathCmdProto(messageName)
        Table (member)
        {
            Table repeated(condtion)
            Table t_SecertCourtRecord(type)
            Table data(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table needgold(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table buytimes(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table moneytype(name)
            Table 4(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table maxbuytimes(name)
            Table 5(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table leftrefreshtimes(name)
            Table 6(memberID)
        }
    }
    Table (message)
    {
        Table t_ArmyShopBathCmdProto(messageName)
        Table (member)
        {
            Table repeated(condtion)
            Table t_SecertCourtRecord(type)
            Table data(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table needgold(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table buytimes(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table moneytype(name)
            Table 4(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table maxbuytimes(name)
            Table 5(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table leftrefreshtimes(name)
            Table 6(memberID)
        }
    }
    Table (message)
    {
        Table t_BuyShopCmdProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table index(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_ArenaPlayer(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table charid(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table rank(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table lv(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table reward(name)
            Table 4(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table power(name)
            Table 5(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table string(type)
            Table name(name)
            Table 6(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table state(name)
            Table 7(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table string(type)
            Table word(name)
            Table 8(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table card(name)
            Table 9(memberID)
        }
        Table (member)
        {
            Table repeated(condtion)
            Table t_PetGeneralStructProto(type)
            Table generals(name)
            Table 10(memberID)
        }
    }
    Table (message)
    {
        Table t_ArenaInfoProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table rank(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table string(type)
            Table word(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table score(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table currency(name)
            Table 4(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table leftTimes(name)
            Table 5(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table maxTimes(name)
            Table 6(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table getRewardLeftTime(name)
            Table 7(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table addTimeGold(name)
            Table 8(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table buyTimes(name)
            Table 9(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table maxBuyTimes(name)
            Table 10(memberID)
        }
        Table (member)
        {
            Table repeated(condtion)
            Table uint32(type)
            Table generals(name)
            Table 11(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table worshiptime(name)
            Table 12(memberID)
        }
    }
    Table (message)
    {
        Table t_ArenaOpponentsProto(messageName)
        Table (member)
        {
            Table repeated(condtion)
            Table t_ArenaPlayer(type)
            Table data(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_OperatorArenaPlayerProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table rank(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table charid(name)
            Table 2(memberID)
        }
    }
    Table (message)
    {
        Table t_ArenaRankingRecord(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table rank(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table charid(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table string(type)
            Table name(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table lv(name)
            Table 4(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table power(name)
            Table 5(memberID)
        }
    }
    Table (message)
    {
        Table t_ArenaRankingProto(messageName)
        Table (member)
        {
            Table repeated(condtion)
            Table t_ArenaRankingRecord(type)
            Table data(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_ArenaFightRecord(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table charid(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table string(type)
            Table name(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table lv(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table action(name)
            Table 4(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table delta(name)
            Table 5(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table time(name)
            Table 6(memberID)
        }
    }
    Table (message)
    {
        Table t_ArenaFightTidingsProto(messageName)
        Table (member)
        {
            Table repeated(condtion)
            Table t_ArenaFightRecord(type)
            Table data(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_AreaShopBathCmdProto(messageName)
        Table (member)
        {
            Table repeated(condtion)
            Table t_SecertCourtRecord(type)
            Table data(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table needgold(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table buytimes(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table moneytype(name)
            Table 4(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table maxbuytimes(name)
            Table 5(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table leftrefreshtimes(name)
            Table 6(memberID)
        }
    }
    Table (message)
    {
        Table t_ArenaRankingRewardsPtoro(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table leagueCurrency(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table leagueMoney(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table count(name)
            Table 3(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table maxcount(name)
            Table 4(memberID)
        }
    }
    Table (message)
    {
        Table t_AreaScoreRewards(messageName)
        Table (member)
        {
            Table repeated(condtion)
            Table t_CommonReward(type)
            Table data(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_rqGetArenaScoreReward(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table id(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_ArenaAlterWordProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table string(type)
            Table word(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_ArenaGeneralSettingProto(messageName)
        Table (member)
        {
            Table repeated(condtion)
            Table uint32(type)
            Table generals(name)
            Table 1(memberID)
        }
    }
    Table (message)
    {
        Table t_ChallengeArenaPlayerProto(messageName)
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table rank(name)
            Table 1(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table charid(name)
            Table 2(memberID)
        }
        Table (member)
        {
            Table optional(condtion)
            Table uint32(type)
            Table result(name)
            Table 3(memberID)
        }
    }
}
