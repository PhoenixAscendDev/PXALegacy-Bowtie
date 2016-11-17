using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common;

namespace JB2.Bowtie
{
    public abstract class AchievementTrigger : JB2Class, IDewdropTrigger
    {

        #region Constructor


        public AchievementTrigger() : this(string.Empty,null)
        {

        }

        public AchievementTrigger(string string1 = null, string string2 = null) : base()
        {

            init(string1);

        }

        #endregion Constructor

        public virtual ServiceResult DoTheDew(IPlayerDewdrop dewdrop)
        {
            try
            {

                var prepo = JB2.Settings.Bowtie.UnitOfWork.PlayerRepository;
                var player = prepo.GetAppPlayerByID(dewdrop.GetPlayerID(), dewdrop.GetApplicationID());

                var aService = new JB2.Bowtie.Service.AchievementService();

                var pa = aService.CalculateAchievement(player, Achievement.ID, dewdrop);
            }
            catch(Exception ex)
            {
                ex.BowtieLog();
            }

            return true;
        }

        public virtual bool ShouldWe(IPlayerDewdrop dewdrop)
        {
            //if Achievement doesn't exist then nope
            if (Achievement == null)
                return false;

            try
            {
                //if player already earned it then nope
                var repo = JB2.Settings.Bowtie.UnitOfWork.AchievementRepository;

                var pa = repo.GetPlayerAchievement(dewdrop.GetPlayerID(), Achievement.GetID());

                if (pa != null && pa.AchievementFlags.Contains(Enum.AchievementFlag.Earned))
                    return false;
            }
            catch(Exception ex)
            {
                //oh no something happened lets not do this
                ex.BowtieLog();
                return false;
            }

            return true;


        }




        protected IAchievement Achievement
        {
            get
            {
                return _props.GetProperty<IAchievement>("A", null);
            }

            set
            {
                _props.SetProperty<IAchievement>("A", value);
            }
        }

        protected void init(string achievementID)
        {
            var repo = JB2.Settings.Bowtie.UnitOfWork.AchievementRepository;

            try
            {
                var a = repo.GetById(achievementID);
                this.Achievement = a;
            }
            catch(Exception ex)
            {
                ex.BowtieLog();
                this.Achievement = null;
            }
        }
    }
}
