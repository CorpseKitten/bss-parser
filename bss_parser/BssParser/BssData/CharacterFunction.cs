using System.Collections.Generic;
using System.IO;
using System.Linq;
using BssParser.Utils;

namespace BssParser.BssData
{
    [BssTable("characterfunction")]
    public class CharacterFunction : IBssEntry
    {
        public ushort index { get; set; }
        public byte shopType { get; set; }
        public byte tradeGroup { get; set; }
        public string buyingFromNpcName { get; set; }
        public string conditionForBuyingFromNpc { get; set; }
        public ushort itemMainGroupKeyForBuyingFromNpc { get; set; }
// sell - confirmed
        public string sellingToNpcName { get; set; }
        public string conditionForSellingToNpc { get; set; }
        public ushort itemMainGroupKeyForSellingToNpc { get; set; }
// guild buy - confirmed
        public string buyingFromGuildShopNpcName { get; set; }
        public string conditionForBuyingFromGuildShopNpc { get; set; }
        public ushort itemMainGroupKeyForBuyingFromNpc2 { get; set; }
// guild sell - confirmed
        public string SellingToGuildShopNpcName { get; set; }
        public string conditionForSellingToGuildShopNpc { get; set; }
        public ushort itemMainGroupKeyForSellingToNpc2 { get; set; }
// trade - confirmed
        public string tradingName { get; set; }
        public string conditionForTrading { get; set; }
        public ushort itemMainGroupKeyForTrading { get; set; }
// auction - confirmed
        public string auctionName { get; set; }
        public string conditionForAuction { get; set; }
        public short auctionKey { get; set; }
// mating - confirmed
        public string matingName { get; set; }
        public string conditionForMating { get; set; }
        public short matingKey { get; set; }
// inn
        public string innName { get; set; }
        public string conditionForInn { get; set; }
        public byte innKey { get; set; }
// explore
        public string exploreKeyName { get; set; }
        public string conditionRewardExplore { get; set; }
        public byte rewardExploreKey { get; set; }
// skill - confirmed
        public long skillNameLength { get; set; }
        public string skillName { get; set; }
        public string conditionTrainSkill { get; set; }
        public byte isTrainSkill { get; set; }
// repair - confirmed
        public string repairName { get; set; }
        public string conditionRepair { get; set; }
        public byte isRepair { get; set; }
// warehouse - confirmed
        public string warehouseName { get; set; }
        public string conditionWarehouse { get; set; }
        public byte isWarehouse { get; set; }
// stable - confirmed
        public long stableNameLength { get; set; }
        public string stableName { get; set; }
        public string conditionStable { get; set; }
        public byte stableType { get; set; }
        public int stableKeySize { get; set; }
        public byte[] stableKey { get; set; }
// transfer - confirmed
        public string transferName { get; set; }
        public string conditionTransfer { get; set; }
        public byte isTransfer { get; set; }
// transfer person - confirmed
        public string transferPersonName { get; set; }
        public string conditionTransferPerson { get; set; }
        public byte isTransferPerson { get; set; }
// intimacy - confirmed
        public long intimacyNameLength { get; set; }
        public string intimacyName { get; set; }
        public string conditionIntimacy { get; set; }
        public byte isIntimacy { get; set; }
        public byte isGreatNpc { get; set; }
// guild - confirmed
        public long guildNameLength { get; set; }
        public string guildName { get; set; }
        public string conditionGuild { get; set; }
        public byte isGuild { get; set; }
// guide explore
        public long exploreNameLength { get; set; }
        public string exploreName { get; set; }
        public string conditionExplore { get; set; }
        public int exploreWayPointCount { get; set; }
        public List<int> exploreWayPoint { get; set; }
        public int guideWaypoint { get; set; }
// lord menu - confirmed
        public string lordMenuName { get; set; }
        public string conditionLordMenu { get; set; }
        public byte isLordMenu { get; set; }
// minor lord menu - confirmed
        public string minorLordMenuName { get; set; }
        public string conditionMinorLordMenu { get; set; }
        public short isMinorLordMenu { get; set; }
// extract - confirmed
        public string extractName { get; set; }
        public string conditionExtract { get; set; }
        public byte isExtract { get; set; }
// territory supply - confirmed
        public string territorySupplyName { get; set; }
        public string conditionForTerritorySupply { get; set; }
        public short territoryKeyForTerritorySupply { get; set; }
// market - confirmed
        public string itemMarketName { get; set; }
        public string conditionForItemMarket { get; set; }
        public short territoryKeyForItemMarket { get; set; }
// knowledge - confirmed
        public string knowledgeName { get; set; }
        public string conditionForKnowledge { get; set; }
        public byte isKnowledgeManagement { get; set; }
// supply shop - confirmed
        public string supplyShopName { get; set; }
        public string conditionForSupplyShop { get; set; }
        public byte supplyShopKey { get; set; }
// fish supply shop - confirmed
        public string fishSupplyShopName { get; set; }
        public string conditionForFishSupplyShop { get; set; }
        public byte fishSupplyShopKey { get; set; }
// guild supply shop - confirmed
        public string guildSupplyShopName { get; set; }
        public string conditionForGuildSupplyShop { get; set; }
        public byte guildSupplyShopKey { get; set; }
// awaken skill
        public string awakenSkillName { get; set; }
        public string conditionForAwakenSkill { get; set; }
        public byte isAwakenSkill { get; set; }
        public byte isStallionSkillExpTraining { get; set; }

