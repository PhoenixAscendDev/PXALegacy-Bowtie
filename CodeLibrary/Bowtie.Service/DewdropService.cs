using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using JB2.Common.Data;

namespace JB2.Bowtie.Service
{
    public class DewdropService
    {
        #region Fields
        protected IDewdropRepository _repo;
        protected IUnitOfWork _uofw;
        #endregion Fields

        #region Constructors
        public DewdropService() : this(JB2.Settings.Bowtie.UnitOfWork)
        {

        }
        public DewdropService(IUnitOfWork uofw)
        {
            _uofw = uofw;
            _repo = (IDewdropRepository)uofw.GetRepository(Enum.RepositoryType.Dewdrop);
        }

        public DewdropService(IDewdropRepository repo)
        {
            _repo = repo;
            _uofw = JB2.Settings.Bowtie.UnitOfWork;
        }
        #endregion Constructors

        public JB2.Common.ServiceResult Validate(IBowtiePlayer player, IDewdrop dewdrop)
        {
            JB2.Common.ServiceResult result = false;

            if (dewdrop.GetjBeanCost() <= 0)
                return true;

            // make sure the person has enough jbeans in wallet
            var wallet = player.GetWallet();

            if (wallet != null && wallet.JBeanTotal >= dewdrop.GetjBeanCost())
                return true;
            else
                return false;
        }

        public PlayerDewdrop GenerateNewPlayerDewdrop(IBowtiePlayer player, IDewdrop dewdrop, string value)
        {
            var metadata = new List<JB2.Common.IMetaData>();
            metadata.Add(new JB2.Common.StringMetaData("PlayerID", player.GetPlayerID()));
            metadata.Add(new JB2.Common.StringMetaData("DewdropID", dewdrop.GetID()));
            metadata.Add(new JB2.Common.DateTimeMetaData("DewDate", DateTime.Now));
            metadata.Add(new JB2.Common.StringMetaData("ApplicationID", dewdrop.GetApplicationID()));

            var newdew = new PlayerDewdrop(metadata,value);
            newdew.Name = dewdrop.Name;
            return newdew;
        }

        #region Retrieve 
        public IEnumerable<Dewdrop> Retrieve(string request, OnErrorReturnType errorReturntype = OnErrorReturnType.ThrowException)
        {
            return _repo.GetAll();
        }

        public IEnumerable<Dewdrop> Retrieve(bool isActive, OnErrorReturnType errorReturntype = OnErrorReturnType.ThrowException)
        {
            return _repo.GetAll();
        }

        public IEnumerable<Dewdrop> Retrieve(OnErrorReturnType errorReturntype = OnErrorReturnType.ThrowException)
        {
            return _repo.GetAll();
        }

        public IEnumerable<Dewdrop> RetrieveByApplication(Application app, OnErrorReturnType errorReturntype = OnErrorReturnType.ThrowException)
        {
            return _repo.GetByApplicationID(app.GetID());
        }

        public Dewdrop RetrieveById(string id, OnErrorReturnType errorReturntype = OnErrorReturnType.ThrowException)
        {
            Regex regex = new Regex(@"\d+");
            Match match = regex.Match(id);
            if(!match.Success)
            {
                id = "dew_" + id;
            }
            try
            {
                return _repo.GetById(id);
            }
            catch(NullReferenceException nullEx)
            {
                switch(errorReturntype)
                {
                    case OnErrorReturnType.ThrowException:
                        throw new ObjectNotFoundInRepositoryException(nullEx, entityId: id, respository: _repo);
                    case OnErrorReturnType.Null:
                        return null;
                    case OnErrorReturnType.EmptyObject:
                    default:
                        return Dewdrop.Empty();
                }                                
            }
        }

        public JB2.Common.ServiceResult Remove(Dewdrop dewdrop, OnErrorReturnType errorReturntype = OnErrorReturnType.ThrowException)
        {
            try
            {
                _repo.Delete(dewdrop);
                return true;
            }
            catch (NullReferenceException nullEx)
            {
                switch (errorReturntype)
                {
                    case OnErrorReturnType.ThrowException:
                        throw new ObjectNotFoundInRepositoryException(nullEx, entityId: dewdrop.ID, respository: _repo);
                    case OnErrorReturnType.Null:
                        return null;
                    case OnErrorReturnType.EmptyObject:
                    default:
                        return false;
                }
            }
            catch(Exception ex)
            {
                ex.BowtieLog();
                return new JB2.Common.ServiceResult(ex);
            }

        }

