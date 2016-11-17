using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common;

namespace JB2.Bowtie
{
    public class PlayerStory : JB2.Common.IDNamePair, IPlayerStory
    {

        #region Fields

        protected string _playerID;
        protected string _applicationID;



        #endregion Fields



        #region Constructor

        public PlayerStory(string playerID, string applicationID, GraphStory story) : this(playerID, applicationID, story.AssociatedAction, story.AssociatedObject)
        {

        }



        public PlayerStory(string playerID, string applicationID, GraphAction graphAction, GraphObject graphObject) : this()
        {
            Action = graphAction;
            Object = graphObject;

            _playerID = playerID;
            _applicationID = applicationID;

            CreateDate = System.DateTime.Now;

            this.ActionData = new List<IMetaData>();
            this.ObjectData = new List<IMetaData>();

            foreach (var ap in ((GraphAction)Action).GetProperties())
            {
                var v = ActionData.ToList();
                v.Add(new MetaData<object>(ap.PropertyName, null));

                this.ActionData = v;
            }

            foreach (var op in ((GraphObject)Object).GetProperties())
            {
                var v = ActionData.ToList();
                v.Add(new MetaData<object>(op.PropertyName, null));

                this.ActionData = v;
            }

        }
        private PlayerStory()
        {
            this.ActionData = new List<IMetaData>();
            this.ObjectData = new List<IMetaData>();
        }

        #endregion Constructor

        public IGraphElement Action
        {
            get; set;

        }

        public IEnumerable<IMetaData> ActionData
        {
            get; set;

        }

        public DateTime CreateDate
        {
            get; set;

        }

        public IGraphElement Object
        {
            get; set;

        }

        public IEnumerable<IMetaData> ObjectData
        {
            get; set;
        }

        public IEnumerable<IMetaData> PlayerData
        {
            get; set;
        }

        public string GetPlayerID()
        {
            return _playerID;
        }

        public string GetApplicationID()
        {
            return _applicationID;
        }

        public int ActionWeight
        {
            get;set;
        }
    }
}
