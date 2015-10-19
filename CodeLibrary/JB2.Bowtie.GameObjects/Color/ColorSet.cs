using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Bowtie.Enum;
using JB2.Common;

namespace JB2.Bowtie.GameObjects
{
    public class ColorSet : BowtieObject, IColorSet
    {

        #region Fields

        private IList<IColor> _colors;
        private ColorSetType _type;

        #endregion Fields


        #region Constructors

        public ColorSet() : this(JB2.Bowtie.Utility.GenerateNewObjectID(),ColorSetType.None,null) 
        {

        }

        public ColorSet(string id, ColorSetType type, IColor[] colors) : base(BowtieObjectType.bowtie_gameobject,id)
        {
            if(colors == null)
            {
                _colors = new List<IColor>();
            }
            _colors = colors.ToList();
            _type = type;

        }

        #endregion Constructors


        #region Properties
        public IColor[] Colors
        {
            get
            {
                return _colors.ToArray();
            }
        }

        public int Count
        {
            get
            {
                return _colors.Count();
            }
        }


        public ColorSetType Type
        {
            get
            {
                return _type;
            }

            set
            {
                _type = value;
            }
        }

        public GameObjectType GameObjectType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion Properties

        public ServiceResult AddColor(IColor c)
        {
            _colors.Add(c);
            return true;
        }

        public ServiceResult RemoveColor(IColor c)
        {
            _colors.Remove(c);
            return true;
        }

        public IColor FindColorByHex(string hexString)
        {
            IColor result = null;
            foreach (IColor c in _colors)
            {
                if (c.HexValue == hexString)
                    result = c;

            }
            return result;
        }

        public IColor FindColorByName(string name)
        {
            IColor result = null;
            foreach(IColor c in _colors)
            {
                if (c.Name == name)
                    result = c;

            }
            return result;

        }


    }
}
