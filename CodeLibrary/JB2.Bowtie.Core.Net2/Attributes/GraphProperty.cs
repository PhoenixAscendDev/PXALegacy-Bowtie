using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Bowtie
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class GraphPropertyMapAttribute : Attribute
    {
        #region Fields
        protected string _property;
        protected string _memberName;
        protected Enum.ClassMemberType _memberType;
        #endregion Fields

        #region Constructor
        public GraphPropertyMapAttribute(string graphPropertyName, string  memberName , Enum.ClassMemberType memberType = Enum.ClassMemberType.Property)
        {
            _property = graphPropertyName;
        }
        #endregion Constructor

        #region Properties
        public string GraphPropertyName
        {
            get
            {
                return _property;
            }
            set
            {
                _property = value;
            }
        }
        public string MemberName
        {
            get
            {
                return _memberName;
            }
            set
            {
                _memberName = value;
            }
        }

        public Enum.ClassMemberType MemberType
        {
            get
            {
                return _memberType;
            }
            set
            {
                _memberType = value;
            }
        }

        #endregion Properties

    }
}
