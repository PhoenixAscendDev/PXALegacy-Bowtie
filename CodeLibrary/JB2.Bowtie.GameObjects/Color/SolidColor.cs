using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Bowtie.Enum;
using JB2.Common;

namespace JB2.Bowtie.GameObjects
{
    public class SolidColor : BowtieObject, IColor
    {

        #region Fields

        private JB2Color _color;
        private ColorSetType _settype;

        #endregion Fields

        #region Constructors

        public SolidColor(string id) : this(id,null,ColorSetType.None)
        {

        }

        public SolidColor(string id, string hexString) : this(id,hexString,ColorSetType.None)
        {

        }

        public SolidColor(string id, string hexString, ColorSetType colorSet) : this(id, JB2Color.FromHex(hexString), colorSet)
        {

        }

        public SolidColor(string id, JB2Color color, ColorSetType colorSet) : base(BowtieObjectType.bowtie_gameobject, id)
        {
            if (color == null)
                color = JB2Color.Empty;
            _color = color;
            _settype = colorSet;
        }


        public SolidColor() : this(JB2Color.Empty)
        {
        }


        public SolidColor(JB2Color color) : base(BowtieObjectType.bowtie_gameobject, JB2.Bowtie.Utility.GenerateNewObjectID())
        {
            _color = color;
            _settype = ColorSetType.None;
        }

        #endregion Constructors



        #region Properties
        public JB2Color Color
        {
            get
            {
                return _color;
            }
        }

        public ColorSetType ColorSet
        {
            get
            {
                return _settype;
            }

            set
            {
                _settype = value;
            }
        }

        public GameObjectType GameObjectType
        {
            get
            {
                return GameObjectType.Color;
            }
        }

        public string HexValue
        {
            get
            {
                return _color.HexString;
            }
        }

        #endregion Properties
    }
}
