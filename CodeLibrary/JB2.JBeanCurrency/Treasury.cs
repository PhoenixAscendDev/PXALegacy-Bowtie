using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie;
using JB2.Economy;

namespace JB2.JBeanCurrency
{
    public class BowtieTreasury : JB2.Bowtie.ICurrencyTreasury
    {
        public decimal RequestAmount(JB2.Bowtie.IApplication application, string currencyID, decimal amount)
        {
            var treasury = JB2.Settings.Jbean.Factory.Treasury;

            var request = new jBeanRequest();
            request.Requestor = application;
            request.VerificationKey = application.GetTreasuryRequestKey(treasury.ID).Key;
            request.RequestDate = System.DateTime.Now;
            request.Amount = (long)amount;

            decimal finalAmount = (decimal)0;

            try
            {
                var note = treasury.IssueNote(request);
                finalAmount = (decimal)note.Amount;
            }
            catch(Exception ex)
            {
                ex.BowtieLog();
            }

            return finalAmount;


        }
    }
}
