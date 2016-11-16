using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common;

namespace JB2.Bowtie
{
    public class PlayerStory : JB2.Common.IDNamePair,IPlayerStory
    {

        #region Fields

        protected string _playerID;



        #endregion Fields



        #region Constructor

        public PlayerStory(GraphStory graphStory) : this()
        {
            Action = graphStory.AssociatedAction;
            Object = graphStory.AssociatedObject;

        }
        public PlayerStory()
        {
            this.ActionData = new List<IMetaData>();
            this.ObjectData = new List<IMetaData>();
        }

        #endregion Constructor

        public IGraphElement Action
        {
            get;set;
            
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

        public string GetPlayerID()
        {
            throw new NotImplementedException();
        }
    }
}
