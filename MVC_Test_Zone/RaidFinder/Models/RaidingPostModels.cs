using System;
using System.Collections.Generic;

namespace RaidFinder.Models
{

    public class RaidingPostModels
    {
        public string? Name { get; set; }
        public int? PowerLevel { get; set; }
        public int? MaxSize { get; set; } = 5;
        public string? Description { get; set; }
        public string? OwnerId { get; set; }
        public List<User> PartyList { get; set; }
        public string? TimeOut { get; set; }

        public RaidingPostModels()
        {
            PartyList = new List<User>();
        }

        public void AddPartyList(User newPartyMember)
        {
            if (PartyList.Count < MaxSize)
            {
                PartyList.Add(newPartyMember);
            }
        }

        public int ShowPartyPower()
        {
            int totalPower = 0;
            foreach (var partyMember in PartyList)
            {
                totalPower += partyMember.Stat.PowerLevel;
            }
            return totalPower;
        }

        public string KickPartyList(int kickindID)
        {
            User memberToRemove = PartyList.Find(member => member.UserId == kickindID);
            if (memberToRemove != null)
            {
                PartyList.Remove(memberToRemove);
                return "PartyMemberKicked";
            }
            else
            {
                return "PartyMemberNotExists";
            }
        }

        public string UpdatePartyPower()
        {
            PowerLevel = ShowPartyPower();
            return "UpdatePartyPowerSuccess";
        }
    }
}
