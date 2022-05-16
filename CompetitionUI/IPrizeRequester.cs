using CompetitionLibrary.Models;

namespace CompetitionUI
{
    public interface IPrizeRequester
    {
        void PrizeComplete(Prize model);
    }
}