        public IBssEntry Read(BinaryReader br)
        {
            var func = new CharacterFunction();
            func.index = br.ReadUInt16();
            func.shopType = br.ReadByte();
            func.tradeGroup = br.ReadByte();
            func.tradeGroup = br.ReadByte();
            br.ReadByte();
            br.ReadByte();
	        // buy - confirmed
            func.buyingFromNpcName = br.ReadUnicodeStringBD();
            func.conditionForBuyingFromNpc = br.ReadUnicodeStringBD();
            func.itemMainGroupKeyForBuyingFromNpc = br.ReadUInt16();// todo fixme
	        // sell - confirmed
            func.sellingToNpcName = br.ReadUnicodeStringBD();
            func.conditionForSellingToNpc = br.ReadUnicodeStringBD();
            func.itemMainGroupKeyForSellingToNpc = br.ReadUInt16();// todo fixme
	        // guild buy - confirmed
            func.buyingFromGuildShopNpcName = br.ReadUnicodeStringBD();
            func.conditionForBuyingFromGuildShopNpc = br.ReadUnicodeStringBD();
            func.itemMainGroupKeyForSellingToNpc2 = br.ReadUInt16();// todo fixme
	        // guild sell - confirmed
            func.sellingToNpcName = br.ReadUnicodeStringBD();
            func.conditionForSellingToGuildShopNpc = br.ReadUnicodeStringBD();
            func.itemMainGroupKeyForSellingToNpc2 = br.ReadUInt16(); // todo fixme
	        // trade - confirmed
            func.tradingName = br.ReadUnicodeStringBD();
            func.conditionForTrading = br.ReadUnicodeStringBD();
            func.itemMainGroupKeyForTrading = br.ReadUInt16();
	        // auction - confirmed
            func.auctionName = br.ReadUnicodeStringBD();
            func.conditionForAuction = br.ReadUnicodeStringBD();
            func.auctionKey = br.ReadInt16();
	        // mating - confirmed
            func.matingName = br.ReadUnicodeStringBD();
            func.conditionForMating = br.ReadUnicodeStringBD();
            func.matingKey = br.ReadInt16();
	        // inn
            func.innName = br.ReadUnicodeStringBD();
            func.conditionForInn = br.ReadUnicodeStringBD();
            func.innKey = br.ReadByte();
	        // explore
            func.exploreKeyName = br.ReadUnicodeStringBD();
            func.conditionRewardExplore = br.ReadUnicodeStringBD();
            func.rewardExploreKey = br.ReadByte();
	        // skill - confirmed
            func.skillName = br.ReadUnicodeStringBD();
            func.conditionForTrading = br.ReadUnicodeStringBD();
            func.isTrainSkill = br.ReadByte();
	        // repair - confirmed
            func.repairName = br.ReadUnicodeStringBD();
            func.conditionRepair = br.ReadUnicodeStringBD();
            func.isRepair = br.ReadByte();
	        // warehouse - confirmed
            func.warehouseName = br.ReadUnicodeStringBD();
            func.conditionWarehouse = br.ReadUnicodeStringBD();
            func.isWarehouse = br.ReadByte();
	        // stable - confirmed
            func.stableName = br.ReadUnicodeStringBD();
            func.conditionStable = br.ReadUnicodeStringBD();
            func.stableType = br.ReadByte();
            func.stableKey = br.ReadBytes(func.stableType);
	        // transfer - confirmed
            func.transferName = br.ReadUnicodeStringBD();
            func.conditionTransfer = br.ReadUnicodeStringBD();
            func.isTransfer = br.ReadByte();
	        // transfer person - confirmed
            func.transferPersonName = br.ReadUnicodeStringBD();
            func.conditionTransferPerson = br.ReadUnicodeStringBD();
            func.isTransferPerson = br.ReadByte();
	        // intimacy - confirmed
            func.intimacyName = br.ReadUnicodeStringBD();
            func.conditionIntimacy = br.ReadUnicodeStringBD();
            func.isIntimacy = br.ReadByte();
            func.isGreatNpc = br.ReadByte();
	        // guild - confirmed
            func.guildName = br.ReadUnicodeStringBD();
            func.conditionGuild = br.ReadUnicodeStringBD();
            func.isGuild = br.ReadByte();
	        // guide explore
            func.exploreName = br.ReadUnicodeStringBD();
            func.conditionExplore = br.ReadUnicodeStringBD();
            func.exploreWayPointCount = br.ReadInt32();
            func.exploreWayPoint = Enumerable.Range(0, func.exploreWayPointCount).Select(x => br.ReadInt32()).ToList();
            func.guideWaypoint = br.ReadInt32();
            if (func.guideWaypoint > 0)
                br.ReadInt32();
	        // lord menu - confirmed
            func.lordMenuName = br.ReadUnicodeStringBD();
            func.conditionLordMenu = br.ReadUnicodeStringBD();
            func.isLordMenu = br.ReadByte();
	        // minor lord menu - confirmed
            func.minorLordMenuName = br.ReadUnicodeStringBD();
            func.conditionMinorLordMenu = br.ReadUnicodeStringBD();
            func.isMinorLordMenu = br.ReadInt16();
	        // extract - confirmed
            func.extractName = br.ReadUnicodeStringBD();
            func.conditionExtract = br.ReadUnicodeStringBD();
            func.isExtract = br.ReadByte();
            br.ReadBytes(18);
	        // territory supply - confirmed
            func.territorySupplyName = br.ReadUnicodeStringBD();
            func.conditionForTerritorySupply = br.ReadUnicodeStringBD();
            func.territoryKeyForTerritorySupply = br.ReadInt16();
	        // market - confirmed
            func.itemMarketName = br.ReadUnicodeStringBD();
            func.conditionForItemMarket = br.ReadUnicodeStringBD();
            func.territoryKeyForItemMarket = br.ReadInt16();
	        // knowledge - confirmed
            func.knowledgeName = br.ReadUnicodeStringBD();
            func.conditionForKnowledge = br.ReadUnicodeStringBD();
            func.isKnowledgeManagement = br.ReadByte();
	        // supply shop - confirmed
            func.supplyShopName = br.ReadUnicodeStringBD();
            func.conditionForSupplyShop = br.ReadUnicodeStringBD();
            func.supplyShopKey = br.ReadByte();
	        // fish supply shop - confirmed
            func.fishSupplyShopName = br.ReadUnicodeStringBD();
            func.conditionForFishSupplyShop = br.ReadUnicodeStringBD();
            func.fishSupplyShopKey = br.ReadByte();
	        // guild supply shop - confirmed
            func.guildSupplyShopName = br.ReadUnicodeStringBD();
            func.conditionForGuildSupplyShop = br.ReadUnicodeStringBD();
            func.guildSupplyShopKey = br.ReadByte();
	        // awaken skill
            func.awakenSkillName = br.ReadUnicodeStringBD();
            func.conditionForAwakenSkill = br.ReadUnicodeStringBD();
            func.isAwakenSkill = br.ReadByte();
            func.isStallionSkillExpTraining = br.ReadByte();
            return func;
        }

        public void ReadTable(BinaryReader br)
        {
            //throw new System.NotImplementedException();
        }
    }
}