using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie;

namespace JB2.Economy
{
    public class JbeanTreasury : JB2.Common.IDNamePair, ITreasury
    {
        #region Fields


        #endregion Fields

        #region Constructor

        public JbeanTreasury() : base(string.Empty,string.Empty)
        {
           
        }

        #endregion Constructor




        public void Cancel(ITreasuryNote treasuryNote)
        {
            throw new NotImplementedException();
        }

        public long GetAmountIssued()
        {
            throw new NotImplementedException();
        }

        public ITreasuryNote IssueDeomination(ITreasuryRequest request)
        {
            throw new NotImplementedException();
        }
    }



    //    ITreasury<JBean,JBeanToken,Enum.JBeanTokenType,string,string>
    //{
    //    private ITreasuryRepository<JB2.Economy.JBeanTreasuryLogEntry,Enum.JBeanTokenType,string> _repo;


    //    public JBeanTreasury(ITreasuryRepository<JB2.Economy.JBeanTreasuryLogEntry,Enum.JBeanTokenType,string> repo)
    //    {
    //        _repo = repo;
    //    }

    //    public JBeanTreasury() : this( new JB2.Bowtie.Data.Linq.JBeanRespository())
    //    {

    //    }



    //    private  bool LogTransaction(JBeanTreasuryLogEntry log)
    //    {
    //        _repo.AddLog(log);
    //        return true;
    //    }

    //    public JBeanToken[] IssueDenomination(Enum.JBeanTokenType type, int quantity)
    //    {

    //        if(JB2.Bowtie.Settings.CurrentApplication == null)
    //            throw new JB2.Bowtie.Exceptions.ApplicationNotInitialized();

    //        if (!JB2.Bowtie.Settings.CurrentApplication.canIssueJBeans)
    //            throw new JB2.Bowtie.Exceptions.IssueJBeanProhibited();


    //        List<JBeanToken> result = new List<JBeanToken>(quantity);


    //        for(int i = 1; i <=quantity;i++)
    //        {
    //            JBeanToken t = new JBeanToken(type);
    //            result.Add(t);
    //        }
    //        JBeanToken logtoken = new JBeanToken(type);
    //        LogTransaction(new JBeanTreasuryLogEntry(JB2.Bowtie.Settings.CurrentApplication.ID, logtoken, quantity));
    //        return result.ToArray();

    //    }

    //    public ulong TotalAmountIssued
    //    {
    //        get;
    //        set;
    //    }

    //    public long DenominationIssuedCount(Enum.JBeanTokenType type)
    //    {
    //        return 0;
    //    }
    //}
}
