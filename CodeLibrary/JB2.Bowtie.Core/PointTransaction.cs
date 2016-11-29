using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class PointTransaction : IPlayerPoint, JB2.Common.IIDNamePair<string,string>, JB2.Common.IValidable
    {
        #region Constructors
        

        protected PointTransaction()
        {

        }
        public PointTransaction(string id)
        {
            this.ID = id;
        }

        #endregion Constructors

        public DateTime TransactionDate { get; set; }

        #region IIDName

        public string ID { get; set; }
        public string Name { get; set; }


        #endregion IIDName

        #region IPointGiver
        public string Description { get; set; }
        public string GiverID { get; set; }      
        public string GiverName { get; set; }


        #endregion IPointGiver

        #region IPlayerPoint
        public string PlayerID { get; set; }

        public string PointGiverType
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int Points { get; set; }
        public string PointSystem { get; set; }

        public string ValidationKey { get; set; }

        public string GetID()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public string GetPlayerID()
        {
            return PlayerID;
        }

        #endregion IPlayerPoint


        #region IValidable

        public bool IsValid { get; set; }
       

        #endregion IValidable

        public static PointTransaction FromPlayerPointGiver(IPlayerPoint playerpoint, IPointGiver pointgiver)
        {
            PointTransaction pt = new PointTransaction();
            pt.ID = JB2.Helper.Bowtie.GenerateID<PointTransaction>();
            pt.Description = pointgiver.Description;
            pt.GiverID = pointgiver.ID;
            pt.GiverName = pointgiver.Name;
            pt.PointGiverType = pointgiver.PointGiverType;
            pt.TransactionDate = System.DateTime.Now;

            pt.PlayerID = playerpoint.GetPlayerID();
            pt.Points = playerpoint.Points;
            pt.PointSystem = playerpoint.PointSystem;
            return pt;
        }

    }
}