        public Dewdrop RetrieveByName(string name)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<DewdropTriggerInfo> RetrieveTriggersByDewdrop(string dewdropID, OnErrorReturnType errorReturntype = OnErrorReturnType.ThrowException)
        {
            try
            {
                var result = _repo.GetDewdropTriggersByDewdrop(dewdropID);

                return result;

            }
            catch (NullReferenceException nullEx)
            {
                switch (errorReturntype)
                {
                    case OnErrorReturnType.ThrowException:
                        throw new ObjectNotFoundInRepositoryException(nullEx, entityId: dewdropID, respository: _repo);
                    case OnErrorReturnType.Null:
                        return null;
                    case OnErrorReturnType.EmptyObject:
                    default:
                        return new DewdropTriggerInfo[0];
                }
            }
            catch (Exception ex)
            {
                ex.BowtieLog();
                return new DewdropTriggerInfo[0];
            }

        }
        
        public DewdropTriggerInfo RetrieveTriggerById(string id, OnErrorReturnType errorReturntype = OnErrorReturnType.ThrowException)
        {
            try
            {
                var result = _repo.GetDewdropTriggerByID(id);

                return result;

            }
            catch (NullReferenceException nullEx)
            {
                switch (errorReturntype)
                {
                    case OnErrorReturnType.ThrowException:
                        throw new ObjectNotFoundInRepositoryException(nullEx, entityId: id, respository: _repo);
                    case OnErrorReturnType.Null:
                        return new DewdropTriggerInfo();
                    case OnErrorReturnType.EmptyObject:
                    default:
                        return new DewdropTriggerInfo();
                }
            }
            catch (Exception ex)
            {
                ex.BowtieLog();
                return new DewdropTriggerInfo();
            }

        }
        
        #endregion Retrieve

        public JB2.Common.ServiceResult Save(DewdropTriggerInfo trigger, OnErrorReturnType errorReturntype = OnErrorReturnType.ThrowException)
        {
            try
            {
                _repo.InsertTriggerInfo(trigger);
            }
            catch (NullReferenceException nullEx)
            {
                switch (errorReturntype)
                {
                    case OnErrorReturnType.ThrowException:
                        throw new ObjectNotFoundInRepositoryException(nullEx, entityId: trigger.ID, respository: _repo);
                    case OnErrorReturnType.Null:
                        return null;
                    case OnErrorReturnType.EmptyObject:
                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                ex.BowtieLog();
                return false;
            }

            return true;

        }

        public bool Save(Dewdrop entity)
        {
            _repo.Insert(entity);
            return true;
        }

        public JB2.Common.ServiceResult FireDewdropTriggers(IPlayerDewdrop pd)
        {
            var result = new JB2.Common.ServiceResult();
            var triggers = RetrieveTriggersByDewdrop(pd.GetDewdropID());
            foreach(var t in triggers)
            {
                try
                {
                    if (string.IsNullOrEmpty(t.Classname))
                    {
                        throw new ArgumentNullException("Classname");
                    }
                    var fullName = t.Namespace + "." + t.Classname;
                    // This is assuming that the type will be in the same assembly
                    // as the call. If that's not the case, we can look at that later.
                    Type type = Type.GetType(t.AssemblyQualifiedName);
                    if (type == null)
                    {
                        throw new ArgumentException("No such type: " + type);
                    }
                    if (!typeof(IDewdropTrigger).IsAssignableFrom(type))
                    {
                        throw new ArgumentException("Type " + type +
                                                    " is not compatible with FooParent.");
                    }
                    var trigger = (IDewdropTrigger)Activator.CreateInstance(type, t.ParamaterString1, t.ParameterString2);

                    if (trigger.ShouldWe(pd))
                    {
                        trigger.DoTheDew(pd);
                        
                    }

                }
                catch(Exception ex)
                {
                    ex.BowtieLog();
                    result.Validation.Add(new Common.Validation(ex) { IsValid = false });

                }
            }
            return result;
        }

        #region PlayerDewdrop

        public IEnumerable<IPlayerDewdrop> RetrievePlayerDewdropByPlayerID(string id)
        {
            return _repo.GetPlayerDewsByPlayerID(id);
        }
       
        public bool Save(IPlayerDewdrop playerdewdrop, bool addToQueue = true)
        {

            _repo.InsertPlayerDew(playerdewdrop);

            if (addToQueue)
                _uofw.DewdropQueue.PushDewdrop(playerdewdrop);

            return true;
        }

        #endregion PlayerDewdrop
    }
}
