using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie.GameObjects;

namespace JB2.Bowtie
{
    public interface IGameObjectRepository : JB2.Common.IRepository<JB2.Bowtie.IGameObject, string>
    {

        #region Bingo Methods
        IBingoCard GetBingoCardByID(string id);

        JB2.Common.ServiceResult InsertBingoCard(IBingoCard card);

        JB2.Common.ServiceResult InsertBingoCardImage(IBingoCard card, string styleCode);

        JB2.Common.ServiceResult InsertBingoCardImage(string id, string styleCode, JB2.Common.JB2Image image);
        JB2.Common.JB2Image GetBingoCardImage(IBingoCard card, string styleCode);
        JB2.Common.JB2Image GetBingoCardImage(string id, string styleCode);

        JB2.Common.JB2Image GetBingoCardStyle(string styleCode);

        #endregion Bingo Methods

        #region Color Methods
        JB2.Common.ServiceResult InsertColor(IColor c);
        IColor GetColorByHex(string hexString);

        IColor GetColorByID(string id);

        IColor GetColorByName(string name, JB2.Bowtie.Enum.ColorSetType set);

        IColor[] GetColorsByColorSet(JB2.Bowtie.Enum.ColorSetType set);
        

        JB2.Common.ServiceResult InsertColorSet(IColorSet c);

        IColorSet GetColorSet(JB2.Bowtie.Enum.ColorSetType type);

        #endregion Color Methods

    }
}
