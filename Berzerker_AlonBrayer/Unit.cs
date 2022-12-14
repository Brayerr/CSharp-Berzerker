//---- C# II (Dor Ben Dor) ----
//        Alon Brayer
//-----------------------------
using Berzerker_AlonBrayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berzerker_AlonBrayer
{
    //base class Unit
    public abstract class Unit
    {
        public virtual string Name { get; protected set; }
        public abstract string Role { get; protected set; }
        public virtual int HP { get; protected set; } = 20;
        public abstract IRandomProvider Damage { get; protected set; }
        public abstract IRandomProvider HitChance { get; protected set; }
        public abstract IRandomProvider DefenseRating { get; protected set; }
        uint scalarHolder;

        public Random rand = new();

        public abstract void Attack(Unit defender);

        public abstract bool Defend(Unit attacker);

        public enum Race
        {
            Elf,
            Ogre,
            Dwarf
        }

        public enum Weather
        {
            Sunny,
            Rainy,
            Snowy,
        }

        public void WeatherEffectToggle(string weather, Unit unit1, Unit unit2, bool onOrOff)
        {
            if (onOrOff)
            {
                switch (weather)
                {
                    case "Sunny":
                        {
                            if(unit1.Damage is Dice)
                                unit1.Damage = unit1.SunnyWeatherEffectOn(((Dice)unit1.Damage).Scalar, ((Dice)unit1.Damage).BaseDie, ((Dice)unit1.Damage).Modifier);                            
                            
                            if(unit2.Damage is Dice)                            
                                unit2.Damage = unit2.SunnyWeatherEffectOn(((Dice)unit2.Damage).Scalar, ((Dice)unit2.Damage).BaseDie, ((Dice)unit2.Damage).Modifier);
                            
                            break;
                        }
                    case "Rainy":
                        {
                            if(unit1.Damage is Dice)
                                unit1.Damage = unit1.RainyWeatherEffectOn(((Dice)unit1.Damage).Scalar, ((Dice)unit1.Damage).BaseDie, ((Dice)unit1.Damage).Modifier);
                            
                            if(unit2.Damage is Dice)
                                unit2.Damage = unit2.RainyWeatherEffectOn(((Dice)unit2.Damage).Scalar, ((Dice)unit2.Damage).BaseDie, ((Dice)unit2.Damage).Modifier);
                            break;
                        }
                    case "Snowy":
                        {
                            if(unit1.Damage is Dice)
                                unit1.Damage = unit1.SnowyWeatherEffectOn(((Dice)unit1.Damage).Scalar, ((Dice)unit1.Damage).BaseDie, ((Dice)unit1.Damage).Modifier);
                            
                            if(unit2.Damage is Dice)
                                unit2.Damage = unit2.SnowyWeatherEffectOn(((Dice)unit2.Damage).Scalar, ((Dice)unit2.Damage).BaseDie, ((Dice)unit2.Damage).Modifier);
                            break;
                        }
                    case "Default":
                        {
                            break;
                        }
                    default:
                        break;
                }
            }
            else
            {
                switch (weather)
                {
                    case "Sunny":
                        {
                            if(unit1.Damage is Dice)
                                unit1.Damage = unit1.SunnyWeatherEffectOff(((Dice)unit1.Damage).Scalar, ((Dice)unit1.Damage).BaseDie, ((Dice)unit1.Damage).Modifier);
                            
                            if(unit2.Damage is Dice)
                                unit2.Damage = unit2.SunnyWeatherEffectOff(((Dice)unit2.Damage).Scalar, ((Dice)unit2.Damage).BaseDie, ((Dice)unit2.Damage).Modifier);
                            break;
                        }
                    case "Rainy":
                        {
                            if(unit1.Damage is Dice)
                                unit1.Damage = unit1.RainyWeatherEffectOff(((Dice)unit1.Damage).Scalar, ((Dice)unit1.Damage).BaseDie, ((Dice)unit1.Damage).Modifier);

                            if(unit2.Damage is Dice)
                                unit2.Damage = unit2.RainyWeatherEffectOff(((Dice)unit2.Damage).Scalar, ((Dice)unit2.Damage).BaseDie, ((Dice)unit2.Damage).Modifier);
                            break;
                        }
                    case "Snowy":
                        {
                            if(unit1.Damage is Dice)
                                unit1.Damage = unit1.SnowyWeatherEffectOff(((Dice)unit1.Damage).Scalar, ((Dice)unit1.Damage).BaseDie, ((Dice)unit1.Damage).Modifier);

                            if(unit2.Damage is Dice)
                                unit2.Damage = unit2.SnowyWeatherEffectOff(((Dice)unit2.Damage).Scalar, ((Dice)unit2.Damage).BaseDie, ((Dice)unit2.Damage).Modifier);
                            break;
                        }
                    case "Default":
                        {
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        private Dice SunnyWeatherEffectOn(uint scalar, uint baseDie, int modifier)
        {
            return new Dice(scalar + 1, baseDie, modifier);
        }

        private Dice SunnyWeatherEffectOff(uint scalar, uint baseDie, int modifier)
        {
            return new Dice(scalar - 1, baseDie, modifier);
        }

        private Dice RainyWeatherEffectOn(uint scalar, uint baseDie, int modifier)
        {
            return new Dice(scalar, baseDie - 2, modifier);
        }

        private Dice RainyWeatherEffectOff(uint scalar, uint baseDie, int modifier)
        {
            return new Dice(scalar, baseDie + 2, modifier);
        }

        private Dice SnowyWeatherEffectOn(uint scalar, uint baseDie, int modifier)
        {
            scalarHolder = scalar;
            return new Dice(scalar = 0, baseDie, modifier);
        }
        
        private Dice SnowyWeatherEffectOff(uint scalar, uint baseDie, int modifier)
        {
            return new Dice(scalar = scalarHolder, baseDie, modifier);

        }

        public virtual void TakeDamage(int Damage)
        {
            if (HP > 0)
            {
                HP -= Damage;
            }
            else
            {
                Console.WriteLine("Unit cannot take anymore damage, he ded.");
            }
        }

        public virtual void TakeHeal(int healAmount)
        {
            HP += healAmount;
            Console.WriteLine(Name + " was successfully healed for " + healAmount + " HP!");            
        }
    }


    //derived classes sorted to 3 branches (ranged, melee, support)
    public class RangedUnit : Unit
    {
        public override IRandomProvider Damage { get; protected set; } = new Dice(1, 6, +1);
        public override int HP { get; protected set; } = 15;
        public override string Role { get; protected set; } = "Ranged";
        public override IRandomProvider HitChance { get; protected set; } = new Dice(1, 6, 0);
        public override IRandomProvider DefenseRating { get; protected set; } = new Dice(1, 8, 0);



        public override bool Defend(Unit attacker)
        {
            if (DefenseRating.Provide() > 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Attack(Unit defender)
        {
            int count = 0;
            while (count < 2)
            {
                if (HitChance.Provide() > 1)
                {
                    if (defender.Defend(this))
                    {
                        defender.TakeDamage((Damage.Provide()));
                        Console.WriteLine(Name + " successfully hit " + defender.Name + " for " + (Damage.Provide()) + " damage.");

                    }
                    else
                    {
                        Console.WriteLine(defender.Name + " successfully defended " + this.Name + " attack!");
                    }
                }
                else
                {
                    Console.WriteLine(Name + " missed his shot, what a newb.");
                }
                count++;
            }
        }
    }

    public class MeleeUnit : Unit
    {
        public override IRandomProvider Damage { get; protected set; } = new Dice(2, 6, 0);
        public override int HP { get; protected set; } = 20;
        public override IRandomProvider HitChance { get; protected set; } = new Dice(1, 6, 0);
        public override IRandomProvider DefenseRating { get; protected set; } = new Dice(1, 8, 0);



        public override string Role { get; protected set; } = "Melee";

        public override void Attack(Unit defender)
        {
            if (HitChance.Provide() > 1)
            {
                if (!defender.Defend(this))
                {
                    defender.TakeDamage((Damage.Provide()));
                    Console.WriteLine(Name + " successfully attacked " + defender.Name + " for " + (Damage.Provide()) + " damage.");
                }
                else
                {
                    Console.WriteLine(defender.Name + " successfully defended " + Name + "'s attack!");
                }
            }
            else
            {
                Console.WriteLine(Name + " missed his swing, HOW?");
            }
        }

        public override bool Defend(Unit attacker)
        {
            if (DefenseRating.Provide() > 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class SupportUnit : Unit
    {


        public override int HP { get; protected set; } = 30;
        public override IRandomProvider Damage { get; protected set; } = new Bag(new int[] {4, 6, 8, 10});
        public override string Role { get; protected set; } = "Support";
        public override IRandomProvider HitChance { get; protected set; } = new Dice(1, 6, 0);
        public override IRandomProvider DefenseRating { get; protected set; } = new Dice(1, 8, 0);


        public override void Attack(Unit defender)
        {
            if (HitChance.Provide() > 3)
            {
                defender.TakeHeal(Damage.Provide());
            }
            else
            {
                Console.WriteLine(Name + "'s heal was unsuccesfull");
            }
        }

        public override bool Defend(Unit attacker)
        {
            if (DefenseRating.Provide() > 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }



    //base class projectile
    public abstract class Projectile
    {
        public abstract int AddedDamage { get; protected set; }
    }


    //3 ranged weapons that inherit from projectile class
    public class Arrow : Projectile
    {
        public override int AddedDamage { get; protected set; } = 0;
    }

    public class CannonBall : Projectile
    {
        public override int AddedDamage { get; protected set; } = 2;
    }

    public class Shuriken : Projectile
    {
        public override int AddedDamage { get; protected set; } = 4;
    }



    //base class melee weapon
    public abstract class MeleeWeapon
    {
        public abstract int AddedDamage { get; protected set; }
    }


    //3 melee weapons that inherit from melee weapon class
    public class OneHandedSword : MeleeWeapon
    {
        public override int AddedDamage { get; protected set; } = 3;
    }

    public class TwoHandedSword : MeleeWeapon
    {
        public override int AddedDamage { get; protected set; } = 6;
    }

    public class DualBlade : MeleeWeapon
    {
        public override int AddedDamage { get; protected set; } = 5;
    }



    //base class support weapon
    public abstract class SupportWeapon
    {
        public abstract Dice HealAmount { get; protected set; }
        public abstract float HealChance { get; protected set; }
    }


    //3 support weapons that inherit from support weapn class
    public class Staff : SupportWeapon
    {
        public override Dice HealAmount { get; protected set; } = new(2, 6, +1);
        public override float HealChance { get; protected set; } = 0.8f;
    }

    public class Wand : SupportWeapon
    {
        public override Dice HealAmount { get; protected set; } = new(1, 6, +4);
        public override float HealChance { get; protected set; } = 0.6f;
    }

    public class Syringe : SupportWeapon
    {
        public override Dice HealAmount { get; protected set; } = new(1, 8, +2);
        public override float HealChance { get; protected set; } = 1f;
    }



    //3 ranged units in 3 different races
    public sealed class BowMan : RangedUnit
    {
        public Arrow arrow = new Arrow();
        public Race race = Race.Elf;

        public BowMan(string name)
        {
            Name = name;

            if(Damage is Dice)
                ((Dice)Damage).AddWeaponDamage(arrow.AddedDamage);
        }


    }

    public sealed class Cannoneer : RangedUnit
    {
        public CannonBall cannon = new CannonBall();
        public Race race = Race.Dwarf;

        public Cannoneer(string name)
        {
            Name = name;

            if (Damage is Dice)
                ((Dice)Damage).AddWeaponDamage(cannon.AddedDamage);

        }

    }

    public sealed class ArchNinja : RangedUnit
    {
        public Shuriken shuriken = new();
        public Race race = Race.Ogre;

        public ArchNinja(string name)
        {
            Name = name;

            if (Damage is Dice)
                ((Dice)Damage).AddWeaponDamage(shuriken.AddedDamage);

        }
    }



    //3 melee units in 3 different races
    public sealed class SwordMan : MeleeUnit
    {
        public OneHandedSword oneHandedSword = new();
        public Race race = Race.Elf;

        public SwordMan(string name)
        {
            Name = name;

            if (Damage is Dice)
                ((Dice)Damage).AddWeaponDamage(oneHandedSword.AddedDamage);

        }

    }

    public sealed class DarkKnight : MeleeUnit
    {
        public TwoHandedSword twoHandedSword = new();
        public Race race = Race.Ogre;

        public DarkKnight(string name)
        {
            Name = name;

            if (Damage is Dice)
                ((Dice)Damage).AddWeaponDamage(twoHandedSword.AddedDamage);

        }

    }

    public sealed class DualBlader : MeleeUnit
    {
        public DualBlade dualBlade = new();
        public Race race = Race.Dwarf;

        public DualBlader(string name)
        {
            Name = name;

            if (Damage is Dice)
                ((Dice)Damage).AddWeaponDamage(dualBlade.AddedDamage);

        }
    }



    //3 support units in 3 different races
    public sealed class Bishop : SupportUnit
    {
        public Wand wand = new();
        public Race race = Race.Elf;

        public Bishop(string name)
        {
            Name = name;
        }
    }

    public sealed class Doctor : SupportUnit
    {
        public Syringe syringe = new();
        public Race race = Race.Dwarf;

        public Doctor(string name)
        {
            Name = name;
        }
    }

    public sealed class ArchBishop : SupportUnit
    {
        public Staff staff = new();
        public Race race = Race.Ogre;

        public ArchBishop(string name)
        {
            Name = name;
        }
    }


    //random number providers
    public struct Dice : IRandomProvider
    {
        Random rand = new();
        public uint Scalar;
        public uint BaseDie;
        public int Modifier;

        public Dice(uint _scalar, uint _baseDie, int _modifier)
        {
            Scalar = _scalar;
            BaseDie = _baseDie;
            Modifier = _modifier;
        }

        //public int Roll()
        //{
        //    int sum = Modifier;
        //    if(Scalar > 0)
        //    {
        //        for (int i = 0; i < Scalar; i++)
        //        {
        //            sum += rand.Next(1, (int)BaseDie);
        //        }
        //        return sum;
        //    }
        //    else
        //    {
        //        return sum = 0;
        //    }
        //}

        public void AddWeaponDamage(int weaponDamage)
        {
            Modifier = weaponDamage;
        }

        public override string ToString()
        {
            return Scalar + "d" + BaseDie + Modifier;
        }

        public override bool Equals(object? obj)
        {
            var d = (Dice)obj;
            return Scalar == d.Scalar && BaseDie == d.BaseDie && Modifier == d.Modifier;
        }

        public override int GetHashCode()
        {
            return (int)Scalar * (int)BaseDie + Modifier;
        }

        public int Provide()
        {
            int sum = Modifier;
            if (Scalar > 0)
            {
                for (int i = 0; i < Scalar; i++)
                {
                    sum += rand.Next(1, (int)BaseDie);
                }
                return sum;
            }
            else
            {
                return sum = 0;
            }
        }

        

        public string WeatherEffectDistinguisher(int effectChosen)
        {
            switch (effectChosen)
            {
                case 1:
                    {
                        return "Sunny";
                    }
                case 2:
                    {
                        return "Rainy";
                    }
                case 3:
                    {
                        return "Snowy";
                    }
                default:
                    {
                        return "Default";
                    }                   
            }
        }
    }

    public class Bag : IRandomProvider
    {
        Random rand = new();
        List<int> damageValues = new List<int>();


        public Bag(int[] values)
        {
            foreach (var item in values)
            {
                damageValues.Add(item);
            }
        }
        public int Provide()
        {
            if(damageValues.Count != 0)
            {
                return ElementChooser();
            }
            else
            {
                RefillBag();
                return ElementChooser();
            }
        }

        void RefillBag()
        {
            damageValues.Clear();
            damageValues.Add(4);
            damageValues.Add(6);
            damageValues.Add(8);
            damageValues.Add(10);
        }

        int ElementChooser()
        {
            int chosenIndex = rand.Next(0, damageValues.Count);
            int chosenIndexValue = damageValues.ElementAt(chosenIndex);
            damageValues.RemoveAt(chosenIndex);
            return chosenIndexValue;
        }
    }

    public interface IRandomProvider
    {
        public int Provide();

    }
}
