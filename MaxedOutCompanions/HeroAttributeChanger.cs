using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace MaxedOutCompanions
{
    class HeroAttributeChanger : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            bool addAllPerks = ConfigReader.GetInstance().Get("AddAllPerks") == "YES" ? true : false;
            int skillCap = int.Parse(ConfigReader.GetInstance().Get("SetSkillToValue"));
            int[] attributeCaps = new int[6];
            attributeCaps[0] = int.Parse(ConfigReader.GetInstance().Get("AttributeControl"));
            attributeCaps[1] = int.Parse(ConfigReader.GetInstance().Get("AttributeCunning"));
            attributeCaps[2] = int.Parse(ConfigReader.GetInstance().Get("AttributeEndurance"));
            attributeCaps[3] = int.Parse(ConfigReader.GetInstance().Get("AttributeIntelligence"));
            attributeCaps[4] = int.Parse(ConfigReader.GetInstance().Get("AttributeSocial"));
            attributeCaps[5] = int.Parse(ConfigReader.GetInstance().Get("AttributeVigor"));
            CampaignEvents.NewCompanionAdded.AddNonSerializedListener(this, new Action<Hero>(
                hero =>
                {
                    MaxOutCompanion(hero, attributeCaps, skillCap, addAllPerks);
                }
            ));
        }

        private void MaxOutCompanion(Hero hero, int[] attributes, int skillCap, bool addPerks)
        {
            hero.SetAttributeValue(CharacterAttributesEnum.Control, attributes[0]);
            hero.SetAttributeValue(CharacterAttributesEnum.Cunning, attributes[1]);
            hero.SetAttributeValue(CharacterAttributesEnum.Endurance, attributes[2]);
            hero.SetAttributeValue(CharacterAttributesEnum.Intelligence, attributes[3]);
            hero.SetAttributeValue(CharacterAttributesEnum.Social, attributes[4]);
            hero.SetAttributeValue(CharacterAttributesEnum.Vigor, attributes[5]);

            foreach (var skill in SkillObject.All)
            {
                hero.SetSkillValue(skill, skillCap);
            }
            if (addPerks)
            {
                foreach (var perk in PerkObject.All)
                {
                    hero.SetPerkValue(perk, true);
                }
            }
        }

        public override void SyncData(IDataStore dataStore)
        {

        }
    }
}
