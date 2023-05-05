using System;

namespace Tonbale
{
    enum Race
    {
        human,
        elf,
        orc,
        goblin,
        dwarf
    }

    enum Faction
    {
        independant,
        royal,
        military
    }

    public class Character
    {
        string name;
        Race race;
        Faction faction;

        public void MoveCharacter()
        {

        }
    }
}
