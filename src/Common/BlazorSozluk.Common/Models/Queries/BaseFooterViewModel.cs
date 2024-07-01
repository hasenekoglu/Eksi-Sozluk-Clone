using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorSozluk.Common.ViewModels;

namespace BlazorSozluk.Common.Models.Queries
{
    public class BaseFooterRateViewModel
    {
        public VoteType VoteType { get; set; }

    }

    public class BaseFooterFavoriteViewModel
    {
        public bool IsFavorited { get; set; }
        public int FavoritedCount { get; set; }

    }

    public class BaseFooterRateFavoritedViewModel : BaseFooterFavoriteViewModel
    {
        public VoteType VoteType { get; set; }
    }
}
