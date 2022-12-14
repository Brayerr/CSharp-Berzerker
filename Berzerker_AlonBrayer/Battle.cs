using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berzerker_AlonBrayer
{
    public class Battle
    {
        List<Unit> _army1;
        List<Unit> _army2;
        Random rand = new();
        Dice d = new();
        public bool player1Win { get; protected set; } = false;
        public bool player2Win { get; protected set; } = false;


        public Battle(List<Unit> army1, List<Unit> army2)
        {
            _army1 = army1;
            _army2 = army2;
        }

        public void BattleLoop(List<Unit> army1, List<Unit> army2)
        {
            while(army1.Count > 0 && army2.Count > 0)
            {
                int unitChosen1 = rand.Next(army1.Count);
                int unitChosen2 = rand.Next(army2.Count);
                string effectChosen;
                Console.WriteLine("new round");
                effectChosen =  d.WeatherEffectDistinguisher(WeatherEffectChooser());
                army1.ElementAt(unitChosen1).WeatherEffectToggle(effectChosen, army1.ElementAt(unitChosen1), army2.ElementAt(unitChosen2), true);
                Console.WriteLine("This round's weather effect is "+ effectChosen);
                Console.WriteLine("AB's turn");

                switch (army1.ElementAt(unitChosen1).Role)
                {
                    case "Ranged":
                    case "Melee":
                        {
                            army1.ElementAt(unitChosen1).Attack(army2.ElementAt(unitChosen2));
                            break;
                        }

                    case "Support":
                        {
                            int unitChosenToHeal1 = rand.Next(army1.Count);

                            army1.ElementAt(unitChosen1).Attack(army1.ElementAt(unitChosenToHeal1));
                            break;
                        }
                        
                    default:
                        break;
                }

                Console.WriteLine();
                Console.WriteLine("DBD's turn");

                switch (army2.ElementAt(unitChosen2).Role)
                {
                    case "Ranged":
                    case "Melee":
                        {
                            army2.ElementAt(unitChosen2).Attack(army1.ElementAt(unitChosen1));
                            break;
                        }

                    case "Support":
                        {
                            int unitChosenToHeal2 = rand.Next(army2.Count);

                            army2.ElementAt(unitChosen2).Attack(army2.ElementAt(unitChosenToHeal2));
                            break;
                        }

                    default:
                        break;
                }

                army1.ElementAt(unitChosen1).WeatherEffectToggle(effectChosen, army1.ElementAt(unitChosen1), army2.ElementAt(unitChosen2), false);
                Console.WriteLine("round over");

                CheckForDeadUnits(army1);

                CheckForDeadUnits(army2);

                if(CheckForDeadArmies(army1))
                {
                    Console.WriteLine("AB's army is all dead");
                    player2Win = true;
                }

                if(CheckForDeadArmies(army2))
                {
                    Console.WriteLine("DBD's army is all dead");
                    player1Win = true;
                }

                Console.WriteLine("");

            }
        }

        public void CheckForDeadUnits(List<Unit> army)
        {
            for (int i = 0; i < army.Count; i++)
            {
                if (army.ElementAt(i).HP <= 0)
                {
                    Console.WriteLine(army.ElementAt(i) + " died and was removed from the army.");
                    army.Remove(army.ElementAt(i));
                }
            }
        }

        public bool CheckForDeadArmies(List<Unit> army)
        {
            if(army.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        int WeatherEffectChooser()
        {
            if (rand.NextDouble() > .7f)
            {
                return rand.Next(1, 4);
            }
            else
            {
                return 0;
            }
        }
    }
    
}
