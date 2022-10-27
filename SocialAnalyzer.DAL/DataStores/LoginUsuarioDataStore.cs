using Microsoft.EntityFrameworkCore;
using SocialAnalyzer.DAL.Interfaces;
using SocialAnalyzer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAnalyzer.DAL.DataStores
{
    public class LoginUsuarioDataStore : DataStore<LoginUsuarios>, ILoginUsuarioDataStore {

        private readonly analizerContext _dbContext;
        public LoginUsuarioDataStore(analizerContext context) : base(context)
    {
        _dbContext = context;
    }




}
}
