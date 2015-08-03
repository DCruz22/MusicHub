using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicHub.Models;
using MusicHub.ViewModels;
using MusicHub.Data.Reps.Reps;

namespace MusicHub.Data.StrategyAlgorithms
{
    public class SearchUserAlgorithm
    {
        public string filter { get; set; }

        private UserRepository _usrep = new UserRepository();

        public SearchUserAlgorithm(string filter)
        {
            this.filter = filter;
        }

        public IEnumerable<User> SearchUser()
        {
            List<User> users = _usrep.Filter(x => x.UserName.Contains(filter) || x.Name.Contains(filter) || x.LastName.Contains(filter))
                               .OrderByDescending(x => x.JoinDate)
                               .ToList();
                return users;
        }

        public async Task<IEnumerable<User>> SearchUserAsync()
        {
            List<User> users = (await _usrep.FilterAsync(x => x.UserName.Contains(filter) || x.Name.Contains(filter) || x.LastName.Contains(filter)))
                               .OrderByDescending(x => x.JoinDate)
                               .ToList();
            return users;
        }
    }
}