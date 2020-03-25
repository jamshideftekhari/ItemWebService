using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DRMusic.Models
{
    public class MusicContext : DbContext
    {
      
        public MusicContext(DbContextOptions<MusicContext> options) : base(options)
        {

        }

        public DbSet<Music> Musics { get; set; }
       
    }
}
