using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berzerker_AlonBrayer
{
    public class Player
    {
        public string Name { get; protected set; }
        public int Race { get; protected set; }
        public int CarryingCapacity { get; protected set; } = 10;

        Random rand = new Random();
        Loot loot = new Loot();

        public List<Unit> army = new List<Unit>();
        public List<string> inventory = new List<string>();
        public Player(string name, int race)
        {
            Name = name;
            Race = race;
        }

        int RandomNumProvider()
        {
            return rand.Next(1, 6);
        }

        //since having more then 1 healer in a list makes the fight much much longer ive made it harder to get healers.
        public int RandomUnitChooser()
        {
            if (rand.NextDouble() > .1f)
            {
                return rand.Next(1, 4);
            }
            else
            {
                return rand.Next(1, 3);
            }
        }

        public void InstantiateNewUnit(int race, List<Unit> list)
        {

            switch (race)
            {
                case 1:
                    {
                        switch (RandomUnitChooser())
                        {
                            case 1:
                                {
                                    BowMan bowMan = new("Bowman");
                                    list.Add(bowMan);
                                    break;
                                }
                            case 2:
                                {
                                    SwordMan swordMan = new("Swordman");
                                    list.Add(swordMan);
                                    break;
                                }
                            case 3:
                                {
                                    Bishop bishop = new("Bishop");
                                    list.Add(bishop);
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }
                case 2:
                    {
                        switch (RandomUnitChooser())
                        {
                            case 1:
                                {
                                    ArchNinja archNinja = new("Archninja");
                                    list.Add(archNinja);
                                    break;
                                }
                            case 2:
                                {
                                    DarkKnight darkKnight = new("Darkknight");
                                    list.Add(darkKnight);
                                    break;
                                }
                            case 3:
                                {
                                    ArchBishop archBishop = new("Archbishop");
                                    list.Add(archBishop);
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }
                case 3:
                    {

                        switch (RandomUnitChooser())
                        {
                            case 1:
                                {
                                    Cannoneer cannoneer = new("Canoneer");
                                    list.Add(cannoneer);
                                    break;
                                }
                            case 2:
                                {
                                    DualBlader dualBlader = new("Dualblader");
                                    list.Add(dualBlader);
                                    break;
                                }
                            case 3:
                                {
                                    Doctor doctor = new("Doctor");
                                    list.Add(doctor);
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }
                default:
                    break;
            }

        }

        public void InstantiateNewInventory(List<string> inventory)
        {
            inventory.Capacity = CarryingCapacity;
            switch (RandomNumProvider())
            {
                case 1:

                    inventory.Add(loot.Stone);
                    break;

                case 2:

                    inventory.Add(loot.Wood);
                    break;

                case 3:

                    inventory.Add(loot.Gem);
                    break;

                case 4:

                    inventory.Add(loot.Gold);
                    break;

                case 5:

                    inventory.Add(loot.Potion);
                    break;

                default:
                    break;
            }
        }
    }
}
