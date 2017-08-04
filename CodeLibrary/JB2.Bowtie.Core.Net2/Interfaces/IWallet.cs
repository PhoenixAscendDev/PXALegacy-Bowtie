using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public interface IWallet : JB2.Economy.IApplicationWallet<JB2.Common.IPerson<string>, string>, JB2.Bowtie.IPlayerable<string>
    {
        //JB2.Economy.JBeanBag JBeanTotal { get; }

        //IEnumerable<JB2.Economy.jBeanToken> GetJBeanTokens();

        //bool AddTreasuryNote(JB2.Economy.JbeanTreasuryNote note);

        IEnumerable<WalletReceipt> GetReceipts();

        IEnumerable<JB2.Economy.ITreasuryNote> GetTreasuryNotes();

        bool AddTreasuryNote(JB2.Economy.ITreasuryNote note);
    }
}
