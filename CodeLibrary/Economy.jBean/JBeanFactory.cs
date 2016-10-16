using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;
using JB2.Economy.Enum;

using JB2.Common.Log;

namespace JB2.Economy
{
    //private static JB2.Economy.EconomicFactory;
    public class JBeanFactory : Economy.EconomicFactory<IIDProp<string>, jBeanAccountStatus, ITreasuryRequest, IRequestor, string, JB2.Common.Enum.LogServerityType, Common.ILogEntry>
    {


        public ISetting GetSetting(string settingName)
        {
            if (Settings == null)
                return null;
            else
                return Settings[settingName];
        }

        public string GetTokenImageFront(Enum.JBeanTokenType type)
        {
            if (Settings == null)
                return string.Empty;

            var settingName = string.Empty;
            switch (type)
            {
                case JBeanTokenType.Kidney:
                    settingName = JB2.Economy.JbeanSettingName.KidneyFrontImage;
                    break;
                case JBeanTokenType.Navy:
                    settingName = JB2.Economy.JbeanSettingName.NavyFrontImage;
                    break;
                case JBeanTokenType.Pinto:
                    settingName = JB2.Economy.JbeanSettingName.PintoFrontImage;
                    break;
            }

            if (Settings[settingName] != null)
                return (string)Settings[settingName].Value;
            else
                return string.Empty;
        }

        public string GetTokenImageBack(Enum.JBeanTokenType type)
        {
            if (Settings == null)
                return string.Empty;

            var settingName = string.Empty;
            switch (type)
            {
                case JBeanTokenType.Kidney:
                    settingName = JB2.Economy.JbeanSettingName.KidneyBackImage;
                    break;
                case JBeanTokenType.Navy:
                    settingName = JB2.Economy.JbeanSettingName.NavyBackImage;
                    break;
                case JBeanTokenType.Pinto:
                    settingName = JB2.Economy.JbeanSettingName.PintoBackImage;
                    break;
            }

            if (Settings[settingName] != null)
                return (string)Settings[settingName].Value;
            else
                return string.Empty;
        }

        public int GetTokenValue(Enum.JBeanTokenType type)
        {
            if (Settings == null)
            {
                switch (type)
                {
                    case Enum.JBeanTokenType.Kidney:
                        return 1;
                    case Enum.JBeanTokenType.Navy:
                        return 100;
                    case Enum.JBeanTokenType.Pinto:
                        return 1000;
                    default:
                        return 0;
                }
            }

            var settingName = string.Empty;

            switch (type)
            {
                case Enum.JBeanTokenType.Kidney:
                    settingName = JbeanSettingName.KidneyValue;
                    break;
                case Enum.JBeanTokenType.Navy:
                    settingName = JbeanSettingName.NavyValue;
                    break;
                case Enum.JBeanTokenType.Pinto:
                    settingName = JbeanSettingName.PintoValue;
                    break;
                default:
                    return 0;
            }

            if (Settings[settingName] != null)
                return (int)Settings[settingName].Value;
            else
                return 0;


        }

        public static JBeanFactory Configure(IEnumerable<ISetting> settings, IJBeanRepository repo)
        {
            JBeanFactory factory = new JBeanFactory();

            JB2.Common.SettingCollection<string> sc = new SettingCollection<string>(settings);

            IEnumerable<ISetting> defaultsettings = new List<ISetting>();
            //get default settings
            try
            {
                foreach (var s in settings)
                {
                    if (s.ID == JB2.Economy.JbeanSettingName.CurrencyID)
                        defaultsettings = repo.GetFactorySettings((string)s.Value);
                }
               
                foreach (var ds in defaultsettings)
                {
                    if (settings.ToList().Find(x => x.ID == ds.ID) == null)
                        sc.Add(ds);
                }

            }
            catch (Exception ex)
            {
                sc = new SettingCollection<string>(settings);
            }

            factory.Settings = sc;
            factory.Denominations = new IDenomination[3] {  new JBeanDenomination(Enum.JBeanTokenType.Kidney),
                                                            new JBeanDenomination(Enum.JBeanTokenType.Navy),
                                                            new JBeanDenomination(Enum.JBeanTokenType.Pinto)
                                                          };

            ICurrency jBeanCurrency = new JBean(JB2.Configuration.GetjBeanCurrencyID());

            factory.Currencies = new ICurrency[1] { jBeanCurrency };
            factory.Denominations = jBeanCurrency.Denominations;

            var treasury = new JbeanTreasury(repo);
            treasury.NoteCancelled += factory.Log_NoteCancelled;
            treasury.NoteIssued += factory.Log_NoteIssued;
            factory.Treasury = treasury;


            var logger = new JB2.Infrastructure.ProjectLogger((JB2.Common.Log.ILogRepo)sc[JB2.Economy.JbeanSettingName.LogRepo].Value);
            factory.Logger = logger;

           

            var bank = new jBeanCentralBank(factory.Treasury, repo);
            bank.AccountNumberGenerated += factory.Log_AccountNumberGenerated;
            bank.AccountAccessed += factory.Log_AccountAccessed;
            bank.AccountOpened += factory.Log_AccountOpened;
            bank.AccountStatusChange += factory.Log_AccountStatus;
            bank.AccountDeposited += factory.Log_AccountDeposit;
            bank.AccountWithdrawn += factory.Log_AccountWithdrawn;             
            factory.CentralBank = bank;




            return factory;
        }


        #region Log Bank Events

