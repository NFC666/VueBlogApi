
using Microsoft.EntityFrameworkCore;
using VueBlog.API.DbContext;
using VueBlog.API.Utility;

namespace VueBlog.API.Services.Essay
{
    public class EssayService : IEssayService
    {
        private readonly AppDbContext db;
        public EssayService(AppDbContext db)
        {
            this.db = db;
        }


        public async Task<Models.Essay> CreateEssayAsync(Models.Essay essay)
        {
            essay.Slug = TextformConverter.ToSlug(essay.Title);
            essay.Views = 0;
            essay.Likes = 0;
            essay.CommentsCount = 0;
            essay.CreatedAt = DateTime.Now;
            essay.UpdatedAt = DateTime.Now;
            essay.Excerpt = TextformConverter.ContentToExcerpt(essay.Content);
            var res = await db.Essays.AddAsync(essay);
            await db.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<Models.Essay> DeleteEssayAsync(Guid id)
        {
            var res = await db.Essays.FirstOrDefaultAsync(e => e.Id == id);
            if(res != null)
            {
                db.Essays.Remove(res);
                await db.SaveChangesAsync();
                return res;
            }
            return null;
        }

        public async Task<Models.Essay> GetEssayById(Guid id)
        {
            var res = await db.Essays.FirstOrDefaultAsync(e => e.Id == id);
            if(res != null)
            {
                return res;
            }
            return null;
        }

        public async Task<List<Models.Essay>> GetEssaysByTagAsync(string[] tags, int page)
        {
            int pageSize = 10;
            if (page < 1) page = 1; // 避免非法页码
            if (tags == null || tags.Length == 0)
            {
                return new List<Models.Essay>(); // 返回空列表而不是 null
            }

            return await db.Set<Models.Essay>()
                .Where(e => e.Tags.Any(tag => tags.Contains(tag))) // 修正列表查询
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Models.Essay>> GetEssaysAsync(int page)
        {
            int pageSize = 10; // 每页10条
            if (page < 1) page = 1; // 避免非法页码

            return await db.Essays
                .OrderByDescending(x => x.UpdatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Models.Essay> UpdateEssayAsync(Guid id,Models.Essay essay)
        {
            var res = await db.Essays.FirstOrDefaultAsync(e => e.Id == id);
            if (res != null)
            {
                res.Slug = TextformConverter.ToSlug(essay.Title);
                res.UpdatedAt = DateTime.Now;
                res.Content = essay.Content;
                res.Excerpt = TextformConverter.ContentToExcerpt(essay.Content);
                res.Title = essay.Title;
                res.Author = essay.Author;
                res.CoverImage = essay.CoverImage;
                res.Tags = essay.Tags;
                res.Category = essay.Category;
                res.IsPublished = essay.IsPublished;
                res.MetaDescription = essay.MetaDescription;
                await db.SaveChangesAsync();
                return res;
            }
            return null;
        }


    }
}
