using System;
using System.Collections.Generic;

namespace I_EARTH.Models
{
    public class PartyMember
    {
        public string? ID { get; set; }
        public int Power { get; set; }
    }

    public class RaidingPostModels
    {
        public string? RaidingName { get; set; }
        public int? PowerLevel { get; set; }
        public int? PartyMaxSize { get; set; }
        public string? Description { get; set; }
        public string? LeaderIP { get; set; }
        public List<PartyMember> PartyList { get; set; }
        public string? RaidingTime { get; set; }

        public RaidingPostModels()
        {
            PartyList = new List<PartyMember>();
        }

        public string AddPartyList(PartyMember newPartyMember)
        {
            if (PartyList.Count < PartyMaxSize)
            {
                PartyList.Add(newPartyMember);
                return "AddPartySuccess";
            }
            else
            {
                return "AddPartyFailure";
            }
        }

        public int ShowPartyPower()
        {
            int totalPower = 0;
            foreach (var partyMember in PartyList)
            {
                totalPower += partyMember.Power;
            }
            return totalPower;
        }

        public string KickPartyList(string kickindID)
        {
            PartyMember memberToRemove = PartyList.Find(member => member.ID == kickindID);
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
