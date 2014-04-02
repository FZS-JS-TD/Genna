using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Genna.Levels
{
    public class Tile
    {
        // Attributes
        private Rectangle rectangle;
        private bool collides;
        private Texture2D texture;
        private int id;
        private int x;
        private int y;

        // Tile types (so far)
        private Texture2D brick1;
        private Texture2D brick2;
        private Texture2D concrete;
        private Texture2D concreteStriped;
        private Texture2D gateBars;
        private Texture2D gateHorizontalBar;
        private Texture2D gateTop;
        private Texture2D gateTopEnd;
        private Texture2D pipeStraight1;
        private Texture2D pipeStraight2;
        private Texture2D pipeLTurn;
        private Texture2D platform1;
        private Texture2D chestNormal;
        private Texture2D chestEnchanted;
        private Texture2D chestArmored;
        private Texture2D clinicFloor;
        private Texture2D clinicTile;
        private Texture2D clock;
        private Texture2D clock2;
        private Texture2D counter;
        private Texture2D counter2;
        private Texture2D dirt1;
        private Texture2D dirt2;
        private Texture2D dirtPoisoned;
        private Texture2D doorTop;
        private Texture2D doorBottom;
        private Texture2D emptyShelve;
        private Texture2D emptyShelveLeft;
        private Texture2D emptyShelveRight;
        private Texture2D potionShelve;
        private Texture2D potionShelveLeft;
        private Texture2D potionShelveRight;
        private Texture2D flowerVase;
        private Texture2D medKit;
        private Texture2D rock1;
        private Texture2D rock2;
        private Texture2D rock3;
        private Texture2D roofingSlope;
        private Texture2D roofingSlopeReversed;
        private Texture2D roofingTile;
        private Texture2D window;
        private Texture2D wood;
        private Texture2D woodFloor;
        private Texture2D spikes;

        // File names for the texture types
        private string BRICK1 = "Brick";
        private string BRICK2 = "Brick1";
        private string CONCRETE = "Concrete";
        private string CONCRETESTRIPED = "ConcreteStriped";
        private string GATEBARS = "Gate_Bars";
        private string GATEHORIZONTALBAR = "Gate_Bars_Horizontal";
        private string GATETOP = "Gate_Top";
        private string GATETOPEND = "Gate_Top_End";
        private string PIPESTRAIGHT1 = "Pipe_Straight";
        private string PIPESTRAIGHT2 = "Pipe_Straight_2";
        private string PIPELTURN = "Pipe_Turned";
        private string PLATFORM1 = "Platform1";
        private string CHESTNORMAL = "Chest1";
        private string CHESTENCHANTED = "Chest2.2";
        private string CHESTARMORED = "Chest2";
        private string CLINICFLOOR = "ClinicFloor";
        private string CLINICTILE = "ClinicTile";
        private string CLOCK = "Clock";
        private string CLOCK2 = "Clock2";
        private string COUNTER = "Counter";
        private string COUNTER2 = "Counter2";
        private string DIRT1 = "Dirt1";
        private string DIRT2 = "Dirt2";
        private string DIRTPOISONED = "Dirt_Poisoned";
        private string DOORTOP = "Door_Top";
        private string DOORBOTTOM = "Door_Bottom";
        private string EMPTYSHELVE = "EmptyShelve";
        private string EMPTYSHELVELEFT = "EmptyShelveLeft";
        private string EMPTYSHELVERIGHT = "EmptyShelveRight";
        private string POTIONSHELVE = "PotionShelve";
        private string POTIONSHELVELEFT = "PotionShelveLeft";
        private string POTIONSHELVERIGHT = "PotionShelveRight";
        private string FLOWERVASE = "FlowerVase";
        private string MEDKIT = "MedKit";
        private string ROCK1 = "Rock1";
        private string ROCK2 = "Rock2";
        private string ROCK3 = "Rock3";
        private string ROOFINGSLOPE = "RoofingSlope";
        private string ROOFINGSLOPEREVERSED = "RoofingSlopeReversed";
        private string ROOFINGTILE = "RoofingTile";
        private string WINDOW = "Window";
        private string WOOD = "Wood";
        private string WOODFLOOR = "WoodFloor";
        private string SPIKES = "Spikes";

        // Properties
        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        public bool Collides
        {
            get { return collides; }
            set { collides = value; }
        }

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        #region Tile Texure Properties
        public Texture2D Brick1
        {
            get { return brick1; }
        }

        public Texture2D Brick2
        {
            get { return brick2; }
        }

        public Texture2D Concrete
        {
            get { return concrete; }
        }

        public Texture2D ConcreteStriped
        {
            get { return concreteStriped; }
        }

        public Texture2D GateBars
        {
            get { return gateBars; }
        }

        public Texture2D GateHorizontalBar
        {
            get { return gateHorizontalBar; }
        }

        public Texture2D GateTop
        {
            get { return gateTop; }
        }
        public Texture2D GateTopEnd
        {
            get { return gateTopEnd; }
        }

        public Texture2D PipeStraight1
        {
            get { return pipeStraight1; }
        }

        public Texture2D PipeStraight2
        {
            get { return pipeStraight2; }
        }

        public Texture2D PipeLTurn
        {
            get { return pipeLTurn; }
        }

        public Texture2D Platform1
        {
            get { return platform1; }
        }

        public Texture2D ChestNormal
        {
            get { return chestNormal; }
        }

        public Texture2D ChestEnchanted
        {
            get { return chestEnchanted; }
        }

        public Texture2D ChestArmored
        {
            get { return chestArmored; }
        }

        public Texture2D ClinicFloor
        {
            get { return clinicFloor; }
        }

        public Texture2D ClinicTile
        {
            get { return clinicTile; }
        }

        public Texture2D Clock
        {
            get { return clock; }
        }

        public Texture2D Clock2
        {
            get { return clock2; }
        }

        public Texture2D Counter
        {
            get { return counter; }
        }

        public Texture2D Counter2
        {
            get { return counter2; }
        }

        public Texture2D Dirt1
        {
            get { return dirt1; }
        }

        public Texture2D Dirt2
        {
            get { return dirt2; }
        }

        public Texture2D DirtPoisoned
        {
            get { return dirtPoisoned; }
        }

        public Texture2D EmptyShelve
        {
            get { return emptyShelve; }
        }

        public Texture2D EmptyShelveLeft
        {
            get { return emptyShelveLeft; }
        }

        public Texture2D EmptyShelveRight
        {
            get { return emptyShelveRight; }
        }

        public Texture2D PotionShelve
        {
            get { return potionShelve; }
        }

        public Texture2D PotionShelveLeft
        {
            get { return potionShelveLeft; }
        }

        public Texture2D PotionShelveRight
        {
            get { return potionShelveRight; }
        }

        public Texture2D FlowerVase
        {
            get { return flowerVase; }
        }

        public Texture2D MedKit
        {
            get { return medKit; }
        }

        public Texture2D Rock1
        {
            get { return rock1; }
        }

        public Texture2D Rock2
        {
            get { return rock2; }
        }

        public Texture2D Rock3
        {
            get { return rock3; }
        }

        public Texture2D RoofingSlope
        {
            get { return roofingSlope; }
        }

        public Texture2D RoofingSlopeReversed
        {
            get { return roofingSlopeReversed; }
        }

        public Texture2D RoofingTile
        {
            get { return roofingTile; }
        }

        public Texture2D Window
        {
            get { return window; }
        }

        public Texture2D Wood
        {
            get { return wood; }
        }

        public Texture2D WoodFloor
        {
            get { return woodFloor; }
        }

        public Texture2D DoorTop
        {
            get { return doorTop; }
        }

        public Texture2D DoorBottom
        {
            get { return doorBottom; }
        }

        public Texture2D Spikes
        {
            get { return spikes; }
        }
        #endregion

        /// <summary>
        /// Loads the Texture2Ds
        /// </summary>
        /// <param name="content">ContentManager from Game1 class</param>
        public void Load(ContentManager content)
        {
            brick1 = content.Load<Texture2D>("Tiles/" + BRICK1);

            brick2 = content.Load<Texture2D>("Tiles/" + BRICK2);

            concrete = content.Load<Texture2D>("Tiles/" + CONCRETE);

            concreteStriped = content.Load<Texture2D>("Tiles/" + CONCRETESTRIPED);

            gateBars = content.Load<Texture2D>("Tiles/" + GATEBARS);

            gateHorizontalBar = content.Load<Texture2D>("Tiles/" + GATEHORIZONTALBAR);

            gateTop = content.Load<Texture2D>("Tiles/" + GATETOP);

            gateTopEnd = content.Load<Texture2D>("Tiles/" + GATETOPEND);

            pipeStraight1 = content.Load<Texture2D>("Tiles/" + PIPESTRAIGHT1);

            pipeStraight2 = content.Load<Texture2D>("Tiles/" + PIPESTRAIGHT2);

            pipeLTurn = content.Load<Texture2D>("Tiles/" + PIPELTURN);

            platform1 = content.Load<Texture2D>("Tiles/" + PLATFORM1);

            chestNormal = content.Load<Texture2D>("Tiles/" + CHESTNORMAL);

            chestEnchanted = content.Load<Texture2D>("Tiles/" + CHESTENCHANTED);

            chestArmored = content.Load<Texture2D>("Tiles/" + CHESTARMORED);

            clinicFloor = content.Load<Texture2D>("Tiles/" + CLINICFLOOR);

            clinicTile = content.Load<Texture2D>("Tiles/" + CLINICTILE);

            clock = content.Load<Texture2D>("Tiles/" + CLOCK);

            clock2 = content.Load<Texture2D>("Tiles/" + CLOCK2);

            counter = content.Load<Texture2D>("Tiles/" + COUNTER);

            counter2 = content.Load<Texture2D>("Tiles/" + COUNTER2);

            dirt1 = content.Load<Texture2D>("Tiles/" + DIRT1);

            dirt2 = content.Load<Texture2D>("Tiles/" + DIRT2);

            dirtPoisoned = content.Load<Texture2D>("Tiles/" + DIRTPOISONED);

            doorTop = content.Load<Texture2D>("Tiles/" + DOORTOP);

            doorBottom = content.Load<Texture2D>("Tiles/" + DOORBOTTOM);

            emptyShelve = content.Load<Texture2D>("Tiles/" + EMPTYSHELVE);

            emptyShelveLeft = content.Load<Texture2D>("Tiles/" + EMPTYSHELVELEFT);

            emptyShelveRight = content.Load<Texture2D>("Tiles/" + EMPTYSHELVERIGHT);

            potionShelve = content.Load<Texture2D>("Tiles/" + POTIONSHELVE);

            potionShelveLeft = content.Load<Texture2D>("Tiles/" + POTIONSHELVELEFT);

            potionShelveRight = content.Load<Texture2D>("Tiles/" + POTIONSHELVERIGHT);

            flowerVase = content.Load<Texture2D>("Tiles/" + FLOWERVASE);

            medKit = content.Load<Texture2D>("Tiles/" + MEDKIT);

            rock1 = content.Load<Texture2D>("Tiles/" + ROCK1);

            rock2 = content.Load<Texture2D>("Tiles/" + ROCK2);

            rock3 = content.Load<Texture2D>("Tiles/" + ROCK3);

            roofingSlope = content.Load<Texture2D>("Tiles/" + ROOFINGSLOPE);

            roofingSlopeReversed = content.Load<Texture2D>("Tiles/" + ROOFINGSLOPEREVERSED);

            roofingTile = content.Load<Texture2D>("Tiles/" + ROOFINGTILE);

            window = content.Load<Texture2D>("Tiles/" + WINDOW);

            wood = content.Load<Texture2D>("Tiles/" + WOOD);

            woodFloor = content.Load<Texture2D>("Tiles/" + WOODFLOOR);

            spikes = content.Load<Texture2D>("Tiles/" + SPIKES);
        }
    }
}