        protected void Log_AccountAccessed(IBank<JB2.Common.IIDProp<string>, JB2.Economy.Enum.jBeanAccountStatus, ITreasuryRequest, IRequestor, string> bank, 
                                          IBankAccount<JB2.Common.IIDProp<string>, JB2.Economy.Enum.jBeanAccountStatus> account)
        {
            ILogEntry entry = new Common.Log.LogEntry("Info-01" + JB2.Common.NewID.ShortGuid(), Common.Enum.LogServerityType.Informational, "Account Accessed: " + account.AccountNumber, null, DateTime.Now);
            Logger.Log(entry);
        }

        protected void Log_AccountNumberGenerated(string number)
        {
            ILogEntry entry = new Common.Log.LogEntry("Info-02" + JB2.Common.NewID.ShortGuid(), Common.Enum.LogServerityType.Informational, "Account Number Generated: " + number, null, DateTime.Now);
            Logger.Log(entry);
        }

        protected void Log_AccountOpened(IBank<JB2.Common.IIDProp<string>, JB2.Economy.Enum.jBeanAccountStatus, ITreasuryRequest, IRequestor, string> bank, IBankAccount<JB2.Common.IIDProp<string>, JB2.Economy.Enum.jBeanAccountStatus> account)
        {
            ILogEntry entry = new Common.Log.LogEntry("Info-03" + JB2.Common.NewID.ShortGuid(), Common.Enum.LogServerityType.Informational, "New Account :" + account.AccountNumber , null, DateTime.Now);
            Logger.Log(entry);
        }

        protected void Log_AccountStatus(IBank<JB2.Common.IIDProp<string>, JB2.Economy.Enum.jBeanAccountStatus, ITreasuryRequest, IRequestor, string> bank, 
                                         IBankAccount<JB2.Common.IIDProp<string>, JB2.Economy.Enum.jBeanAccountStatus> account, 
                                         JB2.Economy.Enum.jBeanAccountStatus prevStatus, JB2.Economy.Enum.jBeanAccountStatus newStatus)
        {
            ILogEntry entry = new Common.Log.LogEntry("Info-04" + JB2.Common.NewID.ShortGuid(), Common.Enum.LogServerityType.Informational,
                                            "Account Status Change " + "\r\n"
                                          + "Account Number =" + account.AccountNumber + "\r\n"
                                          + "Previous =" + prevStatus.ToString() + "\r\n"
                                          + "New = " + newStatus.ToString(), null, DateTime.Now);

            Logger.Log(entry);
        }

        protected void Log_AccountDeposit(IBank<JB2.Common.IIDProp<string>, JB2.Economy.Enum.jBeanAccountStatus, ITreasuryRequest, IRequestor, string> bank,
                                          IBankAccount<JB2.Common.IIDProp<string>, JB2.Economy.Enum.jBeanAccountStatus> account, 
                                          IBankTransactionReceipt receipt,
                                          long amount)
        {
            ILogEntry entry = new Common.Log.LogEntry("Info-05" + JB2.Common.NewID.ShortGuid(), Common.Enum.LogServerityType.Informational,
                                            "Account Deposit " + "\r\n"
                                          + "Account Number =" + account.AccountNumber + "\r\n"
                                          + "TransationID=" + receipt.TransactionNumber + "\r\n"
                                          + "Amount = " + amount.ToString(), null, DateTime.Now);

            Logger.Log(entry);

        }

        protected void Log_AccountWithdrawn(IBank<JB2.Common.IIDProp<string>, JB2.Economy.Enum.jBeanAccountStatus, ITreasuryRequest, IRequestor, string> bank,
                                  IBankAccount<JB2.Common.IIDProp<string>, JB2.Economy.Enum.jBeanAccountStatus> account,
                                  IBankTransactionReceipt receipt,
                                  long amount)
        {
            ILogEntry entry = new Common.Log.LogEntry("Info-06" + JB2.Common.NewID.ShortGuid(), Common.Enum.LogServerityType.Informational,
                                          "Account Withdraw " + "\r\n"
                                          + "Account Number =" + account.AccountNumber + "\r\n"
                                          + "TransationID=" + receipt.TransactionNumber + "\r\n"
                                          + "Amount = " + amount.ToString(),null, DateTime.Now);

            Logger.Log(entry);

        }

        protected void Log_NoteIssued(ITreasury treasury, ITreasuryNote treasuryNote, ITreasuryRequest request)
        {
            ILogEntry entry = new Common.Log.LogEntry("Info-07" + JB2.Common.NewID.ShortGuid(), Common.Enum.LogServerityType.Informational,
                              "Note Issued " + "\r\n"
                              + "Requested By=" + treasuryNote.GetRequestor().ID + "\r\n"
                              + "ID=" + treasuryNote.ID + "\r\n"                            
                              + "Amount = " + treasuryNote.Amount.ToString(), null, DateTime.Now);
            Logger.Log(entry);

        }

        protected void Log_NoteCancelled(ITreasury treasury, ITreasuryNote treasuryNote)
        {
            ILogEntry entry = new Common.Log.LogEntry("Info-07" + JB2.Common.NewID.ShortGuid(), Common.Enum.LogServerityType.Informational,
                              "Note Cancelled " + "\r\n"
                              + "Requested By=" + treasuryNote.GetRequestor().ID + "\r\n"
                              + "ID=" + treasuryNote.ID + "\r\n"
                              + "Amount = " + treasuryNote.Amount.ToString(), null, DateTime.Now);
            Logger.Log(entry);
        }


        #endregion
    }
}
