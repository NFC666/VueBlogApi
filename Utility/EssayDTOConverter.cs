using VueBlog.API.Models;

namespace VueBlog.API.Utility
{
    public static class EssayDTOConverter
    {
        public static EssayDTO Converter(Essay entity)
        {
            if (entity == null)
                return null;

            return new EssayDTO
            {
                Id = entity.Id,
                Slug = entity.Slug,
                Views = entity.Views,
                Likes = entity.Likes,
                CommentsCount = entity.CommentsCount,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                Excerpt = entity.Excerpt,
                Title = entity.Title,
                Content = entity.Content,
                Author = entity.Author,
                CoverImage = entity.CoverImage,
                Tags = entity.Tags,
                Category = entity.Category,
                IsPublished = entity.IsPublished,
                MetaDescription = entity.MetaDescription
            };
        }
        public static List<EssayDTO> Converter(List<Essay> entitites)
        {
            List<EssayDTO> essayDTOs = new List<EssayDTO>();
            if (entitites != null)
            {
                for (int i = 0; i < entitites.Count; i++)
                {
                    essayDTOs.Add(Converter(entitites[i]));
                }
            }
            return essayDTOs;
        }
    }
}
