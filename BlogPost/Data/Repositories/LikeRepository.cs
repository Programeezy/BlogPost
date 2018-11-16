using BlogPost.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPost.Data.Repositories
{
    public class LikeRepository
    {
        private BlogContext _context;
        public LikeRepository(BlogContext context)
        {
            _context = context;
        }

        public void Add(Like entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<Like>(entity);
            _context.Set<Like>().Add(entity);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Delete(Like entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<Like>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }
    }
}
